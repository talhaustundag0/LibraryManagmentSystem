using LibraryManagment.Data.Model;
using LibraryManagment.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class BooksController : Controller
    {
        UnitOfWork unitOfWork;
        public BooksController()
        {
            unitOfWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            // Veritabanındaki Kitap verilerini getirdik
            var books = unitOfWork.GetRepository<Books>().GetAll();
            return View(books);
        }

        //Kitap Ekleme İşlemi
        public ActionResult Add()
        {
            // Kitap eklerken yazar ve kategori seçmek için yazar ve kategori tablolarını çektik
            ViewBag.Categories = unitOfWork.GetRepository<Category>().GetAll();
            ViewBag.Writers = unitOfWork.GetRepository<Writer>().GetAll();
            return View();
        }

        //Kitap Ekleme İşlemi
        [HttpPost]
        public JsonResult EkleJson(string[] categories, string writer, string bookName, string bookPiece)
        {
            //Girilen değerlerin boş olup olmadığını kontrol ettik
            if (categories != null && !string.IsNullOrEmpty(writer) && !string.IsNullOrEmpty(bookName) && !string.IsNullOrEmpty(bookPiece))
            {
                // Birden fazla kategori ekleyebildiğimiz için Liste oluşturduk ve foreach ile döndük
                List<Category> c = new List<Category>();
                foreach (var ctgIds in categories)
                {
                    var categoryID = Convert.ToInt32(ctgIds);
                    var category = unitOfWork.GetRepository<Category>().GetById(categoryID);
                    c.Add(category);
                }

                // Yeni kitap nesnesi oluşturup çektiğimiz değerleri verdik
                Books book = new Books();
                book.name = bookName;
                book.piece = Convert.ToInt32(bookPiece);
                book.WriterID = Convert.ToInt32(writer);
                book.DOAdd = DateTime.Now;
                book.Categories = c;
                
                //Kitabı veritabanına kaydetme işlemleri
                //Status değişkeni ile kaydetmeden tekrar verilerin eksikliğini kontrol ettik
                unitOfWork.GetRepository<Books>().Add(book);
                unitOfWork.SaveChanges();
                int status = unitOfWork.SaveChanges();
                if (status > 0)
                {
                    return Json("1");
                }  
                else return Json("0");
            }
            else
                return Json("cannotNull");
            
        }

        //Kitap Silme İşlemi
        [HttpPost]
        public JsonResult DeleteJson(int booksId)
        {
            //ID'ye göre kitabı sildik ve değişiklikleri kaydettik
            unitOfWork.GetRepository<Books>().Delete(booksId);
            int status = unitOfWork.SaveChanges();
            if (status > 0)
            {
                return Json("1");
            }
            else
                return Json("0");
        }

        //Kitap Güncelleme İşlemi
        public ActionResult Update(int booksId)
        {
            //Kitabı güncelleyebilmek için veritabanında kayıtlı tüm bilgilerini çekttik
            ViewBag.Categories = unitOfWork.GetRepository<Category>().GetAll();
            ViewBag.Writers = unitOfWork.GetRepository<Writer>().GetAll();
            Books book = unitOfWork.GetRepository<Books>().GetById(booksId);
            return View(book);
        }

        //Kitap Güncelleme İşlemi
        [HttpPost]
        public JsonResult UpdateJson(int booksId, string[] categories, string writer, string bookName, string bookPiece)
        {
            //Gelen verilerin boş olup olmadığını kontrol ettik
            if (categories != null && !string.IsNullOrEmpty(bookName) && !string.IsNullOrEmpty(bookPiece))
            {
                //Birden fazla kategori ekleyebildiğimiz için foreach ile döndük
                List<Category> c = new List<Category>();
                foreach (var ctgIds in categories)
                {
                    var categoryID = Convert.ToInt32(ctgIds);
                    var category = unitOfWork.GetRepository<Category>().GetById(categoryID);
                    c.Add(category);
                }

                //Yeni gelen verilerle kitap verilerini değiştirdik ve güncelleme işlemini yaptık
                var book = unitOfWork.GetRepository<Books>().GetById(booksId);
                book.name = bookName;
                book.piece = Convert.ToInt32(bookPiece);
                book.WriterID = Convert.ToInt32(writer);
                book.Categories.Clear();
                book.Categories = c;
                unitOfWork.GetRepository<Books>().Update(book);
                unitOfWork.SaveChanges();
                int status = unitOfWork.SaveChanges();
                if (status > 0)
                {
                    return Json("1");
                }
                else return Json("0");
            }
            else
                return Json("cannotNull");

        }
    }
     
}
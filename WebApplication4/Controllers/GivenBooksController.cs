using LibraryManagment.Data.Model;
using LibraryManagment.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class GivenBooksController : Controller
    {
        UnitOfWork unitOfWork;
        public GivenBooksController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            //Teslim edilmemiş kitapları veritabanından çektik
            var givenbooks = unitOfWork.GetRepository<GivenBooks>().GetAll(x => x.Delivered == null);
            return View(givenbooks);
        }

        public ActionResult DeliveredBooks()
        {
            //Teslim edilmiş kitapları veritabanından çektik
            var givenbooks = unitOfWork.GetRepository<GivenBooks>().GetAll(x => x.Delivered != null);
            return View(givenbooks);
        }

        public ActionResult GiveBooks()
        {
            //Adeti en az 1 olan kitapları veritabanından çektik
            ViewBag.Books = unitOfWork.GetRepository<Books>().GetAll(x => x.piece > 0);
            ViewBag.Members = unitOfWork.GetRepository<Members>().GetAll();
            return View();
        }

        //Kitap verme işlemi
        [HttpPost]
        public JsonResult GiveBooksJson(int memberId, int bookId, DateTime deliveryDate)
        {
            //Verilecek kitap için nesne oluşturduk ve gerekli değer atamalarını yaptık
            GivenBooks givenbook = new GivenBooks();
            //Kitabın verildiği tarih
            givenbook.Receiving = DateTime.Now;
            //Teslim edileceği tarih
            givenbook.Delivery = deliveryDate;
            givenbook.BookID = bookId;
            givenbook.MemberID = memberId;
            //Kitabı verilen kitap tablosuna ekledik ve durum kontrolü yaparak işlemleri kaydettik
            unitOfWork.GetRepository<GivenBooks>().Add(givenbook);
            var status = unitOfWork.SaveChanges();
            if (status > 0)
            {
                return Json("1");
            }
            else return Json("0");
        }

        public ActionResult UpdateGivenBooks(int givenbooksId)
        {
            //Verilmiş olan kitabın bilgilerini çektik
            ViewBag.Books = unitOfWork.GetRepository<Books>().GetAll(x => x.piece > 0);
            ViewBag.Members = unitOfWork.GetRepository<Members>().GetAll();
            var givenbook = unitOfWork.GetRepository<GivenBooks>().GetById(givenbooksId);
            return View(givenbook);
        }

        //Verilen kitap üstünde güncelleme işlemi
        [HttpPost]
        public JsonResult UpdateGivenBooksJson(int GivenBooksId, int memberId, int bookId, DateTime deliveryDate)
        {
            var givenbook = unitOfWork.GetRepository<GivenBooks>().GetById(GivenBooksId);
            givenbook.Delivery = deliveryDate;
            givenbook.BookID = bookId;
            givenbook.MemberID = memberId;
            unitOfWork.GetRepository<GivenBooks>().Update(givenbook);
            var status = unitOfWork.SaveChanges();
            if (status > 0)
            {
                return Json("1");
            }
            else return Json("0");
        }

        //Kitabı geri alma işlemi
        [HttpPost]
        public JsonResult TakeJson(int givenbooksId)
        {
            var givenbook = unitOfWork.GetRepository<GivenBooks>().GetById(givenbooksId);
            givenbook.Delivered = DateTime.Now;
            unitOfWork.GetRepository<GivenBooks>().Update(givenbook);
            var status = unitOfWork.SaveChanges();
            if (status > 0)
            {
                return Json("1");
            }
            else return Json("0");
        }
    }
}
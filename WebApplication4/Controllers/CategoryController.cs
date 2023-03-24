using LibraryManagment.Data;
using LibraryManagment.Data.Model;
using LibraryManagment.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class CategoryController : Controller
    {
        UnitOfWork unitOfWork;
        public CategoryController()
        {
            unitOfWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            //Veritabanındaki kategori bilgilerini getirdik
            var categories = unitOfWork.GetRepository<Category>().GetAll();
            return View(categories);
        }

        //Kategori Ekleme İşlemi
        [HttpPost]
        public JsonResult AddJson(string ctgName)
        {
            Category ctg = new Category();
            ctg.CategoryName = ctgName;
            var AddedCtg = unitOfWork.GetRepository<Category>().Add(ctg);
            unitOfWork.SaveChanges();
            //Json'a verileri gönderiyoruz
            return Json(
                new
                {
                    Result = new
                    {
                        AddedCtg.ID,
                        AddedCtg.CategoryName
                    },
                    //Verilerin Json'a gönderilebilmesi için izin veriyoruz
                    JsonRequestBehavior.AllowGet
                }
                );
        }

        //Kategori Güncelleme İşlemi
        [HttpPost]
        public JsonResult UpdateJson(int ctgId, string ctgCategoryName)
        {
            //Id'ye göre güncellemek istediğimiz kategorinin bilgisini çektik ve yeni değeri verip kaydettik
            var category = unitOfWork.GetRepository<Category>().GetById(ctgId);
            category.CategoryName = ctgCategoryName;
            var status = unitOfWork.SaveChanges();
            if (status > 0) return Json("1");
            return Json("0");
        }

        //Kategori Silme İşlemi
        [HttpPost]
        public JsonResult DeleteJson(int ctgId)
        {
            //Id'ye göre kategori verisini aldık ve sildik işlemleri kaydettik
            unitOfWork.GetRepository<Category>().Delete(ctgId);
            var status = unitOfWork.SaveChanges();
            if (status > 0) return Json("1");
            return Json("0");
        }
    }
}
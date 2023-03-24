using LibraryManagment.Data.Model;
using LibraryManagment.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class WriterController : Controller
    {
        UnitOfWork unitOfWork;

        public WriterController()
        {
            unitOfWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            var writers = unitOfWork.GetRepository<Writer>().GetAll();
            return View(writers);
        }

        // Yazar Ekleme İşlemi

        [HttpPost]
        public JsonResult AddJson(string wrtName)
        {
            Writer wrt = new Writer();
            wrt.Name = wrtName;
            var AddedWrt = unitOfWork.GetRepository<Writer>().Add(wrt);
            unitOfWork.SaveChanges();
            //Verileri Json'a gönderiyoruz
            return Json(
                new
                {
                    Result = new
                    {
                        AddedWrt.ID,
                        AddedWrt.Name
                    },
                    //Verilerin Json'a gönderilebilmesi için izin veriyoruz
                    JsonRequestBehavior.AllowGet
                }
                );
        }

        // Yazar Güncelleme İşlemi
        [HttpPost]
        public JsonResult UpdateJson(int wrtId, string wrtName)
        {
            var writer = unitOfWork.GetRepository<Writer>().GetById(wrtId);
            writer.Name = wrtName;
            var status = unitOfWork.SaveChanges();
            if (status > 0) return Json("1");
            return Json("0");
        }

        // Yazar Silme İşlemi
        [HttpPost]
        public JsonResult DeleteJson(int wrtId)
        {
            unitOfWork.GetRepository<Writer>().Delete(wrtId);
            var status = unitOfWork.SaveChanges();
            if (status > 0) return Json("1");
            return Json("0");
        }
    }
}
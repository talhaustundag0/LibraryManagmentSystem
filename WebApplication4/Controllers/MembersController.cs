using LibraryManagment.Data.Model;
using LibraryManagment.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class MembersController : Controller
    {
        UnitOfWork unitOfWork;
        public MembersController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            // Üye bilgilerini veritabanından çekiyoruz
            var members = unitOfWork.GetRepository<Members>().GetAll();
            return View(members);
        }

        public ActionResult Add()
        {
            return View();
        }

        // Üye Ekleme İşlemi
        [HttpPost]
        public JsonResult AddJson(string memberName, string memberLastName, string memberTCKNO, string memberPhone)
        {
            //Gelen verilerin boş olup olmadığını kontrol ediyoruz ve oluşturduğumuz üye nesnesine değerleri atıyoruz ve veritabanına ekliyoruz
            if (!string.IsNullOrEmpty(memberName) && !string.IsNullOrEmpty(memberLastName) && !string.IsNullOrEmpty(memberTCKNO) && !string.IsNullOrEmpty(memberPhone))
            {
                if (memberTCKNO.Length!=11)
                {
                    return Json("2");
                }
                else if (memberPhone.Length!=11)
                {
                    return Json("2");
                }
                Members member = new Members();
                member.Name = memberName;
                member.Lastname = memberLastName;
                member.TCKNO = memberTCKNO;
                member.Phone = memberPhone;
                member.DORegistration = DateTime.Now;
                unitOfWork.GetRepository<Members>().Add(member);
                var status = unitOfWork.SaveChanges();
                if (status > 0)
                {
                    return Json("1");
                }
                else
                    return Json("0");
            }
            else return Json("cannotNull");
        }


        // Üye Silme İşlemi
        [HttpPost]
        public JsonResult DeleteJson(int memberID)
        {
            unitOfWork.GetRepository<Members>().Delete(memberID);
            var status = unitOfWork.SaveChanges();
            if (status > 0) return Json("1");
            return Json("0");
        }

        public ActionResult Update(int memberId)
        {
            var member = unitOfWork.GetRepository<Members>().GetById(memberId);
            return View(member);
        }

        // Üye Güncelleme İşlemi
        [HttpPost]
        public JsonResult UpdateJson(int memberID, string memberName, string memberLastName, string memberTCKNO, string memberPhone)
        {
            // Yeni gelen verilerin boş olup olmadığını kontrol ediyoruz ve aynı Id'ye sahip olacak şekilde değişiklikleri uyguluyoruz
            if (!string.IsNullOrEmpty(memberName) && !string.IsNullOrEmpty(memberLastName) && !string.IsNullOrEmpty(memberTCKNO) && !string.IsNullOrEmpty(memberPhone))
            {
                var member = unitOfWork.GetRepository<Members>().GetById(memberID);
                member.Name = memberName;
                member.Lastname = memberLastName;
                member.TCKNO = memberTCKNO;
                member.Phone = memberPhone;
                unitOfWork.GetRepository<Members>().Update(member);
                var status = unitOfWork.SaveChanges();
                if (status > 0)
                {
                    return Json("1");
                }
                else
                    return Json("0");
            }
            else return Json("cannotNull");
        }
    }
}
using LibraryManagment.Data.Model;
using LibraryManagment.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagment.Data.HelperClass;

namespace LibraryManagmentSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public LoginController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            //Giriş yapmış kişi çıkış yapmadan login sayfasına erişemesin diye kitapların ana sayfasına yönlendiriyoruz
            if (Request.Cookies["member"] != null)
            {
                return RedirectToAction("Index", "Books");
            }
            return View();
        }

        //Giriş yapma işlemi
        [HttpPost]
        public JsonResult LoginControl(string mail, string password, bool remember)
        {
            //Mail ve şifredeki boşlukları temizledik ve boş olup olmadıklarını kontrol ettik
            mail = mail.Trim();
            password = password.Trim();
            if (string.IsNullOrEmpty(mail) && string.IsNullOrEmpty(password))
                return Json("cannotNull");

            //Veritabanında girilen mail ve şifreye sahip üyenin verilerini alıyoruz
            var member = new Members();
            try
            {
                member = _unitOfWork.GetRepository<Members>().Get(x => x.Mail == mail && x.Password == password);
            }
            catch { }

            // Cookie ekleme işlemi
            if (member != null)
            {
                HttpCookie cookie = new HttpCookie("member");
                cookie.Values.Add("Id", member.ID.ToString());
                cookie.Values.Add("Name", member.Name.ToString());
                cookie.Values.Add("LastName", member.Lastname.ToString());

                //Beni hatırla checkbox'ı aktif ise cookie'yi 1 gün saklıyoruz
                if (remember) { cookie.Expires = DateTime.Now.AddDays(1); }

                Response.Cookies.Add(cookie);

                return Json("Success");
            }
            else return Json("Error");
        }

        //Çıkış yapma işlemi
        public ActionResult Logout()
        {
            var cookie = Response.Cookies["member"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index");
        }
    }
}
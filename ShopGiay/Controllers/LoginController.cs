using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;

namespace ShopGiay.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ShopBanGiayEntities db = new ShopBanGiayEntities();
        public ActionResult Register()
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Register(Nguoi_dung nguoi_Dung)
        {
            if (ModelState.IsValid)
            {
                db.Nguoi_dung.Add(nguoi_Dung);
                db.SaveChanges();
            }
            return RedirectToAction("Index","TTN_Shop");
        }
        public ActionResult Login()
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            return View();
        }
    }
}
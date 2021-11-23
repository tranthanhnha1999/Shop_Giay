using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;
namespace ShopGiay.Areas.Admin.Controllers
{
    public class DonhangController : Controller
    {
        // GET: Admin/Order
        ShopBanGiayEntities db = new ShopBanGiayEntities();
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return Redirect("https://localhost:44316/Login/Login");
            var model = db.Don_hang.ToList();
            return View(model);
        }
        public ActionResult Details(int id )
        {
            ViewBag.dh = db.Don_hang.Find(id);
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            var model = db.Chi_tiet_don_hang.Where(p => p.ID_Donhang == id ).ToList();
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            if (Session["Admin"] == null)
                return Redirect("https://localhost:44316/Login/Login");
            var model = db.Don_hang.Find(id);
            model.ID_Nhanvien =(int) Session["Admin"];
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Don_hang don_Hang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(don_Hang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Donhang");
            }
            return RedirectToAction("Edit", "Donhang");
        }
    }
}
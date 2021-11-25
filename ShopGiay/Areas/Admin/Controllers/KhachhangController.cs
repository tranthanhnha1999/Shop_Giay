using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;

namespace ShopGiay.Areas.Admin.Controllers
{
    public class KhachhangController : Controller
    {
        ShopBanGiayEntities db = new ShopBanGiayEntities();
        // GET: Admin/Khachhang
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return Redirect("https://localhost:44316/Login/Login");
            var mdoel = db.Nguoi_dung.ToList();
            return View(mdoel);
        }
        public ActionResult Edit(int id)
        {
            if (Session["Admin"] == null)
                return Redirect("https://localhost:44316/Login/Login");
            ViewBag.vaitro = new SelectList(db.Vai_tro.ToList(), "ID_Vai_tro", "Ten_vai_tro");
            var model = db.Nguoi_dung.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Nguoi_dung nguoi_Dung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoi_Dung).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Khachhang");
            }
            return RedirectToAction("Edit", "Khachhang");
        }
    }
}
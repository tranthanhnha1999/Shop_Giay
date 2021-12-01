using System;
using System.Collections.Generic;
using System.Data.Entity;
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
           
            var model = db.Nguoi_dung.Find(id);
            ViewBag.vaitro = new SelectList(db.Vai_tro.ToList(), "ID_Vai_tro", "Ten_vai_tro", model.ID_vaitro);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Nguoi_dung nguoi_Dung)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(nguoi_Dung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Khachhang");
            }
            ViewBag.vaitro = new SelectList(db.Vai_tro.ToList(), "ID_Vai_tro", "Ten_vai_tro", nguoi_Dung.ID_vaitro);
            return View(nguoi_Dung);
        }
        public ActionResult EditTrangThai(int id)
        {
            var model = db.Nguoi_dung.Find(id);
            ViewBag.vaitro = new SelectList(db.Vai_tro.ToList(), "ID_Vai_tro", "Ten_vai_tro", model.ID_vaitro);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditTrangThai(Nguoi_dung nguoi_Dung)
        {
            ViewBag.vaitro = new SelectList(db.Vai_tro.ToList(), "ID_Vai_tro", "Ten_vai_tro", nguoi_Dung.ID_vaitro);
            try
            {
                db.Entry(nguoi_Dung).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Khachhang");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Chỉnh sửa không thành công");
                return View(nguoi_Dung);
            }
        }
    }
}
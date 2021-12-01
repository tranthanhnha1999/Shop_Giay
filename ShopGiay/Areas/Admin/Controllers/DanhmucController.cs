using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;

namespace ShopGiay.Areas.Admin.Controllers
{
    public class DanhmucController : Controller
    {
        // GET: Admin/Danhmuc
        ShopBanGiayEntities db = new ShopBanGiayEntities();
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return Redirect("https://localhost:44316/Login/Login");
            var model = db.Danh_muc.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
                return Redirect("https://localhost:44316/Login/Login");
            ViewBag.id_ndm = new SelectList(db.Nhom_Danh_Muc.ToList(), "ID_Nhom_Danh_muc", "Ten_Nhom_Danh_muc");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Danh_muc danh_Muc)
        {
            try
            {
                ViewBag.id_ndm = new SelectList(db.Nhom_Danh_Muc.ToList(), "ID_Nhom_Danh_muc", "Ten_Nhom_Danh_muc");
                var id_dm = db.Danh_muc.FirstOrDefault(p => p.ID_Danhmuc == danh_Muc.ID_Danhmuc);
                if (ModelState.IsValid)
                {
                    if (id_dm != null)
                    {
                        ViewBag.checkiddm = "ID Danh mục đã tồn tại";
                        return View();
                        
                    }
                    else
                    {
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.Danh_muc.Add(danh_Muc);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Danhmuc");
                    }

                }
                return View();
            }
            catch
            {
                return View();
            }
            

        }
        public ActionResult Details(string id)
        {
            var model = db.Danh_muc.Find(id);
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            ViewBag.id_ndm = new SelectList(db.Nhom_Danh_Muc.ToList(), "ID_Nhom_Danh_muc", "Ten_Nhom_Danh_muc");
            var model = db.Danh_muc.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Danh_muc danh_Muc)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(danh_Muc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Danhmuc");
            }
            return View();

        }
        public ActionResult Delete(string id)
        {
            var model = db.Danh_muc.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ComfirmDelete(string id)
        {
            Danh_muc item = db.Danh_muc.Find(id);
            item.Trangthai = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "Danhmuc");
        }

    }
}
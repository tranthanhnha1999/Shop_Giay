using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;

namespace ShopGiay.Areas.Admin.Controllers
{
    public class NhomdanhmucController : Controller
    {
        // GET: Admin/Nhomdanhmuc
        ShopBanGiayEntities db = new ShopBanGiayEntities();
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return Redirect("https://localhost:44316/Login/Login");
            var model = db.Nhom_Danh_Muc.ToList();
            return View(model);
        }
        public ActionResult create()
        {
            if (Session["Admin"] == null)
                return Redirect("https://localhost:44316/Login/Login");
            return View();
        }
        [HttpPost]
        public ActionResult create(Nhom_Danh_Muc nhom_Danh_Muc)
        {
            var id_ndm = db.Nhom_Danh_Muc.FirstOrDefault(p => p.ID_Nhom_Danh_muc == nhom_Danh_Muc.ID_Nhom_Danh_muc);
            if (ModelState.IsValid)
            {   
                if(id_ndm == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Nhom_Danh_Muc.Add(nhom_Danh_Muc);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Nhomdanhmuc");
                }
                else
                {
                    ViewBag.checkidndm = "ID_Nhom Danh mục đã tồn tại";
                    return View();
                }
                
                
            }
            return View();
            
        }
        public ActionResult Delete(String id)
        {
            var item = db.Nhom_Danh_Muc.Find(id);
            return View(item);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult ComfirmDelete(String id)
        {
            var item = db.Nhom_Danh_Muc.Find(id);
            db.Nhom_Danh_Muc.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index", "Nhomdanhmuc");
        }
        public ActionResult Edit(String id)
        {
            var item = db.Nhom_Danh_Muc.Find(id);
            return View(item);
        }
        [HttpPost,ActionName("Edit")]
        public ActionResult Edit(Nhom_Danh_Muc nhom_Danh_Muc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhom_Danh_Muc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Nhomdanhmuc");
            }
            return View();
        }
        public ActionResult Details(String id) {
            var model = db.Nhom_Danh_Muc.Find(id);
            return View(model);
        }

    }
}
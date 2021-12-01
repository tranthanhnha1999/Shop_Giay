using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;
namespace ShopGiay.Areas.Admin.Controllers
{
    public class AdminSanphamController : Controller
    {
        // GET: Admin/AdminSanpham
        ShopBanGiayEntities db = new ShopBanGiayEntities();
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            
                return Redirect("https://localhost:44316/Login/Login");
            var models = db.San_pham.OrderByDescending(p=>p.Ngaycapnhat).ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            return View(models);
        }
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
                return Redirect("https://localhost:44316/Login/Login");
            ViewBag.idKhuyenmai = new SelectList(db.Khuyen_mai.ToList(), "ID_Khuyenmai", "Ten_giamgia");
            ViewBag.idDanhmuc = new SelectList(db.Danh_muc.ToList(), "ID_Danhmuc", "Ten_Danhmuc");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(AdminSanpham adminSanpham ,San_pham sP, HttpPostedFileBase Images, HttpPostedFileBase Images1, HttpPostedFileBase Images2) 
        {
            
            if (ModelState.IsValid)
            {
                San_pham san_Pham = new San_pham();
                san_Pham.Tensanpham = adminSanpham.san_Pham.Tensanpham;
                san_Pham.Ngaycapnhat = adminSanpham.san_Pham.Ngaycapnhat;
                san_Pham.Mota = adminSanpham.san_Pham.Mota;
                san_Pham.Gia = adminSanpham.san_Pham.Gia;
                san_Pham.ID_Khuyenmai = sP.ID_Khuyenmai;
                san_Pham.ID_Danh_Muc = sP.ID_Danh_Muc;
                db.San_pham.Add(san_Pham);
                db.SaveChanges();
                try
                {
                    Hinh_anh ha = new Hinh_anh();
                    ha.ID_San_Pham = san_Pham.ID_Sanpham;
                    if (Images != null && Images.ContentLength > 0)
                    {
                        ha.Hinhchinh = Images.FileName;
                        string urlImages = Server.MapPath("~/Asset/images/" + ha.Hinhchinh);
                        Images.SaveAs(urlImages);
                        db.Hinh_anh.Add(ha);
                    }
                    if (Images1 != null && Images1.ContentLength > 0)
                    {
                        ha.Hinh1 = Images1.FileName;
                        string urlImages1 = Server.MapPath("~/Asset/images/" + ha.Hinh1);
                        Images1.SaveAs(urlImages1);
                        db.Hinh_anh.Add(ha);
                    }
                    if (Images2 != null && Images2.ContentLength > 0)
                    {
                        ha.Hinh2 = Images2.FileName;
                        string urlImages2 = Server.MapPath("~/Asset/images/" + ha.Hinh2);
                        Images2.SaveAs(urlImages2);
                        db.Hinh_anh.Add(ha);
                    }
                }
                catch (Exception) { return View(); }
                db.SaveChanges();

                return RedirectToAction("Index", "AdminSanpham");
            }

            return View();
        }
        public ActionResult Details(int id)
        {
            var model = db.San_pham.Find(id);
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var model = db.San_pham.Find(id);
            ViewBag.idKhuyenmai = new SelectList(db.Khuyen_mai.ToList(), "ID_Khuyenmai", "Ten_giamgia");
            ViewBag.idDanhmuc = new SelectList(db.Danh_muc.ToList(), "ID_Danhmuc", "Ten_Danhmuc");
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(San_pham san_Pham ,Hinh_anh hinh_Anh)
        {
            ViewBag.idKhuyenmai = new SelectList(db.Khuyen_mai.ToList(), "ID_Khuyenmai", "Ten_giamgia");
            ViewBag.idDanhmuc = new SelectList(db.Danh_muc.ToList(), "ID_Danhmuc", "Ten_Danhmuc");
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            if (ModelState.IsValid)
            {
                db.Entry(san_Pham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "AdminSanpham");
            }
            return RedirectToAction("Edit", "AdminSanpham");
        }
        public ActionResult Delete(int id)
        {
            var model = db.San_pham.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ComfirmDelete(int id)
        {
            San_pham item = db.San_pham.Find(id);
            item.Trangthai = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "AdminSanpham");
        }
    }
    
}
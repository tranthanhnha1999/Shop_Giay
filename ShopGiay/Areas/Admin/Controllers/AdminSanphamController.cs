using System;
using System.Collections.Generic;
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
        public ActionResult Create(AdminSanpham adminSanpham,HttpPostedFileBase Images, HttpPostedFileBase Images1, HttpPostedFileBase Images2) 
        {
            
            if (ModelState.IsValid)
            {
                San_pham san_Pham = new San_pham();
                san_Pham.Tensanpham = adminSanpham.san_Pham.Tensanpham;
                san_Pham.Ngaycapnhat = adminSanpham.san_Pham.Ngaycapnhat;
                san_Pham.Mota = adminSanpham.san_Pham.Mota;
                san_Pham.Gia = adminSanpham.san_Pham.Gia;
                san_Pham.ID_Khuyenmai = adminSanpham.san_Pham.ID_Khuyenmai;
                san_Pham.ID_Danh_Muc = adminSanpham.san_Pham.ID_Danh_Muc;
                db.San_pham.Add(san_Pham);
                db.SaveChanges();
                Hinh_anh ha = new Hinh_anh();
                ha.ID_San_Pham = san_Pham.ID_Sanpham;
                if (Images != null && Images.ContentLength > 0)
                {
                    
                    ha.Hinhchinh = Images.FileName;
                    ha.Hinh1 = Images1.FileName;
                    ha.Hinh2 = Images2.FileName;
                    string urlImages = Server.MapPath("~/Asset/images/" + ha.Hinhchinh);
                    string urlImages1 = Server.MapPath("~/Asset/images/" + ha.Hinh1);
                    string urlImages2 = Server.MapPath("~/Asset/images/" + ha.Hinh2);
                    Images.SaveAs(urlImages);
                    Images.SaveAs(urlImages1);
                    Images2.SaveAs(urlImages2);
                }
                db.Hinh_anh.Add(ha);
                db.SaveChanges();

                return RedirectToAction("Index", "AdminSanpham");
            }
            ViewBag.idKhuyenmai = new SelectList(db.Khuyen_mai, "ID_Khuyenmai", "Ten_giamgia",adminSanpham.san_Pham.ID_Khuyenmai);
            ViewBag.idDanhmuc = new SelectList(db.Danh_muc, "ID_Danhmuc", "Ten_Danhmuc",adminSanpham.san_Pham.ID_Danh_Muc);
            return View(adminSanpham);
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
    }
    
}
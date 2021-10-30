using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShopGiay.Models;

namespace ShopGiay.Controllers
{
    public class TTN_ShopController : Controller
    {
        public ShopBanGiayEntities db = new ShopBanGiayEntities();
        public IEnumerable<San_pham> AllListPaging(int page, int pageSize)
        {
            return db.San_pham.OrderByDescending(x => x.Ngaycapnhat).ToPagedList(page, pageSize);
        }
        public IEnumerable<San_pham> AllListPagingByDanhmuc(int page, int pageSize, string iddm)
        {
            return db.San_pham.OrderByDescending(x => x.Ngaycapnhat).Where(x => x.ID_Danh_Muc.Equals(iddm)).ToPagedList(page, pageSize);
        }
        // GET: TTN_Shop
        public ActionResult Index()
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            ViewBag.slide = db.Slides.ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            var model = (from S in db.San_pham orderby S.Ngaycapnhat descending select S).Take(12);

            
            return View(model);
        }
        public ActionResult Product(string id ,int page = 1 ,int pageSize = 16)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            if (id == null)
            {
                var model = AllListPaging(page, pageSize);
                ViewBag.id = null;
                return View(model);
            }
            else
            {
               
                var model = AllListPagingByDanhmuc(page,pageSize,id);
                ViewBag.id = id;
                return View(model);
            }
            
        }
        public ActionResult Details(int id)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            var model = db.San_pham.Find(id);

            return View(model);
        }
    }
}
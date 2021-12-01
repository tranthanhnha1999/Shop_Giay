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
            return db.San_pham.OrderByDescending(x => x.Ngaycapnhat).Where(x=>x.Trangthai != 1).ToPagedList(page, pageSize);
        }
        public IEnumerable<San_pham> AllListPagingByDanhmuc(int page, int pageSize, string iddm)
        {
            return db.San_pham.OrderByDescending(x => x.Ngaycapnhat).Where(x => x.ID_Danh_Muc.Equals(iddm) && x.Trangthai != 1).ToPagedList(page, pageSize);
        }
        public IEnumerable<San_pham> AllListPagingByNhomdanhmuc(int page, int pageSize, string idndm)
        {

            var model = (from s in db.San_pham
                         join d in db.Danh_muc on s.ID_Danh_Muc equals d.ID_Danhmuc
                         join n in db.Nhom_Danh_Muc on d.ID_Nhom_Danh_Muc equals idndm
                         select s).Distinct().OrderByDescending(x => x.Ngaycapnhat).Where(p=>p.Trangthai != 1).ToPagedList(page, pageSize);

            return model;
        }
        // GET: TTN_Shop
        public ActionResult Index()
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p=>p.Trangthai!=1).ToList();
            ViewBag.slide = db.Slides.ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            var model = (from S in db.San_pham orderby S.Ngaycapnhat descending where S.Trangthai != 1 select S).Take(12);
            
            return View(model);
        }
        public ActionResult Product(string idndm, string iddm, int page = 1, int pageSize = 16)
        {
            
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            if (iddm != null)
            {
                 var model = AllListPagingByDanhmuc(page, pageSize, iddm);
                ViewBag.id = iddm;
                return View(model);
            }
            else if (idndm != null)
            {
                 var model = AllListPagingByNhomdanhmuc(page, pageSize, idndm);
                ViewBag.idndm = idndm;
                return View(model);
            }
            else
            {
                 var model = AllListPaging(page, pageSize);
                ViewBag.idndm = null;
                return View(model);
            }

        }
        public ActionResult Details(int id)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            var model = db.San_pham.Find(id);

            return View(model);
        }
        public ActionResult Lichsumh()
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            int id_user = (int)Session["Nguoidung"];
            var models = db.Don_hang.Where(p => p.ID_Nguoidung == id_user).OrderBy(p => p.Ngay_Dat_hang);
            return View(models);
        }
        public ActionResult Detaildh(int id)
        {
            ViewBag.dh = db.Don_hang.Find(id);
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            var model = db.Chi_tiet_don_hang.Where(p => p.ID_Donhang == id).ToList();
            return View(model);
        }
        public ActionResult Search(String search_param)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            try
            {
                int id = Int32.Parse(search_param);
                ViewBag.San_Pham = db.San_pham.Where(x => x.ID_Sanpham == id).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.San_Pham = db.San_pham.Where(x => x.Tensanpham.Contains(search_param)).ToList();
            };
            return View("Search");
        }
    }
}
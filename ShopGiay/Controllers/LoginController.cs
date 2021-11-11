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
            ViewBag.vaitro = new SelectList(db.Vai_tro.ToList(),"ID_Vai_tro", "Ten_vai_tro");
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Register(Nguoi_dung nguoi_Dung)
        {
            ViewBag.vaitro = new SelectList(db.Vai_tro.ToList(), "ID_Vai_tro", "Ten_vai_tro");
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            var checkemail = db.Nguoi_dung.FirstOrDefault(p => p.Email == nguoi_Dung.Email);
            var checktaikhoan = db.Nguoi_dung.FirstOrDefault(p => p.Tai_Khoan == nguoi_Dung.Tai_Khoan);
            if (ModelState.IsValid)
            {
                if (checkemail == null)
                {
                    if (checktaikhoan == null)
                    {

                        db.Configuration.ValidateOnSaveEnabled = false;
                        nguoi_Dung.Mat_Khau = Encode.EncodeMd5(nguoi_Dung.Mat_Khau);
                        Nguoi_dung nd = new Nguoi_dung();
                        nd.ID_vaitro = 1;
                        nd.Ten_Nguoidung = nguoi_Dung.Ten_Nguoidung;
                        nd.Ngay_sinh = nguoi_Dung.Ngay_sinh;
                        nd.So_DT = nguoi_Dung.So_DT;
                        nd.Hinh_anh = nguoi_Dung.Hinh_anh;
                        nd.Dia_chi = nguoi_Dung.Dia_chi;
                        nd.Email = nguoi_Dung.Email;
                        nd.Tai_Khoan = nguoi_Dung.Tai_Khoan;
                        nd.Mat_Khau = nguoi_Dung.Mat_Khau;
                        db.Nguoi_dung.Add(nd);

                        db.SaveChanges();
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        ViewBag.loitaikhoan = "Tài khoản đã tồn tại !! Vui lòng nhập tài khoản khác";
                        return View();
                    }

                }
                else
                {
                    ViewBag.loiEmail = "Email đã tồn tại !! Vui lòng nhập Email khác";
                    return View();
                }

            }
            return View();

        }
        public ActionResult Login()
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            string pass = Encode.EncodeMd5(password);
            var ketqua = db.Nguoi_dung.Where(p => p.Tai_Khoan.Equals(username) && p.Mat_Khau.Equals(pass)).FirstOrDefault();
            if(ketqua != null)
            {
                Session["Nguoidung"] = ketqua.ID_Nguoidung;
                Session["Tennguoidung"] = ketqua.Ten_Nguoidung;
                Session["Emailnguoidung"] = ketqua.Email;
                Session["Mota"] = ketqua.Dia_chi;
                return RedirectToAction("Index", "TTN_Shop");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
    }
}
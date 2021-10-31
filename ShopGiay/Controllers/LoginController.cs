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
            var checkemail = db.Nguoi_dung.FirstOrDefault(p => p.Email == nguoi_Dung.Email);
            var checktaikhoan = db.Nguoi_dung.FirstOrDefault(p => p.Tai_Khoan == nguoi_Dung.Tai_Khoan);
            if (checkemail == null)
            {
                if(checktaikhoan == null)
                {
                    if (ModelState.IsValid)
                    {
                        nguoi_Dung.Mat_Khau = Encode.EncodeMd5(nguoi_Dung.Mat_Khau);
                        db.Nguoi_dung.Add(nguoi_Dung);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "TTN_Shop");
                }
                else
                {
                    ViewBag.loitaikhoan = "Tài khoản đã tồn tại !! Vui lòng nhập tài khoản khác";
                    return RedirectToAction("Register","Login");
                }
               
            }
            else
            {
                ViewBag.loiEmail = "Email đã tồn tại !! Vui lòng nhập Email khác";
                return RedirectToAction("Register", "Login");
            }
            
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
                
                return RedirectToAction("Index", "TTN_Shop");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Register(Nguoi_dung nguoi_Dung)
        {
            ViewBag.vaitro = new SelectList(db.Vai_tro.ToList(), "ID_Vai_tro", "Ten_vai_tro");
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
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
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            string pass = Encode.EncodeMd5(password);
            var ketqua = db.Nguoi_dung.Where(p => p.Tai_Khoan.Equals(username) && p.Mat_Khau.Equals(pass)).FirstOrDefault();
            if(ketqua != null)
            {
                if(ketqua.Trangthai != 1) { 
                    if(ketqua.ID_vaitro == 1) { 
                    Session["Nguoidung"] = ketqua.ID_Nguoidung;
                    Session["Tennguoidung"] = ketqua.Ten_Nguoidung;
                    Session["Emailnguoidung"] = ketqua.Email;
                    Session["Mota"] = ketqua.Dia_chi;
                    Session["Sdt"] = ketqua.So_DT;
                    return RedirectToAction("Index", "TTN_Shop");
                    }
                    else
                    {
                        Session["Admin"] = ketqua.ID_Nguoidung;
                        return Redirect("https://localhost:44316/Admin/AdminHome");
                    }
                }
                else
                {
                    ViewBag.loitaikhoan = "Tài khoản này đã bị khóa !!";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
        public ActionResult ForgotPass() {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPass(string email)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            var kq = db.Nguoi_dung.Where(p => p.Email.Equals(email)).FirstOrDefault();
            string message = "";
            if (kq != null)
            {
                int r = new Random().Next(1000, 9999);

                kq.Code = r;
                db.SaveChanges();
                message += "<br/> Mã code:"+kq.Code ;
                SendMail(email, "Đơn hàng vừa đặt từ www.ttn_shop.com", message);
                return RedirectToAction("ChangePass", "Login",new {id =kq.ID_Nguoidung });
            }
            else
            {
                ViewBag.loi = "Email này chưa có đăng ký !!";
                return View();

            }
        }
        public ActionResult ChangePass(int id)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            ViewBag.Email = db.Nguoi_dung.Find(id).Email;
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(string email,int Code,string password)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.Where(p => p.Trangthai != 1).ToList();
            var kq = db.Nguoi_dung.Where(p => p.Email.Equals(email)).FirstOrDefault();
            if (kq.Code == Code)
            {
                kq.Code = null;
                kq.Mat_Khau = Encode.EncodeMd5(password);
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.Email = email;
                ViewBag.loicode = "Mã code không đúng !!";
                return View();

            }
        }
        public void SendMail(string address, string subject, string message)
        {
            string email = "nhacuteeee@gmail.com";
            string password = "nha151199";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpclient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpclient.EnableSsl = true;
            smtpclient.UseDefaultCredentials = false;
            smtpclient.Credentials = loginInfo;
            smtpclient.Send(msg);
        }

    }
}
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
    public class GiohangController : Controller
    {
        ShopBanGiayEntities db = new ShopBanGiayEntities();
        // GET: Giohang
        public ActionResult Index()
        {
            
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            List<Giohang> list = (List<Giohang>)Session["cartSession"];
            return View(list);
        }
        public ActionResult themgiohang(int id)
        {
            ViewBag.nhomdanhmuc = db.Nhom_Danh_Muc.ToList();
            ViewBag.danhmuc = db.Danh_muc.ToList();
            ViewBag.hinhanh = db.Hinh_anh.ToList();
            var gh = Session["cartSession"];
            List<Giohang> list = new List<Giohang>();
            if (gh == null)
            {
                San_pham san_pham = db.San_pham.Where(x => x.ID_Sanpham == id).SingleOrDefault();
                Giohang item = new Giohang();
                item.san_Pham = san_pham;
                item.soluong = 1;
                list.Add(item);
                Session["cartSession"] = list;
            }
            else
            {
                list = (List<Giohang>)Session["cartSession"];
                if(list.Exists(x => x.san_Pham.ID_Sanpham == id))
                {
                    foreach (var a in list)
                    {
                        if (a.san_Pham.ID_Sanpham == id) 
                        { 
                            a.soluong = a.soluong + 1;
                        }
                    }
                    Session["cartSession"] = list;
                }
                else
                {
                    San_pham san_pham = db.San_pham.Where(x => x.ID_Sanpham == id).SingleOrDefault();
                    Giohang item = new Giohang();
                    item.san_Pham = san_pham;
                    item.soluong = 1;
                    list.Add(item);
                    Session["cartSession"] = list;
                }

            }
            return RedirectToAction("Index", "Giohang");

        }
        public ActionResult capnhat(int id, int quantity)
        {
            List<Giohang> list =(List<Giohang>)Session["cartSession"];
            if(quantity != 0)
            {
                list.Where(p=> p.san_Pham.ID_Sanpham.Equals(id)).FirstOrDefault().soluong = quantity;
            }
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult Xoa(int id)
        {
            List<Giohang> list = (List<Giohang>)Session["cartSession"];
            Giohang gh = list.Where(p => p.san_Pham.ID_Sanpham.Equals(id)).FirstOrDefault();
            list.Remove(gh);
            Session["cartSession"] = list;
            return RedirectToAction("Index", "Giohang");

        }
        public ActionResult dathang()
        {
            if (Session["Nguoidung"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Don_hang dh = new Don_hang();
                dh.ID_Donhang = db.Don_hang.OrderByDescending(p => p.ID_Donhang).First().ID_Donhang + 1;
                dh.Ngay_Dat_hang = DateTime.Now;
                dh.ID_Nguoidung = (int)Session["Nguoidung"];
                dh.Ten_Nguoidung = (string)Session["Tennguoidung"];
                dh.Email_Nguoidung = (string)Session["Emailnguoidung"];
                dh.Mota = (string)Session["Mota"];
                dh.Trangthai = 0;
                //dh.Tongtien = (decimal)Session["Tongtien"];
                db.Don_hang.Add(dh);
                db.SaveChanges();
                List<Giohang> ds = (List<Giohang>)Session["cartSession"];
                string message = "";
                float sum = 0;
                foreach (Giohang item in ds)
                {
                    Chi_tiet_don_hang ctdh = new Chi_tiet_don_hang();
                    ctdh.ID_Donhang = dh.ID_Donhang;
                    ctdh.ID_Sanpham = item.san_Pham.ID_Sanpham;
                    ctdh.soluong = item.soluong;
                    ctdh.Gia = item.san_Pham.Gia;
                    db.Chi_tiet_don_hang.Add(ctdh);
                    db.SaveChanges();
                    message += "<br/> ID Sản Phẩm: " + item.san_Pham.ID_Sanpham;
                    message += "<br/> Tên sản phẩm: " + item.san_Pham.Tensanpham;
                    message += "<br/> Số lượng: " + item.soluong;
                    message += "<br/> Giá: " + item.san_Pham.Gia;
                    message += "<br/> Tông tiền: " + string.Format("{0:0,0 VNĐ}", item.soluong * item.san_Pham.Gia);
                    sum += (float)(item.soluong * item.san_Pham.Gia);
                }
                Session["Tongtien"] = sum;
                message += "<br>  Thành tiền:" + string.Format("{0:0,0 VNĐ}", sum);
                Nguoi_dung nd = db.Nguoi_dung.Find(dh.ID_Nguoidung);
                SendMail(nd.Email, "Đơn hàng vừa đặt từ www.ttn_shop.com", message);
                Session["cartSession"] = null;
                return RedirectToAction("Index", "TTN_Shop");
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
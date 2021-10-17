using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;

namespace ShopGiay.Controllers
{
    public class TTN_ShopController : Controller
    {
        public ShopBanGiayEntities1 db = new ShopBanGiayEntities1();
        // GET: TTN_Shop
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Product()
        {

            return View();
        }
        public ActionResult Details(int id)
        {
            
            return View();
        }
    }
}
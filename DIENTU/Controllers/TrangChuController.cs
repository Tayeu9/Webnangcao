using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIENTU.Models;

namespace DIENTU.Controllers
{
    public class TrangChuController : Controller
    {
        private DB_HVTShopEntities db = new DB_HVTShopEntities();
        // GET: TrangChu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
              
            }
            ViewBag.SL = 1;
            return View(sanPham);
        }
    }
}
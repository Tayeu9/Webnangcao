using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SHOPHVT.Controllers
{
    public class HomeController : Controller
    {
        private DB_HVTShopEntities _db = new DB_HVTShopEntities();
       
        private readonly UserManager<IdentityUser> _userManager;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(KhachHang khachhang)
        {
            if (khachhang.hoTen != null && khachhang.ngaySinh != null && khachhang.gioiTinh != null && khachhang.email != null && khachhang.diaChi != null && khachhang.tenDangNhap != null && khachhang.matKhau != null)
            {
                
                _db.KhachHangs.Add(khachhang);
                _db.SaveChanges();
                TempData["ErrorMessage"] = "Đăng kí thành công mời bạn đăng nhập";
                return RedirectToAction("Dangnhap");
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu thiếu mời nhập lại";
                return View();
            }    
        }
        public ActionResult Dangnhap()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangnhap(KhachHang khachhang)
        {
          
            var emailF = khachhang.email;
            var matkhauF = khachhang.matKhau;
            var check = _db.KhachHangs.SingleOrDefault(x => x.email.Equals(emailF) && x.matKhau.Equals(matkhauF));
            if (check != null)
            {
                Session["KhachHang"] = check;
                Session["email"] = khachhang.email;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View();
            }

        }
        public ActionResult Dangxuat()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
    
}
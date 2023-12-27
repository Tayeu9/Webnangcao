using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOPHVT.Models;

namespace SHOPHVT.Controllers
{
    public class TrangChuController : Controller
    {
        private DB_HVTShopEntities _db = new DB_HVTShopEntities();
        // GET: TrangChu
        public ActionResult Index()
        {
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
                Session["KhachHangID"] = check.khachHangID;
                return RedirectToAction("Index", "TrangChu");
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
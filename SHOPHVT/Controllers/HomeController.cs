using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SHOPHVT.Models;

namespace SHOPHVT.Controllers
{
    public class DetailViewModel
    {
        public SanPham DetailSanPham { get; set; }
        public List<SanPham> AllSanPham { get; set; }
    }

    public class HomeController : Controller
    {
        private DB_HVTShopEntities _db = new DB_HVTShopEntities();
       
        private readonly UserManager<IdentityUser> _userManager;

        public ActionResult Index(int? categoryId)
        {
            // Lấy danh sách danh mục
            var categories = _db.DanhMucs.ToList();

            if (HttpContext.Request.HttpMethod == "POST")
            {
                return RedirectToAction("Index", new { categoryId });
            }

            // Lấy danh sách sản phẩm theo danh mục được chọn
            List<Product> products;
            if (categoryId == null || categoryId == 0)
            {
                // Gọi stored procedure ProcGetSanPham
                products = _db.Database.SqlQuery<Product>("EXEC ProcGetSanPham @sanPhamID=NULL").ToList();

                // Mặc định chọn danh mục "All" khi trang được load
                ViewBag.SelectedCategory = null;
            }
            else
            {
                // Gọi stored procedure ProcGetSanPhamDanhMuc
                var parameter = new SqlParameter("@danhMucID", categoryId);
                products = _db.Database.SqlQuery<Product>("EXEC ProcGetSanPhamDanhMuc @danhMucID", parameter).ToList();

                // Thiết lập giá trị của danh mục được chọn
                ViewBag.SelectedCategory = categoryId;
            }

            // Truyền danh sách danh mục và sản phẩm đến view
            ViewBag.Categories = categories;
            return View(products);
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

        // GET: Detail
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SanPham sanPham = _db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            List<SanPham> allSanPham = _db.SanPhams.ToList();

            var viewModel = new DetailViewModel
            {
                DetailSanPham = sanPham,
                AllSanPham = allSanPham
            };

            return View(viewModel);
        }
    }
    
}
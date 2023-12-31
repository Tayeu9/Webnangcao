﻿using SHOPHVT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOPHVT.Views
{
    public class ProductController : Controller
    {
        private DB_HVTShopEntities db = new DB_HVTShopEntities();

        // GET: Product
        public ActionResult Index(int? categoryId)
        {
            // Lấy danh sách danh mục
            var categories = db.DanhMucs.ToList();

            if (HttpContext.Request.HttpMethod == "POST")
            {
                return RedirectToAction("Index", new { categoryId });
            }

            // Lấy danh sách sản phẩm theo danh mục được chọn
            List<Product> products;
            if (categoryId == null || categoryId == 0)
            {
                // Gọi stored procedure ProcGetSanPham
                products = db.Database.SqlQuery<Product>("EXEC ProcGetSanPham @sanPhamID=NULL").ToList();

                // Mặc định chọn danh mục "All" khi trang được load
                ViewBag.SelectedCategory = null;
            }
            else
            {
                // Gọi stored procedure ProcGetSanPhamDanhMuc
                var parameter = new SqlParameter("@danhMucID", categoryId);
                products = db.Database.SqlQuery<Product>("EXEC ProcGetSanPhamDanhMuc @danhMucID", parameter).ToList();

                // Thiết lập giá trị của danh mục được chọn
                ViewBag.SelectedCategory = categoryId;
            }

            // Truyền danh sách danh mục và sản phẩm đến view
            ViewBag.Categories = categories;
            return View(products);
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
            return View(sanPham);
        }
    }
}
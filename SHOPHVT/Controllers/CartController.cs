using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOPHVT.Models;

namespace SHOPHVT.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DB_HVTShopEntities db = new DB_HVTShopEntities();
        private string CartSession = "CartSession";
        private string CartItemCount = "CartItemCount";
        public ActionResult AddItem(int productId, int quantity)
        {
            var Sanpham = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.sanpham.sanPhamID == productId)) 
                {
                    foreach (var item in list)
                    {
                        if (item.sanpham.sanPhamID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }

                }
                else
                {
                    //tạo mới đối tượng CartItem
                    var item = new CartItem();
                    item.sanpham = Sanpham;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
                Session[CartItemCount] = list.Count;
            }
            else
            {
                //tạo mới đối tượng CartItem
                var item = new CartItem();
                item.sanpham = Sanpham;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
                Session[CartItemCount] = list.Count;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            ViewBag.Tongtien = Tongtien(); 
            return View(list) ;
        }
        public ActionResult Xoagiohang(int productId)
        {
            var Sanpham = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            List<CartItem> list = cart as List<CartItem>;
            if (list == null)
            {
                list = new List<CartItem>();
                cart = list;
            }
            CartItem sanpham = list.SingleOrDefault(n => n.sanpham.sanPhamID == productId);
            if (sanpham != null)
            {
                list.RemoveAll(n => n.sanpham.sanPhamID == productId);
                Session[CartItemCount] = list.Count;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        public ActionResult CapnhapGiohang(int productId, FormCollection f)
        {
            var cart = Session[CartSession];
            List<CartItem> list = cart as List<CartItem>;
            if (list == null)
            {
                list = new List<CartItem>();
                cart = list;
            }
            CartItem sanpham = list.SingleOrDefault(n => n.sanpham.sanPhamID == productId);
            if (sanpham != null)
            {
                sanpham.Quantity = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Index");
        }
        public double Tongtien()
        {
            double itongtien = 0;
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                itongtien = (double)list.Sum(n => n.Quantity * n.sanpham.donGia);
            }
            return itongtien;
        }
        public ActionResult Paycheck()
        {
            string email = (string)Session["email"];
            var user = db.KhachHangs.FirstOrDefault(u => u.email == email);

            if (user != null)
            {
                ViewBag.CustomerName = user.hoTen;
                ViewBag.CustomerPhoneNumber = user.soDienThoai;
                ViewBag.CustomerEmail = user.email;
                ViewBag.CustomerAddress = user.diaChi;
            }
            ViewBag.Tongtien = Tongtien();
            return View();
        }
    }
}
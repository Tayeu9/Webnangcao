using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIENTU.Models;

namespace DIENTU.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DB_HVTShopEntities db = new DB_HVTShopEntities();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGioHang = Session["Giohang"] as List<Giohang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<Giohang>();
                Session["Giohang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGiohang(int isanphamID, string strURL)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.sanPhamID == isanphamID);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<Giohang> lstGioHang = Laygiohang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            Giohang sanpham = lstGioHang.Find(n => n.IsanphamID == isanphamID);
            if(sanpham == null)
            {
                sanpham = new Giohang(isanphamID);
                //Add sản phẩm mới thêm vào list
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.ssoLuong++;
                return Redirect(strURL);
            }
        }
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //Kiểm tra masp
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.sanPhamID == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<Giohang> lstGioHang = Laygiohang();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            Giohang sanpham = lstGioHang.SingleOrDefault(n => n.IsanphamID == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.ssoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("Index");
        }
        public int Tongsoluong()
        {
            int iTongsoluong = 0;
            List<Giohang> lstGiohang = Session["Index"] as List<Giohang>;
            if(lstGiohang != null)
            {
                iTongsoluong = lstGiohang.Sum(n => n.ssoLuong);
            }
            return iTongsoluong;
        }
        private int Tongtien()
        {
            int iTongtien = 0;
            List<Giohang> lstGiohang = Session["Index"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongtien = lstGiohang.Sum(n => n.ThanHTien);
            }
            return iTongtien;
        }
        public ActionResult XoaGioHang(int iMaSP)
        {
            //Kiểm tra masp
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.sanPhamID == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<Giohang> lstGioHang = Laygiohang();
            Giohang sanpham = lstGioHang.SingleOrDefault(n => n.IsanphamID == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.IsanphamID == iMaSP);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang != null)
            {
                foreach (var item in lstGiohang)
                {
                    Debug.WriteLine("SanPhamID: " + item.IsanphamID); // In ra SanPhamID để kiểm tra
                }
            }
            else
            {
                Debug.WriteLine("lstGiohang is null."); // Nếu lstGiohang trả về null, in ra thông báo
            }

            //if (lstGiohang.Count == 0)
            //{
            //    return RedirectToAction("Index", "Trangchu");
            //}
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }
    }
}
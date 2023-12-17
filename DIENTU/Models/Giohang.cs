using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIENTU.Models
{
    public class Giohang
    {
        DB_HVTShopEntities db = new DB_HVTShopEntities();
        public int IsanphamID { set; get; }
        public String stenSanPham { set; get; }
        public Double sdonGia { set; get; }
        public int ssoLuong { set; get; }
        public String shinhAnh { set; get; }
        public int ThanHTien
        {
            get { return (int)(ssoLuong * sdonGia); }
        }
        public Giohang(int sanphamID)
        {
            IsanphamID = sanphamID;
            SanPham sp = db.SanPhams.Single(n => n.sanPhamID == IsanphamID);
            stenSanPham = sp.tenSanPham;
            sdonGia = Double.Parse(sp.donGia.ToString());
            ssoLuong = 1;
            shinhAnh = sp.hinhAnh;
        }

    }
}
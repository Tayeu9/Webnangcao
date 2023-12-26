using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHOPHVT.Models
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
    }
}
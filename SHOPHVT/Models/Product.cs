using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHOPHVT.Models
{
    public class Product
    {
        public int SanPhamID { get; set; }
        public string tenSanPham { get; set; }
        public decimal donGia { get; set; }
        public int soLuong { get; set; }
        public string hinhAnh { get; set; }
        public string moTa { get; set; }
        public int danhMucID { get; set; }
    }

}
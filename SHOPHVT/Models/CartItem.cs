using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations.Model;

namespace SHOPHVT.Models
{
    [Serializable]
    public class CartItem
    {
        public SanPham sanpham { set; get; }
        public int Quantity { set; get; }
    }
}
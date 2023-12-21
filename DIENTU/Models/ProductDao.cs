using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIENTU.Models
{
    public class ProductDao
    {
        DB_HVTShopEntities db = null;
        public ProductDao()
        {
            db = new DB_HVTShopEntities();
        }
        public SanPham ViewDetail(int id)
        {
            return db.SanPhams.Find(id);
        }
    }
}
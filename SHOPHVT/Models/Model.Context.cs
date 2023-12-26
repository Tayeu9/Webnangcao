﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SHOPHVT.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB_HVTShopEntities : DbContext
    {
        public DB_HVTShopEntities()
            : base("name=DB_HVTShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<QuanTriVien> QuanTriViens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
    
        public virtual int ProcDeleteSanPham(Nullable<int> sanPhamID)
        {
            var sanPhamIDParameter = sanPhamID.HasValue ?
                new ObjectParameter("sanPhamID", sanPhamID) :
                new ObjectParameter("sanPhamID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ProcDeleteSanPham", sanPhamIDParameter);
        }
    
        public virtual ObjectResult<ProcGetDanhMuc_Result> ProcGetDanhMuc(Nullable<int> danhMucID)
        {
            var danhMucIDParameter = danhMucID.HasValue ?
                new ObjectParameter("danhMucID", danhMucID) :
                new ObjectParameter("danhMucID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcGetDanhMuc_Result>("ProcGetDanhMuc", danhMucIDParameter);
        }
    
        public virtual ObjectResult<ProcGetGioHang_Result> ProcGetGioHang(Nullable<int> khachHangID)
        {
            var khachHangIDParameter = khachHangID.HasValue ?
                new ObjectParameter("khachHangID", khachHangID) :
                new ObjectParameter("khachHangID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcGetGioHang_Result>("ProcGetGioHang", khachHangIDParameter);
        }
    
        public virtual ObjectResult<ProcGetNameAndNumSPOfDM_Result> ProcGetNameAndNumSPOfDM()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcGetNameAndNumSPOfDM_Result>("ProcGetNameAndNumSPOfDM");
        }
    
        public virtual ObjectResult<ProcGetNumMonth_Result> ProcGetNumMonth()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcGetNumMonth_Result>("ProcGetNumMonth");
        }
    
        public virtual ObjectResult<Nullable<int>> ProcGetRowCountDM()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ProcGetRowCountDM");
        }
    
        public virtual ObjectResult<Nullable<int>> ProcGetRowCountHD()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ProcGetRowCountHD");
        }
    
        public virtual ObjectResult<Nullable<int>> ProcGetRowCountHH()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ProcGetRowCountHH");
        }
    
        public virtual ObjectResult<Nullable<int>> ProcGetRowCountSP()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ProcGetRowCountSP");
        }
    
        public virtual ObjectResult<ProcGetSanPham_Result> ProcGetSanPham(Nullable<int> sanPhamID)
        {
            var sanPhamIDParameter = sanPhamID.HasValue ?
                new ObjectParameter("sanPhamID", sanPhamID) :
                new ObjectParameter("sanPhamID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcGetSanPham_Result>("ProcGetSanPham", sanPhamIDParameter);
        }
    
        public virtual ObjectResult<ProcGetSanPhamDanhMuc_Result> ProcGetSanPhamDanhMuc(Nullable<int> danhMucID)
        {
            var danhMucIDParameter = danhMucID.HasValue ?
                new ObjectParameter("danhMucID", danhMucID) :
                new ObjectParameter("danhMucID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcGetSanPhamDanhMuc_Result>("ProcGetSanPhamDanhMuc", danhMucIDParameter);
        }
    
        public virtual int ProcInsertSanPham(string tenSanPham, Nullable<decimal> donGia, Nullable<int> soLuong, string hinhAnh, string moTa, Nullable<int> danhMucID)
        {
            var tenSanPhamParameter = tenSanPham != null ?
                new ObjectParameter("tenSanPham", tenSanPham) :
                new ObjectParameter("tenSanPham", typeof(string));
    
            var donGiaParameter = donGia.HasValue ?
                new ObjectParameter("donGia", donGia) :
                new ObjectParameter("donGia", typeof(decimal));
    
            var soLuongParameter = soLuong.HasValue ?
                new ObjectParameter("soLuong", soLuong) :
                new ObjectParameter("soLuong", typeof(int));
    
            var hinhAnhParameter = hinhAnh != null ?
                new ObjectParameter("hinhAnh", hinhAnh) :
                new ObjectParameter("hinhAnh", typeof(string));
    
            var moTaParameter = moTa != null ?
                new ObjectParameter("moTa", moTa) :
                new ObjectParameter("moTa", typeof(string));
    
            var danhMucIDParameter = danhMucID.HasValue ?
                new ObjectParameter("danhMucID", danhMucID) :
                new ObjectParameter("danhMucID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ProcInsertSanPham", tenSanPhamParameter, donGiaParameter, soLuongParameter, hinhAnhParameter, moTaParameter, danhMucIDParameter);
        }
    
        public virtual int ProcUpdateSanPham(Nullable<int> sanPhamID, string tenSanPham, Nullable<decimal> donGia, Nullable<int> soLuong, string hinhAnh, string moTa, Nullable<int> danhMucID)
        {
            var sanPhamIDParameter = sanPhamID.HasValue ?
                new ObjectParameter("sanPhamID", sanPhamID) :
                new ObjectParameter("sanPhamID", typeof(int));
    
            var tenSanPhamParameter = tenSanPham != null ?
                new ObjectParameter("tenSanPham", tenSanPham) :
                new ObjectParameter("tenSanPham", typeof(string));
    
            var donGiaParameter = donGia.HasValue ?
                new ObjectParameter("donGia", donGia) :
                new ObjectParameter("donGia", typeof(decimal));
    
            var soLuongParameter = soLuong.HasValue ?
                new ObjectParameter("soLuong", soLuong) :
                new ObjectParameter("soLuong", typeof(int));
    
            var hinhAnhParameter = hinhAnh != null ?
                new ObjectParameter("hinhAnh", hinhAnh) :
                new ObjectParameter("hinhAnh", typeof(string));
    
            var moTaParameter = moTa != null ?
                new ObjectParameter("moTa", moTa) :
                new ObjectParameter("moTa", typeof(string));
    
            var danhMucIDParameter = danhMucID.HasValue ?
                new ObjectParameter("danhMucID", danhMucID) :
                new ObjectParameter("danhMucID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ProcUpdateSanPham", sanPhamIDParameter, tenSanPhamParameter, donGiaParameter, soLuongParameter, hinhAnhParameter, moTaParameter, danhMucIDParameter);
        }
    }
}

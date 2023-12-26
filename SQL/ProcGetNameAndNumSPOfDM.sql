USE DB_HVTShop
GO

CREATE PROC ProcGetNameAndNumSPOfDM
AS
BEGIN
    SELECT SanPham.danhMucID, DanhMuc.tenDanhMuc, SUM(SanPham.soLuong) AS totalQty
    FROM dbo.DanhMuc
    JOIN dbo.SanPham ON DanhMuc.danhMucID = SanPham.danhMucID
    GROUP BY SanPham.danhMucID, DanhMuc.tenDanhMuc;
END
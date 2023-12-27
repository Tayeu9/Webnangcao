-- USE master
-- GO
-- DROP DATABASE DB_HVTShop
-- GO

CREATE DATABASE DB_HVTShop
GO
USE DB_HVTShop
GO

-- Create table

CREATE TABLE QuanTriVien
(
	taiKhoanID INT IDENTITY(1, 1) PRIMARY KEY,
	tenDangNhap VARCHAR(50) NOT NULL,
	matKhau NVARCHAR(50) NOT NULL,
)

CREATE TABLE KhachHang
(
	khachHangID INT IDENTITY(1, 1) PRIMARY KEY,
	hoTen NVARCHAR(50),
	ngaySinh DATE,
	gioiTinh NVARCHAR(10),
	soDienThoai VARCHAR(10),
	email VARCHAR(50),
	diaChi NVARCHAR(200),
	tenDangNhap VARCHAR(50) NOT NULL,
	matKhau VARCHAR(50) NOT NULL,
)

CREATE TABLE DanhMuc
(
	danhMucID INT IDENTITY(1, 1) PRIMARY KEY,
	tenDanhMuc NVARCHAR(50) NOT NULL,
	moTa NVARCHAR(200),
)
GO

CREATE TABLE SanPham
(
	sanPhamID INT IDENTITY(1, 1) PRIMARY KEY,
	tenSanPham NVARCHAR(50) NOT NULL,
	donGia NUMERIC(18, 0) NOT NULL,
	soLuong INT NOT NULL,
	hinhAnh VARCHAR(50),
	moTa NVARCHAR(200),
	danhMucID INT

	FOREIGN KEY (danhMucID) REFERENCES DanhMuc(danhMucID)
	on update
		cascade
	on delete
		cascade
)
GO

CREATE TABLE GioHang
(
	gioHangID INT IDENTITY(1, 1) PRIMARY KEY,
	khachHangID INT NOT NULL,
	sanphamID INT NOT NULL,
	soLuong INT
	
	FOREIGN KEY (khachHangID) REFERENCES KhachHang(khachHangID)
	on update
		cascade
	on delete
		cascade,
	FOREIGN KEY (sanphamID) REFERENCES SanPham(sanPhamID)
	on update
		cascade
	on delete
		cascade,
)
GO

CREATE TABLE HoaDon
(
	hoaDonID INT IDENTITY(1, 1) PRIMARY KEY,
	ngayTao DATE NOT NULL DEFAULT GETDATE(),
	khachHangID INT NOT NULL
	
	FOREIGN KEY (khachHangID) REFERENCES KhachHang(khachHangID)
	on update
		cascade
	on delete
		cascade
)
GO

CREATE TABLE ChiTietHoaDon
(
	chiTietHoaDonID INT IDENTITY(1, 1) PRIMARY KEY,
	hoaDonID INT NOT NULL,
	sanphamID INT NOT NULL,
	soLuong int NOT NULL,
	donGia NUMERIC(18, 0) NOT NULL

	FOREIGN KEY (hoaDonID) REFERENCES HoaDon(hoaDonID)
	on update
		cascade
	on delete
		cascade,
	FOREIGN KEY (sanphamID) REFERENCES SanPham(sanPhamID)
	on update
		cascade
	on delete
		cascade,
)
GO

-- Insert

set dateformat dmy

INSERT INTO QuanTriVien (tenDangNhap, matKhau)
VALUES
	('admin', 'admin')
	
INSERT INTO KhachHang (hoTen, ngaySinh, gioiTinh, soDienThoai, email, diaChi, tenDangNhap, matKhau)
VALUES
	(N'Nguyễn Văn A', '1/1/2003', N'Nam', '0381212123', 'vana@gmail.com', N'Hà Nội', 'vana', '1234'),
	(N'Nguyễn Thế Anh', '1/1/2003', N'Nam', '0381232323', 'theanh@gmail.com', N'Đà Nẵng', 'theanh', '1234'),
	(N'admin', '1/1/2003', N'Nam', '0381234153', 'admin@gmail.com', N'Đà Nẵng', 'admin', 'admin');
	
INSERT INTO DanhMuc (tenDanhMuc)
VALUES	
	(N'Điện thoại'),
	(N'Máy tính'),
	(N'Laptop'),
	(N'Ti vi');
	
INSERT INTO SanPham (tenSanPham, donGia, soLuong, hinhAnh, moTa, danhMucID)
VALUES
	(N'Laptop ASUS VOSTRO 3420 I5', 20000000, 20, 'product-1.jpg', 'Lorem ipsum dolor sit amet', 3),
	(N'Laptop ASUS F570ZD', 15000000, 3, 'product-2.jpg', 'Lorem ipsum dolor sit amet', 3),
	(N'Laptop THINKPAD L14', 14000000, 10, 'product-3.jpg', 'Lorem ipsum dolor sit amet', 3),
	(N'Oppo G38', 4190000, 16, 'product-4.jpg', 'Lorem ipsum dolor sit amet', 1),
	(N'Galaxy A05s', 3990000, 20, 'product-5.jpg', 'Lorem ipsum dolor sit amet', 1),
	(N'OLED LG 8k 88Z2PSA', 890000000, 10, 'product-6.jpg', 'Lorem ipsum dolor sit amet', 4),
	(N'BTS VIP 01', 29000000, 50, 'product-7.jpg', 'Lorem ipsum dolor sit amet', 2),
	(N'PC Sharkoon 01 ', 19990000, 50, 'product-8.jpg', 'Lorem ipsum dolor sit amet', 2);


-- Procedures --

-- Lấy danh mục
GO
CREATE PROC ProcGetDanhMuc (@danhMucID INT)
AS
BEGIN TRANSACTION
    IF (@danhMucID IS NULL)
        SELECT * FROM DanhMuc
    ELSE
        SELECT * FROM DanhMuc WHERE danhMucID = @danhMucID
IF (@@ERROR <> 0)
    ROLLBACK TRANSACTION
ELSE
    COMMIT TRANSACTION

-- Lấy sản phẩm
GO
CREATE PROC ProcGetSanPham (@sanPhamID INT)
AS
BEGIN TRANSACTION
    IF (@sanPhamID IS NULL)
        SELECT * FROM SanPham
    ELSE
        SELECT * FROM SanPham WHERE sanPhamID = @sanPhamID
IF (@@ERROR <> 0)
    ROLLBACK TRANSACTION
ELSE
    COMMIT TRANSACTION
	
-- Lấy sản phẩm với mã danh mục
GO
CREATE PROC ProcGetSanPhamDanhMuc (@danhMucID INT)
AS
BEGIN TRANSACTION
	SELECT * FROM SanPham WHERE danhMucID = @danhMucID
IF (@@ERROR <> 0)
    ROLLBACK TRANSACTION
ELSE
    COMMIT TRANSACTION

-- Lấy giỏ hàng
GO
CREATE PROC ProcGetGioHang (@khachHangID INT)
AS
BEGIN TRANSACTION
    SELECT * FROM GioHang
	WHERE khachHangID = @khachHangID
IF (@@ERROR <> 0)
    ROLLBACK TRANSACTION
ELSE
    COMMIT TRANSACTION
	
-- Thêm sản phẩm
GO
CREATE PROC ProcInsertSanPham (
	@tenSanPham NVARCHAR(50),
	@donGia NUMERIC(18, 0), 
	@soLuong INT,
	@hinhAnh VARCHAR(50),
	@moTa NVARCHAR(200),
	@danhMucID INT
)
AS
BEGIN TRANSACTION
    INSERT INTO SanPham (tenSanPham, donGia, soLuong, hinhAnh, moTa, danhMucID)
	VALUES (@tenSanPham, @donGia, @soLuong, @hinhAnh, @moTa, @danhMucID)
IF (@@ERROR <> 0)
    ROLLBACK TRANSACTION
ELSE
    COMMIT TRANSACTION
	
-- Cập nhật sản phẩm
GO
CREATE PROC ProcUpdateSanPham (
	@sanPhamID INT,
	@tenSanPham NVARCHAR(50),
	@donGia NUMERIC(18, 0), 
	@soLuong INT,
	@hinhAnh VARCHAR(50),
	@moTa NVARCHAR(200),
	@danhMucID INT
)
AS
BEGIN TRANSACTION
    UPDATE SanPham
	SET tenSanPham = @tenSanPham, donGia = @donGia, soLuong = @soLuong, 
		hinhAnh = @hinhAnh, moTa = @moTa, danhMucID = @danhMucID
	WHERE sanPhamID = @sanPhamID
IF (@@ERROR <> 0)
    ROLLBACK TRANSACTION
ELSE
    COMMIT TRANSACTION
	
-- Xóa sản phẩm
GO
CREATE PROC ProcDeleteSanPham (@sanPhamID INT)
AS
BEGIN TRANSACTION
    DELETE FROM SanPham WHERE sanPhamID = @sanPhamID
IF (@@ERROR <> 0)
    ROLLBACK TRANSACTION
ELSE
    COMMIT TRANSACTION

-- Đổi thông tin
GO
CREATE PROC ProcUpdateKhachHang
(
	@khachHangID INT, 
	@hoTen NVARCHAR(50), 
	@ngaySinh DATE, 
	@gioiTinh NVARCHAR(10),
	@soDienThoai NVARCHAR(10),
	@email VARCHAR(50),
	@diaChi NVARCHAR(200),
	@matKhau VARCHAR(50)
)
AS
BEGIN TRANSACTION
    UPDATE KhachHang
	SET hoTen = @hoTen, ngaySinh = @ngaySinh, gioiTinh = @gioiTinh, soDienThoai = @soDienThoai, email = @email, diaChi = @diaChi, matKhau = @matKhau
	WHERE khachHangID = @khachHangID
IF (@@ERROR <> 0)
    ROLLBACK TRANSACTION
ELSE
    COMMIT TRANSACTION

-- Đổi mật khẩu
GO
CREATE PROC ProcUpdateKhachHangMatKhau (@khachHangID INT, @matKhau VARCHAR(50))
AS
BEGIN TRANSACTION
    UPDATE KhachHang
	SET matKhau = @matKhau
	WHERE khachHangID = @khachHangID
IF (@@ERROR <> 0)
    ROLLBACK TRANSACTION
ELSE
    COMMIT TRANSACTION

-- Dashboard
GO
CREATE PROC ProcGetNameAndNumSPOfDM
AS
BEGIN
    SELECT SanPham.danhMucID, DanhMuc.tenDanhMuc, SUM(SanPham.soLuong) AS totalQty
    FROM dbo.DanhMuc
    JOIN dbo.SanPham ON DanhMuc.danhMucID = SanPham.danhMucID
    GROUP BY SanPham.danhMucID, DanhMuc.tenDanhMuc;
END

--
GO
CREATE PROC ProcGetNumMonth
AS
BEGIN
    SELECT MONTH(HoaDon.ngayTao) as month_num, FORMAT(HoaDon.ngayTao, 'MMM') as month_str, SUM(HoaDon.khachHangID) as sum_month
	FROM dbo.HoaDon
	GROUP BY FORMAT(HoaDon.ngayTao, 'MMM'), MONTH(HoaDon.ngayTao)
	ORDER BY MONTH(HoaDon.ngayTao)
END

--
GO
CREATE PROC ProcGetRowCountDM
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.DanhMuc
END

--
GO
CREATE PROC ProcGetRowCountHD
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.HoaDon
END

--
GO
CREATE PROC ProcGetRowCountHH
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.SanPham
	WHERE soLuong <= 5 
END

--
GO
CREATE PROC ProcGetRowCountSP
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.SanPham
END

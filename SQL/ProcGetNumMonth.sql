USE DB_HVTShop
GO

CREATE PROC ProcGetNumMonth
AS
BEGIN
    SELECT MONTH(HoaDon.ngayTao) as month_num, FORMAT(HoaDon.ngayTao, 'MMM') as month_str, SUM(HoaDon.khachHangID) as sum_month
	FROM dbo.HoaDon
	GROUP BY FORMAT(HoaDon.ngayTao, 'MMM'), MONTH(HoaDon.ngayTao)
	ORDER BY MONTH(HoaDon.ngayTao)
END
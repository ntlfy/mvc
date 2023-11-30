CREATE DATABASE Project_63132244
USE Project_63132244
GO
-- TẠO CÁC BẢNG
CREATE TABLE Sanpham (
  Ma_SP varchar(50) PRIMARY KEY NOT NULL,
  Loai_SP nvarchar(50) NOT NULL,
  Ten_SP nvarchar(50) NOT NULL,
  XuatXu bit NOT NULL,
  DonGia decimal(18,2) NOT NULL,
  KhoiLuong decimal(18,2)  NULL,
  MoTa ntext NULL, 
  AnhSP nvarchar(50)  NULL
)
GO
DROP TABLE KhoHang;
CREATE TABLE KhachHang (
  MaKhachHang varchar(20) PRIMARY KEY NOT NULL,
  Password varchar(50) NOT NULL, 
  HoTen nvarchar(50) NOT NULL,
  DienThoai char(10) NOT NULL,
  Email varchar(20) NULL,
  DiaChi nvarchar(100) NOT NULL,
  Avatar nvarchar(100) NULL 
)
GO
CREATE TABLE ChiTietHoaDon (
  Ma_HoaDon varchar(20) NOT NULL,
  Ma_SP varchar(50) NOT NULL,
  KhoiLuong decimal(18,2) NOT NULL,
  DonGia decimal(18,2) NOT NULL,
  PRIMARY KEY (Ma_HoaDon,Ma_SP)
)

CREATE TABLE HoaDon(
  Ma_HoaDon varchar(20) PRIMARY KEY,
  MaKhachHang varchar(20) NOT NULL, 
  TrangThai_VanChuyen bit NOT NULL,
  TrangThai_ThanhToan bit NOT NULL,
  PhuongThuc_ThanhToan bit NOT NULL,
  NgayDat datetime NOT NULL,
  NgayGiao datetime NOT NULL,
  DiaChi nvarchar(100) NOT NULL,
)
ALTER TABLE HoaDon 
ALTER COLUMN NgayGiao DATETIME NULL
GO
CREATE TABLE QuanTri (
  TaiKhoan varchar(20) PRIMARY KEY NOT NULL,
  Password varchar(50) NOT NULL,
  HoTen nvarchar(50) NOT NULL,
  DienThoai char(10) NOT NULL,
  Avatar nvarchar(100) NULL,
  admin bit
)
CREATE TABLE QUESTION(
  IDQuestion int identity(1 ,1) PRIMARY KEY ,
  HoTen nvarchar(50) NOT NULL,
  Email varchar(20) NOT NULL,
  Phone varchar(10) NOT NULL,
  Subject nvarchar(50) NOT NULL,
  Messenge ntext NOT NULL
)

ALTER TABLE QUESTION 
ALTER COLUMN Messenge nvarchar(200) NOT NULL

ALTER TABLE QUESTION
ADD Status bit DEFAULT 0;
DROP TABLE QUESTION
GO
--Khóa Ngoại
ALTER TABLE HoaDon 
ADD CONSTRAINT FK_HoaDon_KhachHang
FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
	ON UPDATE CASCADE
	ON DELETE CASCADE
ALTER TABLE ChiTietHoaDon
ADD CONSTRAINT FK_CTHD_HoaDon
FOREIGN KEY (Ma_HoaDon) REFERENCES HoaDon(Ma_HoaDon)
	ON UPDATE CASCADE
	ON DELETE CASCADE

ALTER TABLE ChiTietHoaDon
ADD CONSTRAINT FK_CTHD_SanPham  
FOREIGN KEY (Ma_SP) REFERENCES SanPham(Ma_SP) 
	ON UPDATE CASCADE
	ON DELETE CASCADE


GO
--INSERT Data
INSERT INTO KhoHang (Ma_SP, TinhTrang)
VALUES
  ('buoi-nam-roi', 1),
  ('cam-sanh', 1),
  ('quyt-uc-2ph', 0),
  ('nho-uc-khong-hat', 0),
  ('dua-luoi-gion', 0),
  ('tao-my-huu-co', 0),
  ('nho-my-khong-hat', 0),
  ('nho-uc-crimson', 0),
  ('tao-envy', 0),
  ('le-han-quoc', 0),
  ('dua-fuji-nhat-ban', 0),
  ('mang-cut', 1),
  ('qua-mang-cau-na', 1),
  ('xoai-cat-hoai-loc', 1),
  ('chom-chom-nhan', 1),
  ('sau-rieng-ri6', 1),
  ('dua-hau-khong-hat', 1),
  ('thanh-long-ruot-do', 1),
  ('cam-cara-uc', 0),
  ('xoai-tu-quy-loai-1', 1),
  ('le-kousui', 0),
  ('thanh-long-ruot-trang', 1),
  ('chuoi-dole', 0),
  ('chom-chom-thai-lan', 1),
  ('thom-mini-thailand', 0),
  ('buoi-duong-la', 1),
  ('sau-rieng-ri6-loai-2', 0),
  ('bo-pin-uc', 1),
  ('xoai-cat-chu-xanh', 0),
  ('sau-rieng-dona', 1);

INSERT INTO SANPHAM (Ma_SP, Loai_SP, Ten_SP, XuatXu, DonGia, MoTa, AnhSP)
VALUES
  ('buoi-nam-roi', N'Bưởi', N'Bưởi Năm Roi', 1, 50000, NULL, 'anh1.jpg'),
  ('cam-sanh', N'Cam', N'Cam Sành', 1, 60000, NULL, 'anh2.jpg'),
  ('quyt-uc-2ph', N'Quýt', N'Quýt Úc 2PH Ngọt Lịm', 0, 55000, NULL, 'anh3.jpg'),
  ('nho-uc-khong-hat', N' Nho ', N'NHO XANH KHÔNG HẠT ÚC GIÒN RỤM', 0, 45000, NULL, 'anh4.jpg'),
  ('dua-luoi-gion', N'Dưa', N'Dưa Lưới Đài Loàn Giòn rụm ngọt lịm', 0, 70000, NULL, 'anh5.jpg'),
  ('tao-my-huu-co', N'Táo ', N'Táo Juliet Organic Pháp dòng táo hữu cơ', 0, 30000, NULL, 'anh6.jpg'),
  ('nho-my-khong-hat', N'Nho ', N'NHO XANH KHÔNG HẠT MỸ GIÒN RỤM', 0, 48000, NULL, 'anh7.jpg'),
  ('nho-uc-crimson', N'Nho', N'Nho Đỏ Không Hạt Úc Crimson', 0, 65000, NULL, 'anh8.jpg'),
  ('tao-envy', N'Táo', N'Táo Envy New Zealand Size Nhỏ', 0, 55000, NULL, 'anh9.jpg'),
  ('le-han-quoc', N'Lê', N'Lê Hàn Quốc chính hiệu', 0, 75000, NULL, 'anh10.jpg'),
  ('dua-fuji-nhat-ban', N'Dưa', N'Dưa FUji Nhật Bản Organic', 0, 60000, NULL, 'anh11.jpg'),
  ('mang-cut', N'Măng Cụt', N'Quả Măng Cụt', 1, 62000, NULL, 'anh12.jpg'),
  ('qua-mang-cau-na', N'Mãng Cầu', N'Quả Mãng Cầu Na', 1, 52000, NULL, 'anh13.jpg'),
  ('xoai-cat-hoai-loc', N'Xoài', N'Xoài Cát Hòa Lộc', 1, 58000, NULL, 'anh14.jpg'),
  ('chom-chom-nhan', N'Chôm chôm', N'Chôm Chôm Nhãn', 1, 69000, NULL, 'anh15.jpg'),
  ('sau-rieng-ri6', N'Sầu Riêng', N'Sầu Riêng Ri6', 1, 45000, NULL, 'anh16.jpg'),
  ('dua-hau-khong-hat', N'Dưa', N'Dưa hấu đỏ không hạt', 1, 52000, NULL, 'anh17.jpg'),
  ('thanh-long-ruot-do', N'Thanh Long', N'Thanh long ruột đỏ', 1, 58000, NULL, 'anh18.jpg'),
  ('cam-cara-uc', N'Cam', N'Cam Cara Úc', 0, 63000, NULL, 'anh19.jpg'),
  ('xoai-tu-quy-loai-1', N'Xoài', N'Xoài Tứ Quý loại 1', 1, 67000, NULL, 'anh20.jpg'),
  ('le-kousui', N'Lê', N'Lê Kousui Nhật Bản', 0, 49000, NULL, 'anh21.jpg'),
  ('thanh-long-ruot-trang', N'Thanh Long', N'Thanh long ruột trắng', 1, 53000, NULL, 'anh22.jpg'),
  ('chuoi-dole', N'Chuối', N'Chuối Dole', 0, 56000, NULL, 'anh23.jpg'),
  ('chom-chom-thai-lan', N'Chôm chôm', N'Chôm Chôm giống Thái Lan', 1, 59000, NULL, 'anh24.jpg'),
  ('thom-mini-thailand', N'Thơm', N'Thơm mini Thái Lan', 0, 47000, NULL, 'anh25.jpg'),
  ('buoi-duong-la', N'Bưởi', N'Bưởi đường lá cam Tân Triều', 1, 51000, NULL, 'anh26.jpg'),
  ('sau-rieng-ri6-loai-2', N'Sầu Riêng', N'Sầu riêng Ri6 loại 2', 0, 54000, NULL, 'anh27.jpg'),
  ('bo-pin-uc', N'Bơ', N'Bơ Pin giống Úc', 1, 58000, NULL, 'anh28.jpg'),
  ('xoai-cat-chu-xanh', N'Xoài', N'Xoài cát chu xanh', 0, 62000, NULL, 'anh29.jpg'),
  ('sau-rieng-dona', N'Sầu Riêng', N'Sầu riêng cơm Dona', 1, 65000, NULL, 'anh30.jpg');
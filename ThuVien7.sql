
CREATE DATABASE QLTV
USE QLTV
go
CREATE TABLE DocGia
(
	MaDocGia INT IDENTITY	 NOT NULL,
	TenDocGia NVARCHAR(30) NOT NULL,
	NgaySinh SMALLDATETIME,
	DiaChi NVARCHAR(70),
	SDT Nvarchar(12),
	Email NVARCHAR(30),
	NgayCap SMALLDATETIME,
	NgayHetHan SMALLDATETIME,
	TinhTrang int not null, -- 0-Bình thường, 1--Khóa
	PRIMARY KEY (MaDocGia)
)
go
CREATE TABLE TheLoai 
(
	MaTheLoai INT IDENTITY NOT NULL,
	TenTheLoai NVARCHAR(30),
	GioiHanMuon INT NOT NULL, --số ngày mượn tối đa 
	PRIMARY KEY (MaTheLoai)
)

go
CREATE TABLE TaiLieuS
(
	MaTaiLieuS INT IDENTITY NOT NULL,
	TenTaiLieu NVARCHAR(50) NOT NULL,
	MaTheLoai INT NOT NULL,
	NhaXuatBan NVARCHAR(50),
	NamXuatBan SMALLDATETIME,
	DonGia MONEY,
	SoLuong INT NOT NULL,
	PRIMARY KEY (MaTaiLieuS),
	CONSTRAINT FK_TaiLieuS_TheLoai FOREIGN KEY (MaTheLoai) REFERENCES TheLoai (MaTheLoai)
)
go
CREATE TABLE TaiLieu
(
	MaTaiLieu INT IDENTITY NOT NULL,
	MaTaiLieuS INT NOT NULL,
	TinhTrang NVARCHAR(10),
	PRIMARY KEY (MaTaiLieu),
	CONSTRAINT FK_TaiLieu_TaiLieuS FOREIGN KEY (MaTaiLieuS) REFERENCES TaiLieuS (MaTaiLieuS)
)
go
CREATE TABLE TacGia
(
	MaTacGia INT IDENTITY NOT NULL,
	TenTacGia NVARCHAR(30) NOT NULL,
	PRIMARY KEY (MaTacGia)
)
go
CREATE TABLE NhomTacGia
(
	MaTaiLieuS INT,
	MaTacGia INT,
	CONSTRAINT FK_NhomTacGia_TaiLieuS FOREIGN KEY (MaTaiLieuS) REFERENCES TaiLieuS (MaTaiLieuS),
	CONSTRAINT FK_NhomTacGia_TacGia FOREIGN KEY (MaTacGia) REFERENCES TacGia (MaTacGia)	
)
go

CREATE TABLE PhieuMuon
(
	MaPhieuMuon INT  NOT NULL,
	MaDocGia INT NOT NULL,	
	SoLuong INT,
	PRIMARY KEY (MaPhieuMuon),
	CONSTRAINT FK_PhieuMuon_DocGia FOREIGN KEY (MaDocGia) REFERENCES DocGia (MaDocGia)
)
go

CREATE TABLE CTPM
(
	MaPhieuMuon INT NOT NULL,
	MaTaiLieu INT NOT NULL,	
	NgayMuon SMALLDATETIME NOT NULL,
	NgayTra SMALLDATETIME NOT NULL,
	TinhTrang NVARCHAR(15) NOT NULL, --1: trả rồi 0: chưa trả
	CONSTRAINT FK_CTPM_PhieuMuon FOREIGN KEY (MaPhieuMuon) REFERENCES PhieuMuon (MaPhieuMuon),
	CONSTRAINT FK_CTPM_TaiLieu FOREIGN KEY (MaTaiLieu) REFERENCES TaiLieu (MaTaiLieu),
)
go

CREATE TABLE ViPham
(
	MaDocGia int not null,
	LoaiViPham int not null,
	CONSTRAINT FK_ViPham_DocGia FOREIGN KEY (MaDocGia) REFERENCES DocGia (MaDocGia)
)

go
CREATE TABLE Account
(
	ID INT IDENTITY NOT NULL PRIMARY KEY,
	DisplayName NVARCHAR(50),
	UserName NVARCHAR(50) NOT NULL,
	PASSWORD NVARCHAR(50) NOT NULL,
	Type INT NOT NULL,
)

go
--kiểm tra tài khoản và mật khẩu
go
Create PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
as
begin
	Select * from Account Where UserName=@userName AND PassWord = @passWord
end  --kiểm tra đăng nhập

go

create proc USP_InsertDocGia
@TenDocGia NVARCHAR(50),
@NgaySinh Smalldatetime,
@DiaChi NVARCHAR(70),
@SDT NVARCHAR(12),
@Email NVARCHAR(30)
AS
BEGIN
	Insert INTO DocGia
		(TenDocGia,
		NgaySinh,
		DiaChi,
		SDT,
		Email,
		NgayCap,
		NgayHetHan,
		TinhTrang
		)
Values (@TenDocGia,
		@NgaySinh,
		@DiaChi,
		@SDT,
		@Email,
		GetDate(),
		DATEADD(MONTH,6,GETDATE()),
		0)
	
END


end


 --thêm độc giả mới 
go

create proc USP_UpdateDocGia --sửa thông tin độc giả
@ID INT,
@TenDocGia NVARCHAR(50),
@NgaySinh Smalldatetime,
@DiaChi NVARCHAR(70),
@SDT NVARCHAR(12),
@Email NVARCHAR(30)
AS
BEGIN
	Update DocGia
		set TenDocGia = @TenDocGia,
		NgaySinh = @NgaySinh,
		DiaChi = @DiaChi,
		SDT = @SDT,
		Email = @Email	
		where MaDocGia=@ID
END



go

create proc USP_InsertPhieuMuon
@MaDocGia INT
as
begin
	declare @TinhTrang int
	select @TinhTrang = TinhTrang from DocGia where MaDocGia=@MaDocGia
	if(@TinhTrang=0)
	begin
		Insert into PhieuMuon
			(MaDocGia,			
			MaPhieuMuon,
			SoLuong
			)
			values
			(
			@MaDocGia,			
			@MaDocGia,
			0
			)
	end
	
			

end

go

create proc USP_KiemTraMaDG
@MaDocGia int,
@NgayMuon Smalldatetime
as 
begin
	select count (*) from PhieuMuon where MaDocGia = @MaDocGia and ( Year(NgayMuon) = Year(@NgayMuon) 
															and MONTH(NgayMuon) = MONTH(@NgayMuon)
															and Day(NgayMuon) = Day(@NgayMuon))
end
 
go

create proc USP_LayMaPhieuMuon
as 
begin
	select max(MaDocGia) from DocGia

end

go											
Create Proc USP_GetListPMByMaDG
as
begin
	select dg.MaDocGia as [Mã độc giả],TenDocGia as [Tên độc giả], pm.MaPhieuMuon as [Mã phiếu mượn]
	from DocGia as dg, PhieuMuon as pm 
	where dg.MaDocGia=pm.MaDocGia and TenDocGia like N'%%'
end

go
create proc USP_DeleteDocGia
@MaDocGia int
as
begin
	declare @MaPhieuMuon int
	select @MaPhieuMuon = MaPhieuMuon from PhieuMuon where MaDocGia=@MaDocGia
	delete CTPM where MaPhieuMuon = @MaPhieuMuon
	delete PhieuMuon where MaDocGia=@MaDocGia
	delete DocGia where MaDocGia = @MaDocGia		
end
go
Create Proc USP_SearchDocGiaByMaDG
@MaDocGia int
as
begin
	select *  
	from DocGia 
	where MaDocGia= @MaDocGia
end

go

Create Proc USP_GetListCTPMByMaPM
@MaPM int
as
begin
	select  ctpm.MaTaiLieu as [Mã tài liệu],tls.TenTaiLieu as [Tên tài liệu], ctpm.NgayMuon as [Ngày mượn], ctpm.NgayTra as [Ngày trả], ctpm.TinhTrang as [Tình trạng] 
	from PhieuMuon as pm, CTPM as ctpm, TaiLieu as tl, TaiLieuS as tls
	where pm.MaPhieuMuon=ctpm.MaPhieuMuon
	and ctpm.MaTaiLieu = tl.MaTaiLieu
	and tl.MaTaiLieuS = tls.MaTaiLieuS
	and ctpm.MaPhieuMuon = @MaPM
	order by ctpm.TinhTrang, NgayMuon
end
go
create proc USP_InsertCTPM
@MaPM Int,
@MaTaiLieu Int
as
begin
	
	declare @TinhTrangTL NVARCHAR(4)
	declare @NgayHetHanDG smalldatetime
	select @TinhTrangTL = TinhTrang from TaiLieu where @MaTaiLieu = MaTaiLieu
	select @NgayHetHanDG = dg.NgayHetHan from DocGia as dg, PhieuMuon as pm where dg.MaDocGia=pm.MaDocGia 
	if(@TinhTrangTL=N'Còn' and (@NgayHetHanDG>GETDATE()))
		begin		
			Insert into CTPM (MaPhieuMuon,
							MaTaiLieu,
							NgayMuon,
							NgayTra,
							TinhTrang
						)
						values(@MaPM,
						@MaTaiLieu,
						GetDATE(),
						DATEADD(day,5,GETDATE()),
						N'Chưa'					
						)						
			Update TaiLieu Set TinhTrang=N'Hết' where  MaTaiLieu=@MaTaiLieu
		end	
end


go
create proc USP_GetMaTaiLieuByMaTaiLieuS
@MaTaiLieuS int
as
	
begin
	SELECT MaTaiLieu FROM TaiLieu where TinhTrang = N'Còn' and MaTaiLieuS = @MaTaiLieuS
end
go
create proc USP_KiemTraTaiLieuSTonTai
@MaTaiLieuS INT
as
begin
	select * From TaiLieuS where MaTaiLieuS = @MaTaiLieuS
end
go
create proc USP_KiemTraTaiLieuCon
@MaTaiLieuS int
as
begin
	Select * from TaiLieu where TinhTrang = N'Còn' and MaTaiLieuS = @MaTaiLieuS	
end






go
create proc USP_ShowTacGiaByMaTaiLieuS
@MaTaiLieuS int
as
begin
	select TenTacGia as [Tác giả] from NhomTacGia ntg, TacGia tg where MaTaiLieuS=@MaTaiLieuS and ntg.MaTacGia=tg.MaTacGia
end

go
create proc USP_GetTenTaiLieu
@MaTaiLieuS int
as
begin
	select TenTaiLieu from TaiLieuS where MaTaiLieuS=@MaTaiLieuS
end

create proc USP_UpdateCTPM
@MaPhieuMuon int,
@MaTaiLieu int,
@TinhTrang NVARCHAR(5),
@NgayTra smalldatetime
as
begin	
	begin
		Update CTPM set TinhTrang = @TinhTrang where MaPhieuMuon=@MaPhieuMuon and MaTaiLieu = @MaTaiLieu and TinhTrang=N'Chưa'
		Update TaiLieu set TinhTrang = N'Còn' where MaTaiLieu = @MaTaiLieu 
	end
	
	
end
go

create proc USP_GetCTPMChuaTraByMaDocGia
@MaDocGia int
as
begin
	select tl.MaTaiLieu as [Mã tài liệu], tls.TenTaiLieu as [Tên tài liệu] from CTPM as ctpm, PhieuMuon as pm, TaiLieu as tl, TaiLieuS as tls where pm.MaDocGia=@MaDocGia
																							and ctpm.MaPhieuMuon = pm.MaPhieuMuon 
																							and ctpm.TinhTrang = N'Chưa'
																							and ctpm.MaTaiLieu = tl.MaTaiLieu
																							and tl.MaTaiLieuS = tls.MaTaiLieuS
end
go

create proc USP_InsertViPham
@MaDocGia int,
@LoaiViPham int
as
begin
	insert into ViPham (MaDocGia,
						LoaiViPham)
					values(@MaDocGia,
					@LoaiViPham)			
end
go
create proc USP_UpdateTaiLieu
@MaTaiLieu int
as
begin
	Update TaiLieu set TinhTrang=N'Hư hỏng' where MaTaiLieu=@MaTaiLieu
end







go
create proc USP_GetSoLuongSachDangMuon
@MaPhieuMuon int
as
begin
	select SoLuong From PhieuMuon where MaPhieuMuon = @MaPhieuMuon
	
end






go
create trigger trg_InserCTPM on CTPM
after Insert
as
begin
	update PhieuMuon
	set SoLuong = SoLuong + 1	
end
drop trigger trg_Update
go
create trigger trg_Update on CTPM
after Update
as
begin
	if (select TinhTrang from inserted) = N'Rồi'
	update PhieuMuon
	set SoLuong = SoLuong  - 1	
end


select * from PhieuMuon
select * from CTPM

delete CTPM
delete PhieuMuon
delete DocGia

insert into CTPM values (22,17,'2019/12/12','2019/12/12',N'Chưa')
update CTPM set TinhTrang=N'Rồi' where MaPhieuMuon = 22

go
create proc USP_DemSoHangTaiLieuDaTra
@MaPhieuMuon int
as
begin
	Select count (*) from CTPM where MaPhieuMuon=@MaPhieuMuon and TinhTrang=N'Rồi'
end
drop proc USP_DemSoHangTaiLieuDaTra
Select count (*) from CTPM where MaPhieuMuon=23 and TinhTrang=N'Rồi'

delete CTPM
delete PhieuMuon
delete DocGia
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    public class DocGia
    {
        public DocGia(int maDocGia, string tenDocGia, DateTime? ngaySinh, string diaChi, string sDT, string email, DateTime? ngayCap, DateTime? ngayHetHan, int TinhTrang)
        {
            this.MaDocGia = maDocGia;
            this.tenDocGia = TenDocGia;
            this.NgaySinh = ngaySinh;
            this.DiaChi = diaChi;
            this.SDT = sDT;
            this.Email = email;
            this.NgayCap = ngayCap;
            this.NgayHetHan = ngayHetHan;
            this.TinhTrang = this.TinhTrang;
            
        }

        public DocGia(DataRow row)
        {
            this.MaDocGia = (int)row["maDocGia"];
            this.TenDocGia = row["tenDocGia"].ToString();
            var NgaySinhTemp = row["ngaySinh"];
            if (NgaySinhTemp.ToString() != "")
                this.NgaySinh = (DateTime?)NgaySinhTemp;
           
            //this.NgaySinh = (DateTime?)row["ngaySinh"];

            this.DiaChi = row["diaChi"].ToString();
            this.SDT = row["sDT"].ToString();
            this.Email = row["email"].ToString();
            //this.NgayCap = (DateTime?)row["ngayCap"];
            var NgayCapTemp = row["ngayCap"];
            if (NgayCapTemp.ToString() != "")
                this.NgayCap = (DateTime?)NgayCapTemp;

            //this.NgayHetHan = (DateTime?)row["ngayHetHan"];
            var NgayHetHanTemp = row["ngayHetHan"];
            if (NgayHetHanTemp.ToString() != "")
                this.NgayHetHan = (DateTime?)NgayHetHanTemp;

            this.TinhTrang = (int)row["tinhTrang"];
          
        }
        


        private int maDocGia;
        private string tenDocGia;
        private DateTime? ngaySinh;
        private string diaChi;
        private string sDT;
        private string email;
        private DateTime? ngayCap;
        private DateTime? ngayHetHan;
        private string thaoTac;
        private int tinhTrang;



        public int MaDocGia { get => maDocGia; set => maDocGia = value; }
        public string TenDocGia { get => tenDocGia; set => tenDocGia = value; }
        public DateTime? NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SDT { get => sDT; set => sDT = value; }
        public string Email { get => email; set => email = value; }
        public DateTime? NgayCap { get => ngayCap; set => ngayCap = value; }
        public DateTime? NgayHetHan { get => ngayHetHan; set => ngayHetHan = value; }
        public string ThaoTac { get => thaoTac; set => thaoTac = value; }
        public int TinhTrang { get => tinhTrang; set => tinhTrang = value; }
    }
}

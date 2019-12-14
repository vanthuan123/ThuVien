using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    public class PhieuMuon
    {
        public PhieuMuon(int maPhieuMuon, int maDocGia, int soLuong)
        {
            this.MaPhieuMuon = maPhieuMuon;
            this.MaDocGia = maDocGia;
         
            this.SoLuong = soLuong;
          

        }

        public PhieuMuon(DataRow row)
        {
            this.MaPhieuMuon = (int)row["maPhieuMuon"];
            this.MaDocGia = (int)row["maDocGia"];         
           
            this.SoLuong = (int)row["soLuong"];
           

            
        }
        private int maPhieuMuon;
        private int maDocGia;
        
        private int soLuong;


        public int MaPhieuMuon { get => maPhieuMuon; set => maPhieuMuon = value; }
        public int MaDocGia { get => maDocGia; set => maDocGia = value; }
      
        public int SoLuong { get => soLuong; set => soLuong = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    public class CTPM
    {
        public CTPM(int maPhieuMuon, int maTaiLieu, DateTime? ngayMuon,DateTime? ngayTra, string tinhTrang)
        {
            this.MaPhieuMuon = maPhieuMuon;
            this.MaTaiLieu = maTaiLieu;
            this.NgayMuon = ngayMuon;
            this.NgayTra = ngayTra;           
            this.TinhTrang = tinhTrang;
        }
        public CTPM(DataRow row)
        {
            this.MaPhieuMuon = (int)row["maPhieuMuon"];
            this.MaTaiLieu = (int)row["maTaiLieu"];
            var NgayMuonTemp = row["ngayMuon"];
            if (NgayMuonTemp.ToString() != "")
                this.NgayMuon = (DateTime?)NgayTra;
            var NgayTraTemp = row["ngayTra"];
            if (NgayMuonTemp.ToString() != "")
                this.NgayTra = (DateTime?)NgayTra;
            this.TinhTrang = row["tinhTrang"].ToString();



        }
        private int maPhieuMuon;
        private int maTaiLieu;
        private DateTime? NgayMuon;
        private DateTime? ngayTra;
        private string tinhTrang;
       

        public int MaPhieuMuon { get => maPhieuMuon; set => maPhieuMuon = value; }
        public int MaTaiLieu { get => maTaiLieu; set => maTaiLieu = value; }      
        
        public string TinhTrang { get => tinhTrang; set => tinhTrang = value; }
        public DateTime? NgayMuon1 { get => NgayMuon; set => NgayMuon = value; }
        public DateTime? NgayTra1 { get => NgayTra; set => NgayTra = value; }
        public DateTime? NgayTra { get => ngayTra; set => ngayTra = value; }
    }
}

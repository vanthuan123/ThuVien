using QuanLyThuVien.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    public class PhieuMuonDAO
    {
        private static PhieuMuonDAO instance;

        public static PhieuMuonDAO Instance
        {
            get { if (instance == null) instance = new PhieuMuonDAO(); return instance; }
            private set { instance = value; }
        }
        private PhieuMuonDAO() { }
        public List<PhieuMuon> GetListPhieuMuon()
        {
            List<PhieuMuon> list = new List<PhieuMuon>();
            string query = "SELECT * FROM PhieuMuon";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                PhieuMuon phieumuon = new PhieuMuon(item);
                list.Add(phieumuon);                
            }
            return list;
        }
        public void InsertPhieuMuon(int MaDocGia)
        {
            DataProvider.Instance.ExcuteNonQuery("exec USP_InsertPhieuMuon @MaDocGia ", new object[] { MaDocGia });
        }
        //public bool KiemTraMaPM(int MaDocGia, DateTime? NgayMuon)
        //{
        //    int result = (int)DataProvider.Instance.ExcuteScalar("exec USP_KiemTraMaDG @MaDocGia , @NgayMuon", new object[] { MaDocGia, NgayMuon });
        //    return result > 0;
            
        //}
        public int LayMaPhieuMuon()
        {

            
            return (int)DataProvider.Instance.ExcuteScalar("exec USP_LayMaPhieuMuon", new object[] { });
            

        }
        public bool GioiHanMuonTaiLieu(int MaPhieuMuon) 
        {
            int x = (int)DataProvider.Instance.ExcuteScalar("exec USP_GetSoLuongSachDangMuon @MaPhieuMuon ", new object[] { MaPhieuMuon });
            if (x == 3)
                return true;
            return false;
        }
      
    }
}

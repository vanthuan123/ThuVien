using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    public class CTPMDAO
    {
        private static CTPMDAO instance;

        public static CTPMDAO Instance
        {
            get { if (instance == null) instance = new CTPMDAO(); return instance; }
            private set { instance = value; }
        }
        private CTPMDAO() { }
        public DataTable ShowCTPMByMaPM(int MaPM)
        {
            return DataProvider.Instance.ExcuteQuery("exec USP_GetListCTPMByMaPM @MaPM", new object[] { MaPM });

        }
        public int InsertCTPM(int MaPhieuMuon, int MaTaiLieu)
        {
            return DataProvider.Instance.ExcuteNonQuery("exec USP_InsertCTPM @MaPhieuMuon , @MaTaiLieu", new object[] { MaPhieuMuon, MaTaiLieu});
        }
        public void UpdateCTPM(int MaPhieuMuon, int MaTaiLieu, string TinhTrang, DateTime? NgayTra)
        {
            DataProvider.Instance.ExcuteNonQuery("exec USP_UpdateCTPM @MaPhieuMuon , @MaTaiLieu , @TinhTrang , @NgayTra", new object[] { MaPhieuMuon, MaTaiLieu, TinhTrang, NgayTra }); 
                    
                   
            

        }
        public bool KiemTraTaiLieuTonTai(int MaTaiLieu)
        {
            string query = string.Format("select * From TaiLieu where MaTaiLieu = '{0}'", MaTaiLieu);
            var x = (DataProvider.Instance.ExcuteScalar(query));
            if(x ==null )
            {
                return false;
            }
            return true;
            
        }
        public bool KiemTraTaiLieuSTonTai(int MaTaiLieuS)
        {
            var x = (DataProvider.Instance.ExcuteScalar("USP_KiemTraTaiLieuSTonTai @MaTaiLieuS", new object[] { MaTaiLieuS }));
            if (x == null)
            {
                return false;
            }
            return true; //ton tai

        }
        public int GetMaTaiLieuByMaTaiLieuS(int MaTaiLieuS)
        {
            int x = (int)DataProvider.Instance.ExcuteScalar("exec USP_GetMaTaiLieuByMaTaiLieuS @MaTaiLieuS", new object[] { MaTaiLieuS});
         
            return x;
        }
        public bool KiemTraTaiLieuCon(int MaTaiLieuS)
        {
            
            var x = (DataProvider.Instance.ExcuteScalar("USP_KiemTraTaiLieuCon @MaTaiLieuS", new object[] { MaTaiLieuS}));
            if (x == null)
            {
                return false;
            }
            return true; //ton tai
        }
        public DataTable ShowTacGiaByMaTaiLieuS(int MaTaiLieuS)
        {
            return DataProvider.Instance.ExcuteQuery("exec USP_ShowTacGiaByMaTaiLieuS @MaTaiLieuS", new object[] { MaTaiLieuS });
        }
        public string GetTaiLieu(int MaTaiLieuS)
        {

            
            string data = DataProvider.Instance.ExcuteScalar("exec USP_GetTenTaiLieu @MaTaiLieuS", new object[] { MaTaiLieuS }).ToString();

            return data;
        }
        public int DemSoSachDaTra(int MaPhieuMuon)
        {
            
            return (int)DataProvider.Instance.ExcuteScalar("exec USP_DemSoHangTaiLieuDaTra @MaPhieuMuon", new object[] { MaPhieuMuon });
        }
    }
}

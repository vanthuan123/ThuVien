using QuanLyThuVien.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    public class DocGiaDAO
    {
        private static DocGiaDAO instance;
        public static DocGiaDAO Instance
        {
            get { if (instance == null) instance = new DocGiaDAO(); return instance; }
            private set { instance = value; }
        }
        private DocGiaDAO() { }
        public List<DocGia> GetListDocGia()
        {
            List<DocGia> list = new List<DocGia>();
            string query = "SELECT * FROM DocGia";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach(DataRow item in data.Rows  )
            {
                DocGia docgia = new DocGia(item);

                list.Add(docgia);
                docgia.ThaoTac = "delete";
            }
            return list;
        }

        public void InsertDocGia(string TenDocGia, DateTime? NgaySinh, string DiaChi, string SDT, string Email)
        {
            DataProvider.Instance.ExcuteNonQuery("exec USP_InsertDocGia @TenDocGia , @NgaySinh , @DiaChi , @SDT , @Email", new object[] { TenDocGia, NgaySinh, DiaChi, SDT, Email });
        }  //truyền querry thêm độc giả
        public void DeleteDocGia(int MaDocGia)
        {
            DataProvider.Instance.ExcuteNonQuery("exec USP_DeleteDocGia @ID", new object[] { MaDocGia });           
        }  //truyền query xóa độc giả chưa có các ràng buộc về khóa ngoại
        public void UpdateDocGia(int ID, string TenDocGia, DateTime? NgaySinh, string DiaChi, string SDT, string Email)
        {
            DataProvider.Instance.ExcuteNonQuery("exec USP_UpdateDocGia @ID , @TenDocGia , @NgaySinh , @DiaChi , @SDT , @Email", new object[] { ID, TenDocGia, NgaySinh, DiaChi, SDT, Email });
        } //truyền query sửa
        public List<DocGia> SerachDocGiaByName(string name)
        {
            List<DocGia> list = new List<DocGia>();
            string query = string.Format("SELECT * FROM DocGia where TenDocGia LIKE N'%{0}%'", name);
            //string query = string.Format("select dg.MaDocGia,dg.TenDocGia, pm.MaPhieuMuon, NgayMuon from DocGia as dg, PhieuMuon as pm where dg.MaDocGia=pm.MaDocGia and TenDocGia like N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DocGia docgia = new DocGia(item);
                list.Add(docgia);
                
            }
            return list;
        }
        public DocGia SerachDocGiaByMaDG(int MaDocGia)
        {
           

            DataTable data = DataProvider.Instance.ExcuteQuery("exec USP_SearchDocGiaByMaDG @MaDocGia", new object[] { MaDocGia });
            foreach (DataRow item in data.Rows)
            {
                DocGia docgia = new DocGia(item);
                return docgia;
                             
            }
            return null;
            



        }
        public DataTable ShowPhieuMuonByMaDg(string name)
        {

            
            DataTable data = DataProvider.Instance.ExcuteQuery("exec USP_GetListPMByMaDG ");
            return data;
        }
        public bool KiemTraDocGiaConHan(int MaDocGia)
        {
            string query = string.Format("select NgayHetHan from DocGia where NgayHetHan > GetDate() and MaDocGia = '{0}'", MaDocGia);
            var x = DataProvider.Instance.ExcuteScalar(query).ToString();
            if (x == "")
                return false;
            return true;
            
        }
        public DataTable GetCTPMChuaTraByMaDocGia(int MaDocGia)
        {
            return DataProvider.Instance.ExcuteQuery("exec USP_GetCTPMChuaTraByMaDocGia @MaDocGia", new object[] { MaDocGia });
        }
        public bool KiemTraDocGiaBiKhoa(int MaDocGia)
        {
            string query = string.Format("Select TinhTrang from DocGia where MaDocGia= '{0}'", MaDocGia);
            int x = int.Parse(DataProvider.Instance.ExcuteScalar(query).ToString());
            if (x == 1)
                return true;
            return false;
        }
        public void CapNhatTinhTrangDocGia(int MaDocGia)
        {
            DataProvider.Instance.ExcuteNonQuery("exec USP_CapNhatTinhTrangDocGia @MaDocGia", new object[] { MaDocGia });
        }
    }
       
}


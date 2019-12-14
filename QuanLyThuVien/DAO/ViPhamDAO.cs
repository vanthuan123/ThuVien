using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    public class ViPhamDAO
    {
        private static ViPhamDAO instance;

        public static ViPhamDAO Instance
        {
            get { if (instance == null) instance = new ViPhamDAO(); return instance; }
            private set { instance = value; }
        }
        private ViPhamDAO() { }
        public void InsertViPham(int MaDocGia, int LoaiViPham, int MaTaiLieu, DateTime NgayTra)
        {
            if(LoaiViPham==0)
            {
                DataProvider.Instance.ExcuteNonQuery("exec USP_UpdateCTPM @MaDocGia , @MaTaiLieu ,  N'Rồi' , @NgayTra", new object[] { MaDocGia, MaTaiLieu, NgayTra });
                DataProvider.Instance.ExcuteNonQuery("exec USP_InsertViPham @MaDocGia , @LoaiVhiPham", new object[] { MaDocGia, LoaiViPham });
                
            }
            if(LoaiViPham==1)
            {
                DataProvider.Instance.ExcuteNonQuery("exec USP_UpdateCTPM @MaDocGia , @MaTaiLieu ,  N'Rồi' , @NgayTra", new object[] { MaDocGia, MaTaiLieu, NgayTra });
                DataProvider.Instance.ExcuteNonQuery("exec USP_UpdateTaiLieu @MaTaiLieu", new object[] { MaTaiLieu });
                DataProvider.Instance.ExcuteNonQuery("exec USP_InsertViPham @MaDocGia , @LoaiVhiPham", new object[] { MaDocGia, LoaiViPham });
                
            }           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    public class ViPham
    {
        public ViPham(int maDocGia, int loaiViPham)
        {
            this.MaDocGia = maDocGia;
            this.LoaiViPham = loaiViPham;
        }
        public ViPham(DataRow row)
        {
            
            this.MaDocGia = (int)row["maDocGia"];
            this.LoaiViPham = (int)row["loaiViPham"];

        }
        private int maDocGia;
        private int loaiViPham;


        public int MaDocGia { get => maDocGia; set => maDocGia = value; }
        public int LoaiViPham { get => loaiViPham; set => loaiViPham = value; }
    }
}

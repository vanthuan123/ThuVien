using QuanLyThuVien.DAO;
using QuanLyThuVien.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fAdmin : Form
    {


       

        public fAdmin()
        {
            InitializeComponent();
            LoadMaDocGia();

            LockbtnTaopm();
            LoadPhieuMuonByMaDocGia(txbHoten.Text.ToString());



        }
        #region thuancode
        #region Method
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }


        }
        void showDocgia()
        {
     
            
            dtgDocgia.DataSource = DocGiaDAO.Instance.GetListDocGia(); //load lên datagridview
            try //hiện thị linkcell (ô màu xanh cuối hàng - ô hoạt động)
            {
                for (int i = 0; i < dtgDocgia.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dtgDocgia[8, i] = linkCell;
            
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }   //hiện thị độc giả lên dâtgridview
        void LoadMaDocGia()
        {
            List<DocGia> listDocgia = DocGiaDAO.Instance.GetListDocGia();
            cbMadg.DataSource = listDocgia;
            cbMadg.DisplayMember = "MaDocGia";
            cbMaDocGiadg.DataSource = listDocgia;
            cbMaDocGiadg.DisplayMember = "MaDocGia";
            for (int i = 0; i < dtgCTPMChuaTra.Rows.Count - 1; i++)
            {
                dtgCTPMChuaTra.Rows[i].Cells[2].Value = false;
                dtgCTPMChuaTra.Rows[i].Cells[3].Value = false;
            }
        }//chuyển dữ liệu mã độc giả vào combobox
        List<DocGia> SearchDocGiaByName(string name)
        {
            List<DocGia> listdocgia = new List<DocGia>();
            listdocgia = DocGiaDAO.Instance.SerachDocGiaByName(name);
            return listdocgia;
        }   //tìm kiếm theo tên độc giả
        void LayMaPhieuMuon()
        {

            //int MaDocGia = int.Parse(cbMadg.Text);
            //DateTime? NgayMuon = dtpNgayMuon.Value;
            ////bool result = PhieuMuonDAO.Instance.KiemTraMaPM(MaDocGia, NgayMuon);
            //int ma = PhieuMuonDAO.Instance.LayMaPhieuMuon(MaDocGia, NgayMuon);

            //if (result == true)
            //{
            //    MessageBox.Show("Độc giả đã lập phiếu mượn trong ngày hôm nay rồi! Và mã phiếu của độc giả là: " + ma);
            //}
            //else
            //{
            //    txbMapm.Text = (ma + 1).ToString();
            //}

        } //lấy mã phiếu mượn ứng với ngày 
        void LockbtnTaopm()
        {



        }           //lock button khi chưa đủ thông tin 
        void LoadPhieuMuonByMaDocGia(string name)
        {

        }
        void LoadThongTinDocGiaInPhieuMuon(int MaDocGia)
        {
            DocGia docgia = DocGiaDAO.Instance.SerachDocGiaByMaDG(MaDocGia);

            txbTendgm.Text = docgia.TenDocGia;
            txbSDTm.Text = docgia.SDT;
            txbEmailm.Text = docgia.Email;
        }
        void AddPhieuMuonBinding()
        {
            //txbMapm.DataBindings.Add(new Binding("Text", dtgListdg.DataSource, "MaPhieuMuon"));
        }
        void LoadCTPMByMaPM(int ma)
        {
            //dtgCTPM.DataSource = CTPMDAO.Instance.ShowCTPMByMaPM(ma);
            DataSet ds = new DataSet();
            ds.Tables.Add(CTPMDAO.Instance.ShowCTPMByMaPM(ma));
            dtgCTPM.DataSource = ds.Tables[0];
            dtgCTPM.Refresh();




        }  //hiện thị ctpm bằng mã phiếu mượn
        bool KiemTraDocGiaConHan(int MaDocGia)
        {
            return DocGiaDAO.Instance.KiemTraDocGiaConHan(MaDocGia);

        }  //kiểm tra thẻ độc giả còn hạn hay không
        bool KiemTraTaiLieuTonTai(int MaTaiLieu)
        {
            return CTPMDAO.Instance.KiemTraTaiLieuTonTai(MaTaiLieu);
        } //kiểm tra tài liệu có tồn tại
        void InsertCTPM(int MaPhieuMuon, int MaTaiLieuS)
        {
            int MaTaiLieu = GetMaTaiLieuByMaTaiLieuS(MaTaiLieuS);

            CTPMDAO.Instance.InsertCTPM(MaPhieuMuon, MaTaiLieu);
        }  //Insert chi tiết phiếu mượn
        int GetMaTaiLieuByMaTaiLieuS(int MaTaiLieuS)
        {
            return CTPMDAO.Instance.GetMaTaiLieuByMaTaiLieuS(MaTaiLieuS);

        }  //Lấy mã tài liệu theo mã tài liệu S 
        bool KiemTraTaiLieuSTonTai(int MaTaiLieuS)  //Kiểm tra tài liệu có tồn tại
        {
            return CTPMDAO.Instance.KiemTraTaiLieuSTonTai(MaTaiLieuS);
        }
        bool KiemTraTaiLieuCon(int MaTaiLieuS) //kiểm tra tài liệu có còn để cho mượn không
        {
            return CTPMDAO.Instance.KiemTraTaiLieuCon(MaTaiLieuS);
        }

        void LoadCTPMChuaTraByMaDocGia(int MaDocGia)
        {

            DataSet ds = new DataSet();
            ds.Tables.Add(DocGiaDAO.Instance.GetCTPMChuaTraByMaDocGia(MaDocGia));
            dtgCTPMChuaTra.Columns.Clear();
            dtgCTPMChuaTra.DataSource = ds.Tables[0];
            DataGridViewCheckBoxColumn dtgCheck1 = new DataGridViewCheckBoxColumn();
            dtgCheck1.HeaderText = "Trả trễ";
            DataGridViewCheckBoxColumn dtgCheck2 = new DataGridViewCheckBoxColumn();
            dtgCheck2.HeaderText = "Hư hỏng";
            dtgCTPMChuaTra.Columns.Add(dtgCheck1);
            dtgCTPMChuaTra.Columns.Add(dtgCheck2);
            dtgCTPMChuaTra.Refresh();
            for (int i = 0; i < dtgCTPMChuaTra.Rows.Count - 1; i++)
            {
                dtgCTPMChuaTra.Rows[i].Cells[4].Value = false;
                dtgCTPMChuaTra.Rows[i].Cells[5].Value = false;
            }
            if (dtgCTPMChuaTra.Rows.Count <= 1)
            {
                dtgCTPMChuaTra.Columns.Clear();
            }

        }
        void InsertViPham()
        {
            int loaivipham = 0;
            for (int i = 0; i < dtgCTPMChuaTra.Rows.Count - 1; i++)
            {


                bool check1 = (bool)dtgCTPMChuaTra.Rows[i].Cells[4].Value;
                bool check2 = (bool)dtgCTPMChuaTra.Rows[i].Cells[5].Value;
                if (check1 == true && check2 == false)
                {
                    loaivipham = 0;
                }
                else if (check2 == true && check1 == false)
                {
                    loaivipham = 1;

                }
                else if (check1 == true && check2 == true)
                {
                    loaivipham = 1;
                }
                else
                {
                    loaivipham = 3;
                }

                ViPhamDAO.Instance.InsertViPham(int.Parse(cbMaDocGiadg.Text.ToString()), loaivipham, int.Parse(dtgCTPMChuaTra.Rows[i].Cells[0].Value.ToString()),DateTime.Parse(dtgCTPMChuaTra.Rows[i].Cells[3].Value.ToString()));
                
            }
            DocGiaDAO.Instance.CapNhatTinhTrangDocGia(int.Parse(cbMaDocGiadg.Text.ToString()));


        }
        #endregion

        #region Events


        private void btnThemTK_Click(object sender, EventArgs e) //thêm độc giả mới
        {
            string userName = txbName.Text;
            string displayName = txuserName.Text;
            int type = (int)numType.Value;
            AddAccount(userName, displayName, type);


        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //DocGia docgia = lsvDocgia.Tag as DocGia;

            string TenDocGia = txbHoten.Text.ToString();
            DateTime? NgaySinh = dtpNgaysinh.Value;
            string DiaChi = txbDiachi.Text.ToString(); ;
            string SDT = txbSDT.Text.ToString();
            string Email = txbEmail.Text.ToString();
            if (txbHoten.Text.ToString() == "" || txbDiachi.Text.ToString() == "" || txbSDT.Text.ToString() == "" || txbEmail.Text.ToString() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!");
            }
            else
            {
                DocGiaDAO.Instance.InsertDocGia(TenDocGia, NgaySinh, DiaChi, SDT, Email);
                PhieuMuonDAO.Instance.InsertPhieuMuon(PhieuMuonDAO.Instance.LayMaPhieuMuon());
                MessageBox.Show("Thêm độc giả thành công!!");
                showDocgia();
            }
            LoadMaDocGia();





        }  //lưu thông tin độc giả

        private void button1_Click(object sender, EventArgs e)
        {
            showDocgia();
        } //button hiện thị danh sách độc giả 

        private void dtgDocgia_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int lastRow = e.RowIndex;
                DataGridViewRow nRow = dtgDocgia.Rows[lastRow];
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                dtgDocgia[8, lastRow] = linkCell;
                nRow.Cells["ThaoTac"].Value = "Update";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }  //sự kiện chọn để chuyển trạng thái delete thành insert

        public void dtgDocgia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = (int)dtgDocgia.Rows[e.RowIndex].Cells[0].Value;
            try
            {

                if (e.ColumnIndex == 8)
                {
                    string Task = dtgDocgia.Rows[e.RowIndex].Cells[8].Value.ToString();
                    if (Task == "delete")
                    {
                        if (MessageBox.Show("Toàn bộ thông tin của độc giả (kể cả phiếu mượn) đều mất đi, bạn có muốn xóa không?", "Đang xóa...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            DocGiaDAO.Instance.DeleteDocGia(ID);
                            showDocgia();
                        }
                    }
                    else if (Task == "Update")
                    {
                        string TenDocGia = dtgDocgia.Rows[e.RowIndex].Cells[1].Value.ToString();
                        DateTime? NgaySinh = (DateTime?)dtgDocgia.Rows[e.RowIndex].Cells[2].Value;
                        string DiaChi = dtgDocgia.Rows[e.RowIndex].Cells[3].Value.ToString();
                        string SDT = dtgDocgia.Rows[e.RowIndex].Cells[4].Value.ToString();
                        string Email = dtgDocgia.Rows[e.RowIndex].Cells[5].Value.ToString();
                        if (MessageBox.Show("Bạn có chắc chắm muốn thay đổi thông tin độc giả có ID " + ID + " ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            DocGiaDAO.Instance.UpdateDocGia(ID, TenDocGia, NgaySinh, DiaChi, SDT, Email);
                            MessageBox.Show("Sửa độc giả có ID " + ID + " thành công!");

                        }

                        showDocgia();


                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }  //sự kiện xóa hoặc sửa độc giả trên datagridview 



        private void btnSearchdocgia_Click(object sender, EventArgs e)
        {
            if (cbMadg.Text.ToString() == "")
            {
                MessageBox.Show("Vui lòng nhập mã độc giả cần tra!");
            }
            else
            {
                LoadCTPMByMaPM(int.Parse(cbMadg.Text.ToString()));
                LoadThongTinDocGiaInPhieuMuon(int.Parse(cbMadg.Text.ToString()));
            }



        } //button tìm kiếm độc giả - Mượn sách

        private void btnKTMaPM_Click(object sender, EventArgs e)
        {
            LayMaPhieuMuon();
            LockbtnTaopm();
        }  //button tạo mã phiếu mượn - Mượn sách



        private void btnCTPM_Click(object sender, EventArgs e)
        {
            //if (txbMapm.Text.ToString() == "")
            //    MessageBox.Show("Vui lòng chọn phiếu mượn cần show!!");
            //else
            //    LoadCTPMByMaPM(int.Parse(cbMadg.Text.ToString()));

        }  //load chi tiết phiếu mượn dựa vào mã phiếu mượn - Mượn sách

        private void btnInsertCTPM_Click(object sender, EventArgs e) // thêm sách vào CTPM
        {
            if (btnInsertCTPM.Text == "Thêm")
            {
                if(KiemTraDocGiaBiKhoa(int.Parse(cbMaDocGiadg.Text.ToString())))
                {
                    MessageBox.Show("Thẻ độc giả đang bị tạm khóa!!");
                }
                else
                {
                    if ((KiemTraTaiLieuSTonTai(int.Parse(txbMatls.Text.ToString())) && !KiemTraTaiLieuCon(int.Parse(txbMatls.Text.ToString()))))
                    {
                        MessageBox.Show("Không còn tài liệu trống, vui vòng kiểm tra lại!");
                        btnInsertCTPM.Text = "Tìm kiếm";
                    }
                    else
                    {

                        if (PhieuMuonDAO.Instance.GioiHanMuonTaiLieu(int.Parse(cbMadg.Text.ToString())) == true)
                        {
                            MessageBox.Show("Độc dã đã mượn đủ số tài liệu");
                        }
                        else
                        {
                            InsertCTPM(int.Parse(cbMadg.Text.ToString()), int.Parse(txbMatls.Text.ToString()));

                            MessageBox.Show("Thêm tài liệu thành công!!");

                        }
                        btnInsertCTPM.Text = "Tìm kiếm";

                        LoadCTPMByMaPM(int.Parse(cbMadg.Text.ToString()));

                    }

                }
                

            }
            else
            {
                if (txbMatls.Text.ToString() != "")
                {
                    if (!KiemTraTaiLieuSTonTai(int.Parse(txbMatls.Text.ToString())))
                    {
                        MessageBox.Show("Tài liệu không tồn tại! Vui lòng kiểm tra lại");
                        btnInsertCTPM.Text = "Tìm kiếm";
                        return;
                    }
                    else
                    {
                        string s = CTPMDAO.Instance.GetTaiLieu(int.Parse(txbMatls.Text.ToString()));
                        txbTenTaiLieum.Text = s;
                        dtgTacGia.DataSource = CTPMDAO.Instance.ShowTacGiaByMaTaiLieuS(int.Parse(txbMatls.Text.ToString()));
                        btnInsertCTPM.Text = "Thêm";

                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã tài liệu cần tra");
                }



            }








        }

        private bool KiemTraDocGiaBiKhoa(int MaDocGia)
        {
            return DocGiaDAO.Instance.KiemTraDocGiaBiKhoa(MaDocGia);
        }

        private void txbMatls_TextChanged(object sender, EventArgs e)
        {
            btnInsertCTPM.Text = "Tìm kiếm";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadCTPMChuaTraByMaDocGia(int.Parse(cbMaDocGiadg.Text.ToString()));
            for (int i = 0; i < dtgCTPMChuaTra.Rows.Count - 1; i++)
            {
                dtgCTPMChuaTra.Rows[i].Cells[4].Value = false;
                dtgCTPMChuaTra.Rows[i].Cells[5].Value = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            InsertViPham();
            LoadCTPMChuaTraByMaDocGia(int.Parse(cbMaDocGiadg.Text.ToString()));
        }
        private void txbSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }




        private void btnUpdateCTPM_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgCTPM.Rows.Count-2 ; i++)
            {
                string checksell = dtgCTPM.Rows[i].Cells[4].Value.ToString();
                int MaPhieuMuon = int.Parse(cbMadg.Text.ToString());
                int MaTaiLieu = int.Parse(dtgCTPM.Rows[i].Cells[0].Value.ToString());
                DateTime NgayTra = DateTime.Parse(dtgCTPM.Rows[i].Cells[3].Value.ToString());
                if(checksell=="Rồi")
                {
                    CTPMDAO.Instance.UpdateCTPM(MaPhieuMuon, MaTaiLieu, checksell, NgayTra);
                }
               
            }
            LoadCTPMByMaPM(int.Parse(cbMadg.Text.ToString()));




        }

      
    }




























    #endregion

    #endregion


}



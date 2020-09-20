using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_VatLieuXayDung
{
    public partial class frmLichSuPhieuNhap : Form
    {
        KETNOI conn = new KETNOI();
        public frmLichSuPhieuNhap()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            try
            {
                string ngay = DateTime.ParseExact(pckNgayCanTim.Text, "MM/dd/yyyy", null).ToString("yyyy/MM/dd");
                string sql = "select PN.MAPN[MÃ PN],NGAYNHAP[NGÀY NHẬP],NCC.TENNCC[TÊN NCC],NV.TENNV[TÊN NHÂN VIÊN],PN.THANHTIEN[THÀNH TIỀN] from PHIEUNHAP PN, NHACUNGCAP NCC, NHANVIEN NV where PN.MANV = NV.MANV and PN.MANCC = NCC.MANCC and PN.NGAYNHAP ='" + ngay + "'";
                DataSet ds = conn.GrdSource(sql);
                dgVDSTK.DataSource = ds.Tables[0];
                dgVDSTK.Refresh();
                return;
            }
            catch
            {
                MessageBox.Show("Lỗi định dạng ngày !!\n Xin vui lòng cài đặt lại ngày hệ thống tháng/ngày/năm!!");
                return;
            }
        }

        private void dgVDSTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                txtMaPN.Text = dgVDSTK.Rows[index].Cells[0].Value.ToString();
                mskNgayNhap.Text = dgVDSTK.Rows[index].Cells[1].Value.ToString();
                txtNhaCC.Text = dgVDSTK.Rows[index].Cells[2].Value.ToString();
                txtTenNV.Text = dgVDSTK.Rows[index].Cells[3].Value.ToString();
                txtTongTien.Text = dgVDSTK.Rows[index].Cells[4].Value.ToString();
                return;
            }
            catch
            {
                return;
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            frmInPhieuNhap frm = new frmInPhieuNhap(txtMaPN.Text.Trim());
            frm.ShowDialog();
        }

        private void txtMaPN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMaPN.Text.Trim() == "")
                {
                    btnInHD.Enabled = false;
                }
                else
                {
                    btnInHD.Enabled = true;
                }
                return;
            }
            catch
            {
                return;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pckNgayCanTim_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ngay = DateTime.ParseExact(pckNgayCanTim.Text, "MM/dd/yyyy", null).ToString("yyyy/MM/dd");
                string sql = "select PN.MAPN[MÃ PN],NGAYNHAP[NGÀY NHẬP],NCC.TENNCC[TÊN NCC],NV.TENNV[TÊN NHÂN VIÊN],PN.THANHTIEN[THÀNH TIỀN] from PHIEUNHAP PN, NHACUNGCAP NCC, NHANVIEN NV where PN.MANV = NV.MANV and PN.MANCC = NCC.MANCC and PN.NGAYNHAP ='" + ngay + "'";
                DataSet ds = conn.GrdSource(sql);
                dgVDSTK.DataSource = ds.Tables[0];
                dgVDSTK.Refresh();
                return;
            }
            catch
            {
                MessageBox.Show("Lỗi định dạng ngày !!\n Xin vui lòng cài đặt lại ngày hệ thống tháng/ngày/năm!!");
                return;
            }
        }
    }
}

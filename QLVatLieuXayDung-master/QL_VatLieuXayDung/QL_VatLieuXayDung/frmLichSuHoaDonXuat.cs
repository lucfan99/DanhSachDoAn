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
    public partial class frmLichSuHoaDonXuat : Form
    {
        KETNOI conn = new KETNOI();
        public frmLichSuHoaDonXuat()
        {
            InitializeComponent();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            try
            {
                string ngay = DateTime.ParseExact(pckNgayCanTim.Text, "MM/dd/yyyy", null).ToString("yyyy/MM/dd");
                string sql = "select HD.MAHD[MÃ HD],NGAYHD[NGÀY HD],TINHTRANGHD[TÌNH TRẠNG],NV.TENNV[TÊN NHÂN VIÊN],KH.MAKH[MÃ KH],TENKH[TÊN KH],KH.DIACHI[ĐỊA CHỈ KH],KH.SDT[SĐT KH],HD.TONGTIEN from HOADON HD, NHANVIEN NV, KHACHHANG KH where HD.MANV = NV.MANV and HD.MAKH = KH.MAKH and HD.NGAYHD ='" + ngay + "'";
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
                txtMaHD.Text = dgVDSTK.Rows[index].Cells[0].Value.ToString();
                mskNgayHD.Text = dgVDSTK.Rows[index].Cells[1].Value.ToString();
                txtTenNV.Text = dgVDSTK.Rows[index].Cells[3].Value.ToString();
                txtMaKH.Text = dgVDSTK.Rows[index].Cells[4].Value.ToString();
                txtTenKH.Text = dgVDSTK.Rows[index].Cells[5].Value.ToString();
                txtDiaChiKH.Text = dgVDSTK.Rows[index].Cells[6].Value.ToString();
                txtSDTKH.Text = dgVDSTK.Rows[index].Cells[7].Value.ToString();
                txtTongTien.Text = dgVDSTK.Rows[index].Cells[8].Value.ToString();
            }
            catch
            {
                return;
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            try
            {
                frmInPhieuXuatVT frm = new frmInPhieuXuatVT(txtMaHD.Text.Trim());
                frm.ShowDialog();
                return;
            }
            catch
            {
                MessageBox.Show("Lỗi định dạng ngày !!\n Xin vui lòng cài đặt lại ngày hệ thống tháng/ngày/năm!!");
                return;
            }
        }

        private void txtMaHD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHD.Text.Trim() == "")
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
                string sql = "select HD.MAHD[MÃ HD],NGAYHD[NGÀY HD],TINHTRANGHD[TÌNH TRẠNG],NV.TENNV[TÊN NHÂN VIÊN],KH.MAKH[MÃ KH],TENKH[TÊN KH],KH.DIACHI[ĐỊA CHỈ KH],KH.SDT[SĐT KH],HD.TONGTIEN from HOADON HD, NHANVIEN NV, KHACHHANG KH where HD.MANV = NV.MANV and HD.MAKH = KH.MAKH and HD.NGAYHD ='" + ngay + "'";
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

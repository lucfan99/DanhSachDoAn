using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_VatLieuXayDung
{
    public partial class frmTrangChu : Form
    {
        KETNOI conn = new KETNOI();
        string tenNV;
        public frmTrangChu()
        {
            InitializeComponent();
        }
        public void doiMatKhau()
        {
            Application.Run(new frmDangKy());
        }

        private void mnuDoiMK_Click(object sender, EventArgs e)
        {
            Thread a = new Thread(new ThreadStart(doiMatKhau));
            a.Start();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            if (frmDangNhap.Quyen == "Admin")
            {
                mnuQuanLyNguoiDung.Enabled = true;
            }
            else
            {
                mnuQuanLyNguoiDung.Enabled = false;
                menuDM_VatTu.Enabled = false;
                menuDMNhap.Enabled = false;
                mnuDM_QLNhanVien.Enabled = false;
                mnuThongKe.Enabled = false;
                menuDMNCC.Enabled = false;
                menuDMLoaiVT.Enabled = false;
                btnQLNhanVien.Enabled = false;
                btnNhapHang.Enabled = false;
                mnuDM_LS_PN.Enabled = false;
            }


            string user = frmDangNhap.ID_USER;
            string cv = frmDangNhap.ChucVu;
            string strSQL = "select TENNV from dbo.TAIKHOAN TK, dbo.NHANVIEN NV where TK.MANV = NV.MANV AND ID ='" + user + "'";
            SqlDataReader dr = conn.getReader(strSQL);
            while (dr.Read())
            {
                hienThiTT.Text = "Xin chào " + (dr["TENNV"].ToString()) + "  || CHỨC VỤ:  " + frmDangNhap.ChucVu + " . Cảm ơn đã sử dụng Ứng dụng Quản Lý Vật Liệu Xây Dựng";
                tenNV = dr["TENNV"].ToString();
            }
        }

        private void frmTrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show(tenNV + "\n Bạn có muốn đăng xuất tài khoản không ?", "Xuất tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                frmDangNhap dn = new frmDangNhap();
                dn.ShowDialog();
            }
        }

        private void menuBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelCongCu.Visible = menuBarToolStripMenuItem.Checked;
            groupBox1.Visible = menuBarToolStripMenuItem.Checked;
            btnBanHang.Visible = menuBarToolStripMenuItem.Checked;
            btnQLNhanVien.Visible = menuBarToolStripMenuItem.Checked;
            btnNhapHang.Visible = menuBarToolStripMenuItem.Checked;
            btnKho.Visible = menuBarToolStripMenuItem.Checked;
        }

        private void mnuQuanLyNguoiDung_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmQuanLyTaiKhoan")
                {
                    f.Activate();
                    return;
                }
            }

            frmQuanLyTaiKhoan frmTK = new frmQuanLyTaiKhoan();
            frmTK.MdiParent = this;

            frmTK.Show();
            frmTK.Top = 0;
            frmTK.Left = 0;
        }

        private void menuDM_VatTu_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmVatTu")
                {
                    f.Activate();
                    return;
                }
            }

            frmVatTu frmVT = new frmVatTu();
            frmVT.MdiParent = this;

            frmVT.Show();
            frmVT.Top = 0;
            frmVT.Left = 0;
        }

        private void menuDMKho_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmKho")
                {
                    f.Activate();
                    return;
                }
            }

            frmKho frmTK = new frmKho();
            frmTK.MdiParent = this;

            frmTK.Show();
            frmTK.Top = 0;
            frmTK.Left = 0;
        }

        private void menuDMLoaiVT_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmLoaiVT")
                {
                    f.Activate();
                    return;
                }
            }

            frmLoaiVT frmLoai = new frmLoaiVT();
            frmLoai.MdiParent = this;

            frmLoai.Show();
            frmLoai.Top = 0;
            frmLoai.Left = 0;
        }

        private void menuDMNCC_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmNhaCungCap")
                {
                    f.Activate();
                    return;
                }
            }

            frmNhaCungCap frmNCC = new frmNhaCungCap();
            frmNCC.MdiParent = this;

            frmNCC.Show();
            frmNCC.Top = 0;
            frmNCC.Left = 0;
        }


        private void btnKho_Click(object sender, EventArgs e)
        {
            menuDMKho_Click(sender, e);
        }

        private void mnuDM_QLNhanVien_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmQuanLyNhanVien")
                {
                    f.Activate();
                    return;
                }
            }

            frmQuanLyNhanVien frmNH = new frmQuanLyNhanVien();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void menuDM_Khachhang_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmKhachHang")
                {
                    f.Activate();
                    return;
                }
            }

            frmKhachHang frmNH = new frmKhachHang();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void mnuDM_BanHang_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmBanVT")
                {
                    f.Activate();
                    return;
                }
            }

            frmBanVT frmBH = new frmBanVT();
            frmBH.MdiParent = this;

            frmBH.Show();
            frmBH.Top = 0;
            frmBH.Left = 0;
        }

        private void menuDMNhap_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmNhapVT")
                {
                    f.Activate();
                    return;
                }
            }

            frmNhapVT frmNH = new frmNhapVT();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            mnuDM_BanHang_Click(sender, e);
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            mnuDM_QLNhanVien_Click(sender, e);
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            menuDMNhap_Click(sender, e);
        }

        private void mnuDX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThuNho_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void btnAn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelThongTin.Visible = statusBarToolStripMenuItem.Checked;
            hienThiTT.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = colorDialog1.Color;
                panelCongCu.BackColor = colorDialog1.Color;
                panelMenu.BackColor = colorDialog1.Color;
                panelThongTin.BackColor = colorDialog1.Color;
                menuStrip.BackColor = colorDialog1.Color;
                groupBox1.BackColor = colorDialog1.Color;
                btnBanHang.BackColor = colorDialog1.Color;
                btnNhapHang.BackColor = colorDialog1.Color;
                btnQLNhanVien.BackColor = colorDialog1.Color;
                btnKho.BackColor = colorDialog1.Color;
                mnuItemHethong.BackColor = colorDialog1.Color;
            }
        }

        private void mnuDiemTK_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmThongKeHoaDon")
                {
                    f.Activate();
                    return;
                }
            }

            frmThongKeHoaDon frmNH = new frmThongKeHoaDon();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void mnu_ThongKe_DoanhThu_Click(object sender, EventArgs e)
        {
            frmInTinhDoanhThu dt = new frmInTinhDoanhThu();
            dt.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTroGiup frm = new frmTroGiup();
            frm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmThongKeHoaDonTuNgayDenNgay")
                {
                    f.Activate();
                    return;
                }
            }

            frmThongKeHoaDonTuNgayDenNgay frmNH = new frmThongKeHoaDonTuNgayDenNgay();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void mnuDM_LS_PN_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmLichSuPhieuNhap")
                {
                    f.Activate();
                    return;
                }
            }

            frmLichSuPhieuNhap frmNH = new frmLichSuPhieuNhap();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void mnuDM_LS_HDX_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmLichSuHoaDonXuat")
                {
                    f.Activate();
                    return;
                }
            }

            frmLichSuHoaDonXuat frmNH = new frmLichSuHoaDonXuat();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}

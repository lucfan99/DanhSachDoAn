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
    public partial class frmKho : Form
    {
        KETNOI conn = new KETNOI();
        public frmKho()
        {
            InitializeComponent();
        }
        public void hienThi()
        {
            string strSQL = "select KHO.MAVT,VATTU.TENVT,KHO.TONGSOLUONG,KHO.SLNHAP,KHO.SLBAN FROM KHO,VATTU WHERE KHO.MAVT = VATTU.MAVT";
            DataSet ds = conn.GrdSource(strSQL);
            dgVKho.DataSource = ds.Tables[0];
            dgVKho.Refresh();
        }
        public void HienThiTK()
        {
            string strSQL = "select KHO.MAVT,VATTU.TENVT,KHO.TONGSOLUONG,KHO.SLNHAP,KHO.SLBAN FROM KHO,VATTU WHERE KHO.MAVT = VATTU.MAVT and TENVT like '%" + txtTK.Text.Trim() + "%'";
            DataSet ds = conn.GrdSource(strSQL);
            dgVKho.DataSource = ds.Tables[0];
            dgVKho.Refresh();
        }
        private void frmKho_Load(object sender, EventArgs e)
        {
            hienThi();

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            HienThiTK();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            frmDSKho ds = new frmDSKho();
            ds.ShowDialog();
        }

        private void txtTK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HienThiTK();
            }
            catch
            { return; }
        }

        private void frmKho_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                e.Cancel = true;
        }
    }
}

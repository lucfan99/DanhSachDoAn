using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Smo;

namespace QL_VatLieuXayDung
{
    public partial class frmKetNoiHeThong : Form
    {
        
        public frmKetNoiHeThong()
        {
            InitializeComponent();
        }
        public class Luu
        {
            static public string user;
            static public string pass;
            static public string server;
        }
        private void frmKetNoiHeThong_Load(object sender, EventArgs e)
        {
            try
            {
                string[] s = { @"\SQLEXPRESS", @"\SQL2012", "" };
                foreach (var i in s)
                {
                    cboTenMay.Items.Add(SystemInformation.UserDomainName.ToString() + i);
                }

                cboTenMay.Text = "";
            }
            catch
            {
                MessageBox.Show("Lỗi");
                return;
            }

        }
        public void chayCT()
        {
            Application.Run(new frmLoad());
        }
        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTenMay.Text == "")
                {
                    MessageBox.Show("Hãy chọn Tên máy!", "Chú ý!");
                    return;
                }
                if (txtUser.Text == "")
                {
                    MessageBox.Show("Hãy nhập User!", "Chú ý!");
                    return;
                }
                if (txtPass.Text == "")
                {
                    MessageBox.Show("Hãy nhập mật khẩu!", "Chú ý!");
                    return;
                }
                Luu.user = txtUser.Text.Trim();
                Luu.pass = txtPass.Text.Trim();
                Luu.server = cboTenMay.Text.Trim();
                KETNOI conn = new KETNOI();

                conn.OpenConnection();
                conn.ClosedConnection();
                MessageBox.Show("Kết nối thành công");
                Thread t = new Thread(new ThreadStart(chayCT));
                t.Start();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Kết nối thất bại");
                return;
            }
        }

        private void cboTenMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)// nếu nhấp nút enter sẽ gọi lại click Kết nối
            {

                btnKetNoi_Click(sender, e);// gọi lại click Kết nối
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}

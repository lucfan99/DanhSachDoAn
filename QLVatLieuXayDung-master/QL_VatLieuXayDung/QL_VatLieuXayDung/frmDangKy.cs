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


namespace QL_VatLieuXayDung
{
    public partial class frmDangKy : Form
    {
        KETNOI conn = new KETNOI();
        private string MK = "";
        private string mk = frmDangNhap.LuuThongTin.mk;
        public frmDangKy()
        {
            InitializeComponent();
        }
        private string getMK(string mkh)
        {
            string mk = "";
            try
            {
                conn.OpenConnection();
                string strSQL = "SELECT * FROM TAIKHOAN WHERE TENTK ='" + frmDangNhap.ID_USER + "'";
                SqlDataReader dr = conn.getReader(strSQL);
                while (dr.Read())
                {
                    mk = dr["MK"].ToString();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            finally
            {
                conn.ClosedConnection();
            }
            return mk;
        }
        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtuser.Text.Trim();
                string pass = txtpass.Text.Trim();
                string repass = txtrepass.Text.Trim();
                MK = getMK(txtuser.Text);
                if (user == "" || pass == "" || repass =="")
                {
                    MessageBox.Show("Mật khẩu không được để trống !!");
                }
                else
                {
                    if (pass != repass)
                    {
                        MessageBox.Show("Mật khẩu nhập lại không khớp");
                    }
                    else if (user != mk)
                    {
                        MessageBox.Show("Mật khẩu không đúng !!");
                    }
                    else
                    {
                        string sql = "update TAIKHOAN SET MK ='" + pass + "' WHERE ID='" + frmDangNhap.ID_USER + "'";
                        conn.updateToDB(sql);
                        MessageBox.Show("Đổi mật khẩu thành công !!");
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Lỗi !!");

            }
        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {

        }

        

        
        private void txtpass_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtrepass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtuser_Click(object sender, EventArgs e)
        {

        }

        private void txtuser_MouseClick_1(object sender, MouseEventArgs e)
        {
            txtuser.Clear();
            txtuser.Focus();
            txtuser.UseSystemPasswordChar = true;
        }

        private void txtpass_TextChanged_1(object sender, EventArgs e)
        {
            txtpass.UseSystemPasswordChar = true;
        }

        private void txtpass_MouseClick_1(object sender, MouseEventArgs e)
        {
            txtpass.Clear();
            txtpass.Focus();
            txtpass.UseSystemPasswordChar = true;
        }

        private void txtrepass_MouseClick_1(object sender, MouseEventArgs e)
        {
            txtrepass.Clear();
            txtrepass.Focus();
            txtrepass.UseSystemPasswordChar = true;
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            txtuser.UseSystemPasswordChar = true;
        }

        private void txtrepass_TextChanged_1(object sender, EventArgs e)
        {
            txtrepass.UseSystemPasswordChar = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtrepass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)// nếu nhấp nút enter sẽ gọi lại click đổi mật khẩu
            {

                label2_Click(sender, e);// gọi lại click đổi mật khẩu
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}

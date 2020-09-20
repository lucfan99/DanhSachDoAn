using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace QL_VatLieuXayDung
{
    
    public partial class frmDangNhap : Form
    {
        KETNOI conn = new KETNOI();
        public static string ID_USER = "";
        public static string ChucVu = "";
        public static string Quyen = "";
        
        public frmDangNhap()
        {

            InitializeComponent();

        }
        public class LuuThongTin
        {
            static public string mk;
        }
        public void chayfrmMain()
        {
            Application.Run(new frmTrangChu());
        }
        private string getID(string username, string pass)
        {
            string id = "";
            try
            {
                conn.OpenConnection();
                string strSQL = "SELECT * FROM TAIKHOAN WHERE TENTK ='" + username + "' and MK='" + pass + "'";
                SqlDataReader dr = conn.getReader(strSQL);
                while (dr.Read())
                {
                    id = dr["ID"].ToString();
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
            return id;
        }
        private string getQuyen(string user, string pass)
        {
            string quyen = "";
            try
            {
                conn.OpenConnection();
                string strSQL = "SELECT * FROM TAIKHOAN WHERE TENTK ='" + user + "' and MK='" + pass + "'";
                SqlDataReader dr = conn.getReader(strSQL);
                while (dr.Read())
                {
                    quyen = dr["QUYEN"].ToString();
                }
                dr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            finally
            {
                conn.ClosedConnection();
            }

            return quyen.Trim();
        }
        private string getChucVu(string user, string pass)
        {
            string cv = "";
            try
            {
                conn.OpenConnection();
                string strSQL = "SELECT * FROM TAIKHOAN TK, NHANVIEN NV WHERE TK.MANV = NV.MANV AND TENTK ='" + user + "' and MK='" + pass + "'";
                SqlDataReader dr = conn.getReader(strSQL);
                while (dr.Read())
                {
                    cv = dr["CHUCVU"].ToString();
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
            return cv;
        }
        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                
                LuuThongTin.mk = txtpass.Text;
                ID_USER = getID(txtuser.Text, txtpass.Text);
                ChucVu = getChucVu(txtuser.Text, txtpass.Text);
                Quyen = getQuyen(txtuser.Text, txtpass.Text);

                if (ID_USER != "")
                {
                    Thread t = new Thread(new ThreadStart(chayThongBao));
                    t.Start();
                    Thread.Sleep(300);
                    t.Abort();
                    Thread k = new Thread(new ThreadStart(chayfrmMain));
                    frmDangNhap_Load(sender, e);
                    k.Start();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tài khoảng và mật khẩu không đúng !");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi kêt nối dữ liệu!!");
                return;
            }
        }
        public void chayThongBao()
        {
            Application.Run(new frmThongBaoThanhCong());
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            txtpass.UseSystemPasswordChar = true;
        }

        private void txtuser_MouseClick(object sender, MouseEventArgs e)
        {
            txtuser.Clear();
            txtuser.Focus();
        }

        private void txtpass_MouseClick(object sender, MouseEventArgs e)
        {
            txtpass.Clear();
            txtpass.Focus();
            txtpass.UseSystemPasswordChar = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rectangleShape1_Click(object sender, EventArgs e)
        {
            label2_Click(sender, e);
            
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)// nếu nhấp nút enter sẽ gọi lại click đăng nhập
            {

                label2_Click(sender, e);// gọi lại click đăng nhập 
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LuuThongTin.server = comboBox1.Text.Trim();
        }

        
    }
}

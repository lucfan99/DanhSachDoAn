using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_VatLieuXayDung
{
    public partial class frmKhachHang : Form
    {
        KETNOI conn = new KETNOI();
        SqlDataAdapter ada_KH = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmKhachHang()
        {
            InitializeComponent();
        }
        public void taoMaKH()
        {
            string sql = "SELECT MAX(RIGHT(MAKH, 8)) FROM KHACHHANG";
            SqlDataReader dr = conn.getReader(sql);
            string ma = "";
            while (dr.Read())
            {
                ma = dr[""].ToString();
            }
            dr.Close();
            conn.ClosedConnection();
            if (ma == "")
            {
                txtMaKH.Text = "KH00000001";
            }
            else
            {
                int m = int.Parse(ma);
                if (m >= 0 && m < 9)
                {
                    ma = "KH0000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                if (m >= 9)
                {
                    ma = "KH000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                txtMaKH.Text = ma;
            }
        }
        public void createTable_KhachHang()
        {
            // tao 1 table tren Dataset
            string strSQL = "SELECT * FROM KHACHHANG";
            ada_KH = conn.getDataAdapter(strSQL, "KHACHHANG");
            primaryKey[0] = conn.Dset.Tables["KHACHHANG"].Columns["MAKH"];
            conn.Dset.Tables["KHACHHANG"].PrimaryKey = primaryKey;// thiet lap khoa chinh cho bang vat tu
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            taoMaKH();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtHoTen.Focus();
            groupBox2.Enabled = true;
            btnThemmoi.Enabled = true;
            btnTaoMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;

        }
        public void load_Begin()
        {
            groupBox2.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThemmoi.Enabled = false;
        }
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            load_Begin();
            createTable_KhachHang();
            dgVKhachHang.DataSource = conn.Dset.Tables["KHACHHANG"];
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            try
            {
                string makh = txtMaKH.Text.Trim();
                string tenkh = txtHoTen.Text.Trim();
                string diachi = txtDiaChi.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                // kiem tra hop le nhap lieu
                if (txtHoTen.Text == "")
                {
                    MessageBox.Show("Hãy nhập Họ tên khách hàng!", "Chú ý!");
                    txtHoTen.Focus();
                    return;
                }
                if (txtDiaChi.Text == "")
                {
                    MessageBox.Show("Hãy nhập Địa chỉ!", "Chú ý!");
                    txtDiaChi.Focus();
                    return;
                }
                if (txtSDT.Text == "")
                {
                    MessageBox.Show("Hãy nhập số điện thoại!", "Chú ý!");
                    txtSDT.Focus();
                    return;
                }
                if (IsValidPhone(txtSDT.Text.Trim()) == false)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ!!\n Xin vui lòng nhập lại!", "Chú ý");
                    txtSDT.Clear();
                    txtSDT.Focus();
                    return;
                }
                if (txtSDT.TextLength != 10)
                {
                    MessageBox.Show("Số điện thoại phải đủ 10 số!!\n Xin vui lòng nhập lại!", "Chú ý");
                    txtSDT.Clear();
                    txtSDT.Focus();
                    return;
                }
                DataRow dr = conn.Dset.Tables["KHACHHANG"].Rows.Find(makh);
                if (dr != null)
                {
                    MessageBox.Show("Khách  này đã tồn tại");
                    return;
                }
                DataRow them = conn.Dset.Tables["KHACHHANG"].NewRow();
                them["MAKH"] = makh;
                them["TENKH"] = tenkh;
                them["DIACHI"] = diachi;
                them["SDT"] = sdt;
                conn.Dset.Tables["KHACHHANG"].Rows.Add(them);
                SqlCommandBuilder builder = new SqlCommandBuilder(ada_KH);
                ada_KH.Update(conn.Dset, "KHACHHANG");
                MessageBox.Show("Thêm thành công Khách hàng " + tenkh);
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnTaoMoi.Enabled = true;
                btnThemmoi.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Loi!!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string makh = txtMaKH.Text.Trim();
                    string tenkh = txtHoTen.Text.Trim();
                    string diachi = txtDiaChi.Text.Trim();
                    string sdt = txtSDT.Text.Trim();
                    // kiem tra hop le nhap lieu
                    if (txtHoTen.Text == "")
                    {
                        MessageBox.Show("Hãy nhập Họ tên khách hàng!", "Chú ý!");
                        txtHoTen.Focus();
                        return;
                    }
                    if (txtDiaChi.Text == "")
                    {
                        MessageBox.Show("Hãy nhập Địa chỉ!", "Chú ý!");
                        txtDiaChi.Focus();
                        return;
                    }
                    if (txtSDT.Text == "")
                    {
                        MessageBox.Show("Hãy nhập số điện thoại!", "Chú ý!");
                        txtSDT.Focus();
                        return;
                    }
                    if (IsValidPhone(txtSDT.Text.Trim()) == false)
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ!!\n Xin vui lòng nhập lại!", "Chú ý");
                        txtSDT.Clear();
                        txtSDT.Focus();
                        return;
                    }
                    if (txtSDT.TextLength != 10)
                    {
                        MessageBox.Show("Số điện thoại phải đủ 10 số!!\n Xin vui lòng nhập lại!", "Chú ý");
                        txtSDT.Clear();
                        txtSDT.Focus();
                        return;
                    }
                
                    DataRow dr = conn.Dset.Tables["KHACHHANG"].Rows.Find(makh);
                    if (dr == null)
                    {
                        MessageBox.Show("Khách  này không tồn tại");
                        return;
                    }

                    dr["TENKH"] = tenkh;
                    dr["DIACHI"] = diachi;
                    dr["SDT"] = sdt;

                    SqlCommandBuilder builder = new SqlCommandBuilder(ada_KH);
                    ada_KH.Update(conn.Dset, "KHACHHANG");
                    MessageBox.Show("Sửa thành công Khách hàng " + tenkh);
                    load_Begin();
                }
                catch
                {
                    MessageBox.Show("Lỗi!!");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string makh = txtMaKH.Text.Trim();
                    string sql = "select count(*) from HOADON where MAKH='" + makh + "'";
                    bool kq = conn.checkForExistence(sql);
                    if (kq == true)
                    {
                        MessageBox.Show("Dữ liệu này đang được sử dụng!!", "Chú ý!");
                        return;
                    }
                    else
                    {
                        DataRow dr = conn.Dset.Tables["KHACHHANG"].Rows.Find(makh);
                        if (dr == null)
                        {
                            MessageBox.Show("Khách  này không tồn tại");
                            return;
                        }
                        dr.Delete();
                        SqlCommandBuilder builder = new SqlCommandBuilder(ada_KH);
                        ada_KH.Update(conn.Dset, "KHACHHANG");
                        MessageBox.Show("Xóa thành công Khách hàng " + makh);
                        load_Begin();
                    }

                }
                catch
                {
                    MessageBox.Show("Loi!!");
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgVKhachHang_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                txtMaKH.Text = dgVKhachHang.Rows[index].Cells[0].Value.ToString();
                txtHoTen.Text = dgVKhachHang.Rows[index].Cells[1].Value.ToString();
                txtDiaChi.Text = dgVKhachHang.Rows[index].Cells[2].Value.ToString();
                txtSDT.Text = dgVKhachHang.Rows[index].Cells[3].Value.ToString();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThemmoi.Enabled = false;
                btnTaoMoi.Enabled = true;
                groupBox2.Enabled = true;
            }
            catch
            {
                return;
            }
        }
        public void HienThi()
        {
            string strSQL = "select * FROM KHACHHANG WHERE TENKH like N'%" + txtTK.Text.Trim() + "%'";
            DataSet ds = conn.GrdSource(strSQL);
            dgVKhachHang.DataSource = ds.Tables[0];
            dgVKhachHang.Refresh();
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            HienThi();
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ctr = (Control)sender;
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(ctr, "Bạn chỉ có thể nhập số");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
        public static bool IsValidPhone(string value)
        {
            value = value ?? string.Empty;
            string pattern = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";
            Regex re = new Regex(pattern);
            if (re.IsMatch(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnInHD_Click(object sender, EventArgs e)
        {
            frmInDSKhachHang ds = new frmInDSKhachHang();
            ds.ShowDialog();
        }

        private void txtTK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HienThi();
            }
            catch
            {
                return;
            }
        }

        private void frmKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                e.Cancel = true;
        }
    }
}

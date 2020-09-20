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
    public partial class frmNhaCungCap : Form
    {
        KETNOI conn = new KETNOI();
        SqlDataAdapter ada_NCC = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmNhaCungCap()
        {
            InitializeComponent();
        }
        public void taoMaNCC()
        {
            string sql = "SELECT MAX(RIGHT(MANCC, 7)) FROM NHACUNGCAP";
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
                txtMaNCC.Text = "NCC0000001";
            }
            else
            {
                int m = int.Parse(ma);
                if (m >= 0 && m < 9)
                {
                    ma = "NCC000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                if (m >= 9)
                {
                    ma = "NCC00000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                txtMaNCC.Text = ma;
            }
        }
        public void createTable_NCC()
        {
            // tao 1 table tren Dataset
            string strSQL = "SELECT * FROM NHACUNGCAP";
            ada_NCC = conn.getDataAdapter(strSQL, "NHACUNGCAP");
            primaryKey[0] = conn.Dset.Tables["NHACUNGCAP"].Columns["MANCC"];
            conn.Dset.Tables["NHACUNGCAP"].PrimaryKey = primaryKey;// thiet lap khoa chinh cho bang nhà cung cấp
        }
        public void load_Begin()
        {
            groupBox1.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            load_Begin();
            createTable_NCC();

            dgVNhaCC.DataSource = conn.Dset.Tables["NHACUNGCAP"];

        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            taoMaNCC();
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtEmail.Clear();
            txtFax.Clear();
            txtTenNCC.Focus();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            groupBox1.Enabled = true;
            btnTaoMoi.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string mancc = txtMaNCC.Text.Trim();
                string tenncc = txtTenNCC.Text.Trim();
                string diachi = txtDiaChi.Text.Trim();
                string dienthoai = txtDienThoai.Text.Trim();
                string fax = txtFax.Text.Trim();
                string email = txtEmail.Text.Trim();
                // kiem tra hop le nhap lieu
                if (txtTenNCC.Text == "")
                {
                    MessageBox.Show("Hãy nhập Tên nhà cung cấp", "Chú ý!");
                    txtTenNCC.Focus();
                    return;
                }
                if (txtDiaChi.Text == "")
                {
                    MessageBox.Show("Hãy nhập Địa chỉ!", "Chú ý!");
                    txtDiaChi.Focus();
                    return;
                }
                if (txtDienThoai.Text == "")
                {
                    MessageBox.Show("Hãy nhập Số điện thoại!", "Chú ý!");
                    txtDienThoai.Focus();
                    return;
                }
                if (txtFax.Text == "")
                {
                    MessageBox.Show("Hãy nhập Fax!", "Chú ý!");
                    txtFax.Focus();
                    return;
                }
                if (txtEmail.Text == "")
                {
                    MessageBox.Show("Hãy nhập Email!", "Chú ý!");
                    txtEmail.Focus();
                    return;
                }
                if (isEmail(txtEmail.Text.Trim()) == false)
                {
                    MessageBox.Show("Email không hợp lệ!!\n Xin vui lòng nhập lại!", "Chú ý");
                    txtEmail.Focus();
                    return;
                }

                if (IsValidPhone(txtDienThoai.Text.Trim()) == false)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ!!\n Xin vui lòng nhập lại!", "Chú ý");
                    txtDienThoai.Clear();
                    txtDienThoai.Focus();
                    return;
                }
                if (txtDienThoai.TextLength != 10)
                {
                    MessageBox.Show("Số điện thoại phải đủ 10 số!!\n Xin vui lòng nhập lại!", "Chú ý");
                    txtDienThoai.Clear();
                    txtDienThoai.Focus();
                    return;
                }
                DataRow dr = conn.Dset.Tables["NHACUNGCAP"].Rows.Find(mancc);
                if (dr != null)
                {
                    MessageBox.Show("Nhà cung cấp  " + mancc + " đã tồn tại");
                    return;
                }
                DataRow newRow = conn.Dset.Tables["NHACUNGCAP"].NewRow();
                newRow["MANCC"] = mancc;
                newRow["TENNCC"] = tenncc;
                newRow["DIACHI"] = diachi;
                newRow["DIENTHOAI"] = dienthoai;
                newRow["FAX"] = fax;
                newRow["EMAIL"] = email;

                conn.Dset.Tables["NHACUNGCAP"].Rows.Add(newRow);

                // dong bo du lieu tu dataset len server
                SqlCommandBuilder builder = new SqlCommandBuilder(ada_NCC);
                ada_NCC.Update(conn.Dset, "NHACUNGCAP");
                MessageBox.Show("Thêm thành công Nhà CC " + mancc);
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
                btnTaoMoi.Enabled = true;
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
                    string mancc = txtMaNCC.Text.Trim();
                    string tenncc = txtTenNCC.Text.Trim();
                    string diachi = txtDiaChi.Text.Trim();
                    string dienthoai = txtDienThoai.Text.Trim();
                    string fax = txtFax.Text.Trim();
                    string email = txtEmail.Text.Trim();
                    // kiem tra hop le nhap lieu
                    if (txtTenNCC.Text == "")
                    {
                        MessageBox.Show("Hãy nhập Tên nhà cung cấp", "Chú ý!");
                        txtTenNCC.Focus();
                        return;
                    }
                    if (txtDiaChi.Text == "")
                    {
                        MessageBox.Show("Hãy nhập Địa chỉ!", "Chú ý!");
                        txtDiaChi.Focus();
                        return;
                    }
                    if (txtDienThoai.Text == "")
                    {
                        MessageBox.Show("Hãy nhập Số điện thoại!", "Chú ý!");
                        txtDienThoai.Focus();
                        return;
                    }
                    if (txtFax.Text == "")
                    {
                        MessageBox.Show("Hãy nhập Fax!", "Chú ý!");
                        txtFax.Focus();
                        return;
                    }
                    if (txtEmail.Text == "")
                    {
                        MessageBox.Show("Hãy nhập Email!", "Chú ý!");
                        txtEmail.Focus();
                        return;
                    }
                    if (isEmail(txtEmail.Text.Trim()) == false)
                    {
                        MessageBox.Show("Email không hợp lệ!!\n Xin vui lòng nhập lại!", "Chú ý");
                        txtEmail.Focus();
                        return;
                    }

                    if (IsValidPhone(txtDienThoai.Text.Trim()) == false)
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ!!\n Xin vui lòng nhập lại!", "Chú ý");
                        txtDienThoai.Clear();
                        txtDienThoai.Focus();
                        return;
                    }
                    if (txtDienThoai.TextLength != 10)
                    {
                        MessageBox.Show("Số điện thoại phải đủ 10 số!!\n Xin vui lòng nhập lại!", "Chú ý");
                        txtDienThoai.Clear();
                        txtDienThoai.Focus();
                        return;
                    }
                    DataRow dr = conn.Dset.Tables["NHACUNGCAP"].Rows.Find(mancc);
                    if (dr == null)
                    {
                        MessageBox.Show("Nhà cung cấp  " + mancc + " không tồn tại");
                        return;
                    }

                    dr["TENNCC"] = tenncc;
                    dr["DIACHI"] = diachi;
                    dr["DIENTHOAI"] = dienthoai;
                    dr["FAX"] = fax;
                    dr["EMAIL"] = email;



                    // dong bo du lieu tu dataset len server
                    SqlCommandBuilder builder = new SqlCommandBuilder(ada_NCC);
                    ada_NCC.Update(conn.Dset, "NHACUNGCAP");
                    MessageBox.Show("Sửa thành công Nhà CC " + mancc);
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
                    string mancc = txtMaNCC.Text.Trim();
                    string sql = "select count(*) from PHIEUNHAP where MANCC='" + mancc + "'";
                    bool kq = conn.checkForExistence(sql);
                    if (kq == true)
                    {
                        MessageBox.Show("Dữ liệu này đang được sử dụng!!", "Chú ý!");
                        return;
                    }
                    else
                    {
                        DataRow dr = conn.Dset.Tables["NHACUNGCAP"].Rows.Find(mancc);
                        if (dr == null)
                        {
                            MessageBox.Show("Nhà cung cấp  " + mancc + " không tồn tại");
                            return;
                        }

                        dr.Delete();



                        // dong bo du lieu tu dataset len server
                        SqlCommandBuilder builder = new SqlCommandBuilder(ada_NCC);
                        ada_NCC.Update(conn.Dset, "NHACUNGCAP");
                        MessageBox.Show("Xóa thành công Nhà CC " + mancc);
                        load_Begin();
                    }

                }
                catch
                {
                    MessageBox.Show("Loi!!");
                }
            }
        }
        private void dgVNhaCC_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index;
                index = e.RowIndex;
                txtMaNCC.Text = dgVNhaCC.Rows[index].Cells[0].Value.ToString();
                txtTenNCC.Text = dgVNhaCC.Rows[index].Cells[1].Value.ToString();
                txtDiaChi.Text = dgVNhaCC.Rows[index].Cells[2].Value.ToString();
                txtDienThoai.Text = dgVNhaCC.Rows[index].Cells[3].Value.ToString();
                txtEmail.Text = dgVNhaCC.Rows[index].Cells[5].Value.ToString();
                txtFax.Text = dgVNhaCC.Rows[index].Cells[4].Value.ToString();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
                groupBox1.Enabled = true;
            }
            catch
            {
                return;
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            frmInDSNhaCC ds = new frmInDSNhaCC();
            ds.ShowDialog();
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
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
        public void hienThiTK()
        {
            try
            {
                string sql = "select * from NHACUNGCAP where TENNCC like N'%" + txtTK.Text.Trim() + "%'";
                DataSet ds = conn.GrdSource(sql);
                dgVNhaCC.DataSource = ds.Tables[0];
                dgVNhaCC.Refresh();
            }
            catch
            {
                return;
            }
        }
        private void btnTK_Click(object sender, EventArgs e)
        {
            hienThiTK();
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
        public static bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTK_TextChanged(object sender, EventArgs e)
        {
            hienThiTK();
        }

        private void frmNhaCungCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                e.Cancel = true;
        }
    }
}

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
    public partial class frmQuanLyNhanVien : Form
    {
        KETNOI conn = new KETNOI();
        SqlDataAdapter ada_NhanVien = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmQuanLyNhanVien()
        {
            InitializeComponent();
        }
        public void taoMaNV()
        {
            string sql = "SELECT MAX(RIGHT(MANV, 7)) FROM NHANVIEN";
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
                txtMaNV.Text = "NV00000001";
            }
            else
            {
                int m = int.Parse(ma);
                if (m >= 0 && m < 9)
                {
                    ma = "NV0000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                if (m >= 9)
                {
                    ma = "NV000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                txtMaNV.Text = ma;
            }
        }
        public void createTable_NHAVIEN()
        {
            // tao 1 table tren Dataset
            string strSQL = "SELECT * FROM NHANVIEN";
            ada_NhanVien = conn.getDataAdapter(strSQL, "NHANVIEN");
            primaryKey[0] = conn.Dset.Tables["NHANVIEN"].Columns["MANV"];
            conn.Dset.Tables["NHANVIEN"].PrimaryKey = primaryKey;// thiet lap khoa chinh cho bang nhà cung cấp
        }
        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            try
            {
                string manv = txtMaNV.Text.Trim();
                string hoten = txtHoTen.Text.Trim();
                string ngaysinh = DateTime.ParseExact(pckNgaySinh.Text.Trim(), "MM/dd/yyyy", null).ToString("yyyy/MM/dd");
                string gioitinh = cboGioiTinh.Text.Trim();
                string diachi = txtDiaChi.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                string email = txtEmail.Text.Trim();
                string chucvu = cboChucVu.Text.Trim();

                // kiem tra hop le nhap lieu
                if (txtHoTen.Text == "")
                {
                    MessageBox.Show("Hãy nhập họ tên!", "Chú ý!");
                    txtHoTen.Focus();
                    return;
                }
                if (pckNgaySinh.Text == "")
                {
                    MessageBox.Show("Hãy nhập ngày sinh!", "Chú ý!");
                    return;
                }
                if (cboGioiTinh.Text == "")
                {
                    MessageBox.Show("Hãy chọn giới tính!", "Chú ý!");
                    return;
                }
                if (txtDiaChi.Text == "")
                {
                    MessageBox.Show("Hãy nhập địa chỉ!", "Chú ý!");
                    txtDiaChi.Focus();
                    return;
                }
                if (txtSDT.Text == "")
                {
                    MessageBox.Show("Hãy nhập số điện thoại!", "Chú ý!");
                    txtSDT.Focus();
                    return;
                }
                if (txtEmail.Text == "")
                {
                    MessageBox.Show("Hãy nhập email!", "Chú ý!");
                    txtEmail.Focus();
                    return;
                }
                if (cboChucVu.Text == "")
                {
                    MessageBox.Show("Hãy nhập chức vụ!", "Chú ý!");
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
                if (isEmail(txtEmail.Text.Trim()) == false)
                {
                    MessageBox.Show("Email không hợp lệ!!\n Xin vui lòng nhập lại!", "Chú ý");
                    txtEmail.Clear();
                    txtEmail.Focus();
                    return;
                }

                DataRow dr = conn.Dset.Tables["NHANVIEN"].Rows.Find(manv);
                if (dr != null)
                {
                    MessageBox.Show("Nhân viên  này đã tồn tại");
                    return;
                }
                DataRow them = conn.Dset.Tables["NHANVIEN"].NewRow();
                them["MANV"] = manv;
                them["TENNV"] = hoten;
                them["NGAYSINH"] = ngaysinh;
                them["GIOITINH"] = gioitinh;
                them["DIACHI"] = diachi;
                them["SDT"] = sdt;
                them["EMAIL"] = email;
                them["CHUCVU"] = chucvu;
                conn.Dset.Tables["NHANVIEN"].Rows.Add(them);
                SqlCommandBuilder builder = new SqlCommandBuilder(ada_NhanVien);
                ada_NhanVien.Update(conn.Dset, "NHANVIEN");
                MessageBox.Show("Thêm thành công Nhân viên " + hoten);
                btnThemmoi.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                return;
            }
            catch
            {
                MessageBox.Show("Lỗi định dạng ngày !!\n Xin vui lòng cài đặt lại ngày hệ thống tháng/ngày/năm!!");
                return;
            }
        }
        public void load_Begin()
        {
            tableLayoutPanel6.Enabled = false;
            btnThemmoi.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }
        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            load_Begin();
            createTable_NHAVIEN();
            dgVNhanVien.DataSource = conn.Dset.Tables["NHANVIEN"];


        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            taoMaNV();
            txtHoTen.Clear();
            pckNgaySinh.Text = "";
            cboGioiTinh.Text = "";
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            cboChucVu.Text = "";
            btnThemmoi.Enabled = true;
            btnTaoMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            tableLayoutPanel6.Enabled = true;
        }
        public void HienThi()
        {
            try
            {
                string sql = "select * from NHANVIEN where TENNV like N'%" + txtTK.Text + "%'";
                DataSet ds = conn.GrdSource(sql);
                dgVNhanVien.DataSource = ds.Tables[0];
                dgVNhanVien.Refresh();
                return;
            }
            catch
            {
                MessageBox.Show("Lỗi !!|n Xin vui lòng thử lại sau hoặc kiểm tra lại!!");
                return;
            }
        }
        private void cboTenNV_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa nhân viên này ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string manv = txtMaNV.Text.Trim();
                    string sql = "select count(*) from TAIKHOAN where MANV ='" + manv + "'";
                    bool kq = conn.checkForExistence(sql);
                    if (kq == true)
                    {
                        MessageBox.Show("Dữ liệu này đang được sử dụng không thể xóa!!", "Chú ý!");
                        return;
                    }
                    else
                    {
                        DataRow dr = conn.Dset.Tables["NHANVIEN"].Rows.Find(manv);
                        if (dr == null)
                        {
                            MessageBox.Show("Nhân viên  này không tồn tại");
                            return;
                        }

                        dr.Delete();
                        SqlCommandBuilder builder = new SqlCommandBuilder(ada_NhanVien);
                        ada_NhanVien.Update(conn.Dset, "NHANVIEN");
                        MessageBox.Show("Xóa thành công");
                        load_Begin();
                    }
                    return;
                }
                catch
                {
                    MessageBox.Show("Loi!!");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string manv = txtMaNV.Text.Trim();
                    string hoten = txtHoTen.Text.Trim();
                    string ngaysinh = DateTime.ParseExact(pckNgaySinh.Text.Trim(), "MM/dd/yyyy", null).ToString("yyyy/MM/dd");
                    string gioitinh = cboGioiTinh.Text.Trim();
                    string diachi = txtDiaChi.Text.Trim();
                    string sdt = txtSDT.Text.Trim();
                    string email = txtEmail.Text.Trim();
                    string chucvu = cboChucVu.Text.Trim();

                    // kiem tra hop le nhap lieu
                    if (txtHoTen.Text == "")
                    {
                        MessageBox.Show("Hãy nhập họ tên!", "Chú ý!");
                        txtHoTen.Focus();
                        return;
                    }
                    if (pckNgaySinh.Text == "")
                    {
                        MessageBox.Show("Hãy nhập ngày sinh!", "Chú ý!");
                        return;
                    }
                    if (cboGioiTinh.Text == "")
                    {
                        MessageBox.Show("Hãy chọn giới tính!", "Chú ý!");
                        return;
                    }
                    if (txtDiaChi.Text == "")
                    {
                        MessageBox.Show("Hãy nhập địa chỉ!", "Chú ý!");
                        txtDiaChi.Focus();
                        return;
                    }
                    if (txtSDT.Text == "")
                    {
                        MessageBox.Show("Hãy nhập số điện thoại!", "Chú ý!");
                        txtSDT.Focus();
                        return;
                    }
                    if (txtEmail.Text == "")
                    {
                        MessageBox.Show("Hãy nhập email!", "Chú ý!");
                        txtEmail.Focus();
                        return;
                    }
                    if (cboChucVu.Text == "")
                    {
                        MessageBox.Show("Hãy nhập chức vụ!", "Chú ý!");
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
                    if (isEmail(txtEmail.Text.Trim()) == false)
                    {
                        MessageBox.Show("Email không hợp lệ!!\n Xin vui lòng nhập lại!", "Chú ý");
                        txtEmail.Clear();
                        txtEmail.Focus();
                        return;
                    }
                    DataRow dr = conn.Dset.Tables["NHANVIEN"].Rows.Find(manv);
                    if (dr == null)
                    {
                        MessageBox.Show("Nhân viên  này không tồn tại");
                        return;
                    }

                    dr["TENNV"] = hoten;
                    dr["NGAYSINH"] = ngaysinh;
                    dr["GIOITINH"] = gioitinh;
                    dr["DIACHI"] = diachi;
                    dr["SDT"] = sdt;
                    dr["EMAIL"] = email;
                    dr["CHUCVU"] = chucvu;
                    SqlCommandBuilder builder = new SqlCommandBuilder(ada_NhanVien);
                    ada_NhanVien.Update(conn.Dset, "NHANVIEN");
                    MessageBox.Show("Sửa thành công Nhân viên " + hoten);
                    return;
                }
                catch
                {
                    MessageBox.Show("Lỗi định dạng ngày !!\n Xin vui lòng cài đặt lại ngày hệ thống tháng/ngày/năm!!");
                    return;
                }
            }
        }


        private void dgVNhanVien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                txtMaNV.Text = dgVNhanVien.Rows[index].Cells[0].Value.ToString();
                txtHoTen.Text = dgVNhanVien.Rows[index].Cells[1].Value.ToString();
                pckNgaySinh.Text = dgVNhanVien.Rows[index].Cells[2].Value.ToString();
                cboGioiTinh.Text = dgVNhanVien.Rows[index].Cells[3].Value.ToString();
                txtDiaChi.Text = dgVNhanVien.Rows[index].Cells[4].Value.ToString();
                txtSDT.Text = dgVNhanVien.Rows[index].Cells[5].Value.ToString();
                txtEmail.Text = dgVNhanVien.Rows[index].Cells[6].Value.ToString();
                cboChucVu.Text = dgVNhanVien.Rows[index].Cells[7].Value.ToString();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThemmoi.Enabled = false;
                btnTaoMoi.Enabled = true;
                tableLayoutPanel6.Enabled = true;
                return;
            }
            catch
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmInDSNhanVien dsnv = new frmInDSNhanVien();
            dsnv.ShowDialog();
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
        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTK_TextChanged(object sender, EventArgs e)
        {
            HienThi();
        }

        private void frmQuanLyNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                e.Cancel = true;
        }
    }
}

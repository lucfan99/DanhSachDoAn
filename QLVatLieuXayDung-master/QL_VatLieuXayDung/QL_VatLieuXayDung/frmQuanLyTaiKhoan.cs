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
    public partial class frmQuanLyTaiKhoan : Form
    {
        KETNOI conn = new KETNOI();
        SqlDataAdapter ada_TAIKHOAN = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        SqlDataAdapter ada_TAIKHOAN_NHANVIEN = new SqlDataAdapter();
        DataColumn[] primaryKey1 = new DataColumn[1];
        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
        }
        public void taoMaTK()
        {
            string sql = "SELECT MAX(RIGHT(ID, 7)) FROM TAIKHOAN";
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
                txtID.Text = "ID_0000001";
            }
            else
            {
                int m = int.Parse(ma);
                if (m >= 0 && m < 9)
                {
                    ma = "ID_000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                if (m >= 9)
                {
                    ma = "ID_00000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                txtID.Text = ma;
            }
        }
        public void createTable_TAIKHOAN()
        {
            // tao 1 table tren Dataset
            string strSQL = "SELECT * FROM TAIKHOAN";
            ada_TAIKHOAN = conn.getDataAdapter(strSQL, "TAIKHOAN");
            primaryKey[0] = conn.Dset.Tables["TAIKHOAN"].Columns["ID"];
            conn.Dset.Tables["TAIKHOAN"].PrimaryKey = primaryKey;// thiet lap khoa chinh cho ID
        }
        public void createTable_TK_NV()
        {
            string s = "select ID[ID],TENTK[Tên TK],MK[Mật khẩu],QUYEN[Quyền],tk.MANV[Mã NV],TENNV[Tên NV],CHUCVU[Chức vụ] from TAIKHOAN tk, NHANVIEN nv where tk.MANV = nv.MANV";
            ada_TAIKHOAN_NHANVIEN = conn.getDataAdapter(s, "TAIKHOAN,NHANVIEN");
            primaryKey1[0] = conn.Dset.Tables["TAIKHOAN,NHANVIEN"].Columns["ID"];
            conn.Dset.Tables["TAIKHOAN,NHANVIEN"].PrimaryKey = primaryKey1;
        }
        public void load_Begin()
        {
            groupBox1.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThemmoi.Enabled = false;

        }
        private void frmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            load_Begin();

            createTable_TAIKHOAN();
            createTable_TK_NV();
            dgrLogin.DataSource = conn.Dset.Tables["TAIKHOAN,NHANVIEN"];
            loadNV();
            
        }
        public void loadNV()
        {
            string sql = "select * from NHANVIEN";
            DataSet dt = conn.GrdSource(sql);
            cboNV.DataSource = dt.Tables[0];
            cboNV.DisplayMember = "TENNV";
            cboNV.ValueMember = "MANV";
            cboNV.Text = "";
            cboNV.Refresh();
        }
        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            taoMaTK();
            txtTaikhoan.Clear();
            txtMK.Clear();
            cboNV.Text = "";
            cboQuyen.Text = "";
            txtTaikhoan.Focus();
            groupBox1.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThemmoi.Enabled = true;
            btnTaoMoi.Enabled = false;
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text.Trim();
                string tentk = txtTaikhoan.Text.Trim();
                string mk = txtMK.Text.Trim();
                string quyen = cboQuyen.Text.Trim();
                string manv = cboNV.SelectedValue.ToString();

                // kiem tra hop le nhap lieu

                if (txtTaikhoan.Text == "")
                {
                    MessageBox.Show("Hãy nhập tên tài khoản!", "Chú ý!");
                    return;
                }
                if (txtMK.Text == "")
                {
                    MessageBox.Show("Hãy nhập mật khẩu!", "Chú ý!");
                    return;
                }
                if (cboQuyen.Text == "")
                {
                    MessageBox.Show("Hãy chọn quyền truy cập!", "Chú ý!");
                    return;
                }
                if (cboNV.Text == "")
                {
                    MessageBox.Show("Hãy chọn nhân viên được cấp!", "Chú ý!");
                    return;
                }
                string sql = "select count(*) from TAIKHOAN where MANV = '" + manv + "'";
                bool kq = conn.checkForExistence(sql);
                if (kq == true)
                {
                    MessageBox.Show("Nhân viên này đã có tài khoản rồi!!");
                    cboNV.Text = "";
                    return;
                }
                else
                {
                    DataRow dr = conn.Dset.Tables["TAIKHOAN"].Rows.Find(id);
                    if (dr != null)
                    {
                        MessageBox.Show("Tài khoản  này đã tồn tại");
                        return;
                    }
                    DataRow them = conn.Dset.Tables["TAIKHOAN"].NewRow();
                    them["ID"] = id;
                    them["TENTK"] = tentk;
                    them["MK"] = mk;
                    them["QUYEN"] = quyen;
                    them["MANV"] = manv;
                    conn.Dset.Tables["TAIKHOAN"].Rows.Add(them);

                    SqlCommandBuilder builder = new SqlCommandBuilder(ada_TAIKHOAN);
                    ada_TAIKHOAN.Update(conn.Dset, "TAIKHOAN");

                    dgrLogin.ClearSelection();
                    createTable_TK_NV();
                    dgrLogin.DataSource = conn.Dset.Tables["TAIKHOAN,NHANVIEN"];
                    MessageBox.Show("Thêm thành công tài khoản" + tentk);
                    btnThemmoi.Enabled = false;
                    btnXoa.Enabled = true;
                    btnSua.Enabled = true;
                    btnTaoMoi.Enabled = true;
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Lỗi!!\n Xin hãy kiểm tra lại!!");
                return;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string id = txtID.Text.Trim();
                    string tentk = txtTaikhoan.Text.Trim();
                    string mk = txtMK.Text.Trim();
                    string quyen = cboQuyen.Text.Trim();
                    string manv = cboNV.SelectedValue.ToString();
                    DataRow dr = conn.Dset.Tables["TAIKHOAN"].Rows.Find(id);
                    if (dr == null)
                    {
                        MessageBox.Show("Tài khoản  này không tồn tại");
                        return;
                    }
                    dr["TENTK"] = tentk;
                    dr["MK"] = mk;
                    dr["QUYEN"] = quyen;
                    dr["MANV"] = manv;
                    SqlCommandBuilder builder = new SqlCommandBuilder(ada_TAIKHOAN);
                    ada_TAIKHOAN.Update(conn.Dset, "TAIKHOAN");

                    dgrLogin.ClearSelection();
                    createTable_TK_NV();
                    dgrLogin.DataSource = conn.Dset.Tables["TAIKHOAN,NHANVIEN"];
                    MessageBox.Show("Sửa thành công thành công tài khoản" + tentk);
                    load_Begin();
                    return;
                }
                catch
                {
                    MessageBox.Show("Lỗi!!\n Xin hãy kiểm tra lại!!");
                    return;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string id = txtID.Text.Trim();
                    string tentk = txtTaikhoan.Text.Trim();
                    DataRow dr = conn.Dset.Tables["TAIKHOAN"].Rows.Find(id);
                    if (dr == null)
                    {
                        MessageBox.Show("Tài khoản  này không tồn tại");
                        return;
                    }
                    dr.Delete();
                    SqlCommandBuilder builder = new SqlCommandBuilder(ada_TAIKHOAN);
                    ada_TAIKHOAN.Update(conn.Dset, "TAIKHOAN");
                    DataRow dr1 = conn.Dset.Tables["TAIKHOAN,NHANVIEN"].Rows.Find(id);
                    dr1.Delete();
                    SqlCommandBuilder builder1 = new SqlCommandBuilder(ada_TAIKHOAN_NHANVIEN);

                    dgrLogin.ClearSelection();
                    createTable_TK_NV();
                    dgrLogin.DataSource = conn.Dset.Tables["TAIKHOAN,NHANVIEN"];
                    MessageBox.Show("Xóa thành công thành công tài khoản" + tentk);
                    load_Begin();
                    return;
                }
                catch
                {
                    MessageBox.Show("Lỗi!!\n Xin hãy kiểm tra lại!!");
                    return;
                }
            }
        }

        private void dgrLogin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index;
                index = e.RowIndex;
                txtID.Text = dgrLogin.Rows[index].Cells[0].Value.ToString();
                txtTaikhoan.Text = dgrLogin.Rows[index].Cells[1].Value.ToString();
                txtMK.Text = dgrLogin.Rows[index].Cells[2].Value.ToString();
                cboNV.Text = dgrLogin.Rows[index].Cells[5].Value.ToString();
                cboQuyen.Text = dgrLogin.Rows[index].Cells[3].Value.ToString();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThemmoi.Enabled = false;
                btnTaoMoi.Enabled = true;
                groupBox1.Enabled = true;
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
        public void hienThiTK()
        {
            try
            {
                string sql = "select ID[ID],TENTK[Tên TK],MK[Mật khẩu],QUYEN[Quyền],tk.MANV[Mã NV],TENNV[Tên NV],CHUCVU[Chức vụ] from TAIKHOAN tk, NHANVIEN nv where tk.MANV = nv.MANV and nv.TENNV like N'%" + txtTK.Text.Trim() + "%'";
                DataSet ds = conn.GrdSource(sql);
                dgrLogin.DataSource = ds.Tables[0];
                dgrLogin.Refresh();
                return;
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

        private void txtTK_TextChanged(object sender, EventArgs e)
        {
            hienThiTK();
        }

        private void frmQuanLyTaiKhoan_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                frmQuanLyNhanVien frm = new frmQuanLyNhanVien();
                frm.ShowDialog();
                loadNV();
                return;
            }
            catch
            {
                return;
            }
        }

    }
}

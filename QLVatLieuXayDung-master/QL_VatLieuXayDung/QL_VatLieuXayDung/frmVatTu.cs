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
    public partial class frmVatTu : Form
    {
        KETNOI conn = new KETNOI();
        SqlDataAdapter ada_VatTu = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmVatTu()
        {
            InitializeComponent();
        }
        public void taoMaVT()
        {
            string sql = "SELECT MAX(RIGHT(MAVT, 8)) FROM VATTU";
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
                txtMaVT.Text = "VT00000001";
            }
            else
            {
                int m = int.Parse(ma);
                if (m >= 0 && m < 9)
                {
                    ma = "VT0000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                if (m >= 9)
                {
                    ma = "VT000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                txtMaVT.Text = ma;
            }
        }
        public void createTable_VatTu()
        {
            // tao 1 table tren Dataset
            string strSQL = "SELECT * FROM VATTU";
            ada_VatTu = conn.getDataAdapter(strSQL, "VATTU");
            primaryKey[0] = conn.Dset.Tables["VATTU"].Columns["MAVT"];
            conn.Dset.Tables["VATTU"].PrimaryKey = primaryKey;// thiet lap khoa chinh cho bang vat tu
        }
        private void frmVatTu_Load(object sender, EventArgs e)
        {
            createTable_VatTu();
            dgVVatTu.DataSource = conn.Dset.Tables["VATTU"];

            string s = "select * from LOAIVT";
            DataTable dt = conn.getDataTable(s, "LOAIVT");
            cboMaLoai.DataSource = dt;
            cboMaLoai.DisplayMember = "TENLOAI";
            cboMaLoai.ValueMember = "MALOAI";

            cboMaLoai.Text = "";
            load_begin();
        }
        public void load_begin()
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            txtTenVT.Clear();
            txtDVT.Clear();
            cboMaLoai.Text = "";
            txtDonGia.Clear();
            txtTenVT.Focus();
            groupBox1.Enabled = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            
            try
            {
                string mavt = txtMaVT.Text.Trim();
                string tenvt = txtTenVT.Text.Trim();
                string dvt = txtDVT.Text.Trim();
                string dongia = txtDonGia.Text.Trim();
                string maloai = cboMaLoai.SelectedValue.ToString();
                // Kiểm tra hợp lệ nhập liệu
                if (txtTenVT.Text == "")
                {
                    MessageBox.Show("Hãy nhập Tên vật tư!", "Chú ý!");
                    return;
                }
                if (txtDVT.Text == "")
                {
                    MessageBox.Show("Hãy nhập đơn vị tính!", "Chú ý!");
                    return;
                }
                if (txtDonGia.Text == "")
                {
                    MessageBox.Show("Hãy nhập Đơn giá!", "Chú ý!");
                    return;
                }
                if (cboMaLoai.Text == "")
                {
                    MessageBox.Show("Hãy chọn loại!", "Chú ý!");
                    return;
                }
                DataRow dr = conn.Dset.Tables["VATTU"].Rows.Find(maloai);
                if (dr != null)
                {
                    MessageBox.Show("Vật Tư  này đã tồn tại");
                    return;
                }
                DataRow them = conn.Dset.Tables["VATTU"].NewRow();
                them["MAVT"] = mavt;
                them["TENVT"] = tenvt;
                them["DVT"] = dvt;
                them["DONGIA"] = dongia;
                them["MALOAI"] = maloai;
                conn.Dset.Tables["VATTU"].Rows.Add(them);
                SqlCommandBuilder builder = new SqlCommandBuilder(ada_VatTu);
                ada_VatTu.Update(conn.Dset, "VATTU");
                MessageBox.Show("Thêm thành công Vật Tư " + mavt);
                btnThem.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Loi!!");
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            taoMaVT();
            txtTenVT.Clear();
            txtDVT.Clear();
            cboMaLoai.Text = "";
            txtDonGia.Clear();
            txtTenVT.Focus();
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnTaoMoi.Enabled = true;
            groupBox1.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có muốn Sửa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // kiem tra hop le nhap lieu
                try
                {
                    string mavt = txtMaVT.Text.Trim();
                    string tenvt = txtTenVT.Text.Trim();
                    string dvt = txtDVT.Text.Trim();
                    string dongia = txtDonGia.Text.Trim();
                    string maloai = cboMaLoai.SelectedValue.ToString();
                    DataRow dr = conn.Dset.Tables["VATTU"].Rows.Find(mavt);
                    if (dr == null)
                    {
                        MessageBox.Show("Vật Tư  " + mavt + " không tồn tại");
                        return;
                    }
                    else
                    {
                        dr["TENVT"] = tenvt;
                        dr["DVT"] = dvt;
                        dr["DONGIA"] = dongia;
                        dr["MALOAI"] = maloai;

                        // dong bo du lieu tu dataset len server
                        SqlCommandBuilder builder = new SqlCommandBuilder(ada_VatTu);
                        ada_VatTu.Update(conn.Dset, "VATTU");
                        MessageBox.Show("Sửa thành công Vật Tư " + mavt);
                    }

                }
                catch
                {
                    MessageBox.Show("Loi!!");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string mavt = txtMaVT.Text.Trim();
                    // kiểm tra khóa ngoại 
                    string sql = "select count(*) from KHO where MAVT='" + mavt + "'";
                    bool kq = conn.checkForExistence(sql);
                    if (kq == true)
                    {
                        MessageBox.Show("Dữ liệu này đang được sử dụng!!", "Chú ý!");
                        return;
                    }
                    else
                    {
                        DataRow dr = conn.Dset.Tables["VATTU"].Rows.Find(mavt);
                        if (dr == null)
                        {
                            MessageBox.Show("Vật Tư  " + mavt + " không tồn tại");
                            return;
                        }

                        dr.Delete();

                        // dong bo du lieu tu dataset len server
                        SqlCommandBuilder builder = new SqlCommandBuilder(ada_VatTu);
                        ada_VatTu.Update(conn.Dset, "VATTU");
                        MessageBox.Show("Xóa thành công Vật Tư " + mavt);
                        load_begin();
                    }
                }
                catch
                {
                    MessageBox.Show("Loi!!");
                }
            }
        }

        private void dgVVatTu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index;
                index = e.RowIndex;
                txtMaVT.Text = dgVVatTu.Rows[index].Cells[0].Value.ToString();
                txtTenVT.Text = dgVVatTu.Rows[index].Cells[1].Value.ToString();
                txtDVT.Text = dgVVatTu.Rows[index].Cells[2].Value.ToString();
                cboMaLoai.Text = dgVVatTu.Rows[index].Cells[3].Value.ToString();
                txtDonGia.Text = dgVVatTu.Rows[index].Cells[4].Value.ToString();
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

        private void btnIn_Click(object sender, EventArgs e)
        {
            frmInDSVatTu ds = new frmInDSVatTu();
            ds.ShowDialog();
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (txtDonGia.Text == "")
            {
                return;
            }
            else
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtDonGia.Text, System.Globalization.NumberStyles.AllowThousands);
                txtDonGia.Text = String.Format(culture, "{0:N0}", value);
                txtDonGia.Select(txtDonGia.Text.Length, 0);
            }
        }
        public void hienThiTK()
        {
            try
            {
                string sql = "select * from VATTU where TENVT like '%" + txtTK.Text.Trim() + "%'";
                DataSet ds = conn.GrdSource(sql);
                dgVVatTu.DataSource = ds.Tables[0];
                dgVVatTu.Refresh();
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

        private void frmVatTu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                e.Cancel = true;
        }
    }
}

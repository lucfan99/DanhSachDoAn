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
    public partial class frmLoaiVT : Form
    {
        KETNOI conn = new KETNOI();
        SqlDataAdapter ada_LoaiVT = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmLoaiVT()
        {
            InitializeComponent();
        }
        public void taoMaLoaiVT()
        {
            string sql = "SELECT MAX(RIGHT(MALOAI, 6)) FROM LOAIVT";
            SqlDataReader dr = conn.getReader(sql);
            string ma="";
            while (dr.Read())
            {
                ma = dr[""].ToString();
            }
            dr.Close();
            conn.ClosedConnection();
            if (ma == "")
            {
                txtMaLoai.Text = "LOAI000001";
            }
            else
            {
                int m = int.Parse(ma);
                if (m >= 0 && m < 9)
                {
                    ma = "LOAI00000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                if (m >= 9)
                {
                    ma = "LOAI0000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                txtMaLoai.Text = ma;
            }
        }
        public void createTable_LoaiVatTu()
        {
            // tao 1 table tren Dataset
            string strSQL = "SELECT * FROM LOAIVT";
            ada_LoaiVT = conn.getDataAdapter(strSQL, "LOAIVT");
            primaryKey[0] = conn.Dset.Tables["LOAIVT"].Columns["MALOAI"];
            conn.Dset.Tables["LOAIVT"].PrimaryKey = primaryKey;// thiet lap khoa chinh cho bang loai vat tu
        }
        private void dgrDSGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        public void load_Begin()
        {
            groupBox1.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }
        private void frmLoaiVT_Load(object sender, EventArgs e)
        {
            load_Begin();

            createTable_LoaiVatTu();
            dgVLoaiVT.DataSource = conn.Dset.Tables["LOAIVT"];

            
        }
        private void dgVLoaiVT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            txtMaLoai.Text = dgVLoaiVT.Rows[index].Cells[0].Value.ToString();
            txtTenLoai.Text = dgVLoaiVT.Rows[index].Cells[1].Value.ToString();
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            

            
            try
            {
                string maloai = txtMaLoai.Text.Trim();
                string tenloai = txtTenLoai.Text.Trim();
                // kiem tra hop le nhap lieu
                if (txtTenLoai.Text == "")
                {
                    MessageBox.Show("Hãy nhập tên loại!", "Chú ý!");
                    return;
                }
                DataRow dr = conn.Dset.Tables["LOAIVT"].Rows.Find(maloai);
                if (dr != null)
                {
                    MessageBox.Show("Loại vật tư  này đã tồn tại");
                    return;
                }
                DataRow them = conn.Dset.Tables["LOAIVT"].NewRow();
                them["MALOAI"] = maloai;
                them["TENLOAI"] = tenloai;
                conn.Dset.Tables["LOAIVT"].Rows.Add(them);
                SqlCommandBuilder builder = new SqlCommandBuilder(ada_LoaiVT);
                ada_LoaiVT.Update(conn.Dset, "LOAIVT");
                MessageBox.Show("Thêm thành công loại " + tenloai);
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string maloai = txtMaLoai.Text.Trim();
                    string sql = "select count(*) from VATTU where MALOAI='" + maloai + "'";
                    bool kq = conn.checkForExistence(sql);
                    if (kq == true)
                    {
                        MessageBox.Show("Dữ liệu này đang được sử dụng!!", "Chú ý!");
                        return;
                    }
                    else
                    {
                        DataRow dr = conn.Dset.Tables["LOAIVT"].Rows.Find(maloai);
                        if (dr == null)
                        {
                            MessageBox.Show("Loại Vật Tư không tồn tại");
                            return;
                        }


                        dr.Delete();


                        // dong bo du lieu tu dataset len server
                        SqlCommandBuilder builder = new SqlCommandBuilder(ada_LoaiVT);
                        ada_LoaiVT.Update(conn.Dset, "LOAIVT");
                        MessageBox.Show("Xóa thành công ");
                        load_Begin();
                    }

                }
                catch
                {
                    MessageBox.Show("Loi!!");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn sửa dữ liệu không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string maloai = txtMaLoai.Text.Trim();
                string tenloai = txtTenLoai.Text.Trim();

                // kiem tra hop le nhap lieu
                try
                {
                    DataRow dr = conn.Dset.Tables["LOAIVT"].Rows.Find(maloai);
                    if (dr == null)
                    {
                        MessageBox.Show("Loại Vật Tư  " + maloai + " không tồn tại");
                        return;
                    }


                    dr["TENLOAI"] = tenloai;


                    // dong bo du lieu tu dataset len server
                    SqlCommandBuilder builder = new SqlCommandBuilder(ada_LoaiVT);
                    ada_LoaiVT.Update(conn.Dset, "LOAIVT");
                    MessageBox.Show("Sửa thanh cong Loại Vật Tư " + maloai);

                }
                catch
                {
                    MessageBox.Show("Loi!!");
                }
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            taoMaLoaiVT();
            txtTenLoai.Clear();
            txtTenLoai.Focus();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            groupBox1.Enabled = true;
            btnTaoMoi.Enabled = false;
        }

        private void dgVLoaiVT_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index;
                index = e.RowIndex;
                txtMaLoai.Text = dgVLoaiVT.Rows[index].Cells[0].Value.ToString();
                txtTenLoai.Text = dgVLoaiVT.Rows[index].Cells[1].Value.ToString();
                groupBox1.Enabled = true;
                btnTaoMoi.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
            }
            catch
            {
                return;
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {

        }
        public void hienThiTK()
        {
            try
            {
                string sql = "select * from LOAIVT where TENLOAI like N'%" + txtTK.Text.Trim() + "'";
                DataSet ds = conn.GrdSource(sql);
                dgVLoaiVT.DataSource = ds.Tables[0];
                dgVLoaiVT.Refresh();
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

        private void frmLoaiVT_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

    }
}

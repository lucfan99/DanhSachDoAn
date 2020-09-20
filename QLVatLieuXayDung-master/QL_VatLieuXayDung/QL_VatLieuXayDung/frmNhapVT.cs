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
    public partial class frmNhapVT : Form
    {
        KETNOI conn = new KETNOI();
        SqlDataAdapter ada_ChiTietNhap = new SqlDataAdapter();
        SqlDataAdapter ada_PhieuNhap = new SqlDataAdapter();
        SqlDataAdapter ada_TonKho = new SqlDataAdapter();
        DataColumn[] primaryKeys2 = new DataColumn[2];
        DataColumn[] primaryKeys1 = new DataColumn[1];
        DataColumn[] primaryKeys3 = new DataColumn[1];
        int demcboMAVT = 0;
        public frmNhapVT()
        {
            InitializeComponent();
        }
        public void taoPN()
        {
            string sql = "SELECT MAX(RIGHT(MAPN, 8)) FROM PHIEUNHAP";
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
                txtMaPN.Text = "PN00000001";
            }
            else
            {
                int m = int.Parse(ma);
                if (m >= 0 && m < 9)
                {
                    ma = "PN0000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                if (m >= 9)
                {
                    ma = "PN000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                txtMaPN.Text = ma;
            }
        }

        public void createTable_CTNhap()
        {
            // tao 1 table tren Dataset
            string strSQL = "select * from CHITIETNHAP";
            ada_ChiTietNhap = conn.getDataAdapter(strSQL, "CHITIETNHAP");
            primaryKeys2[0] = conn.Dset.Tables["CHITIETNHAP"].Columns["MAPN"];
            primaryKeys2[1] = conn.Dset.Tables["CHITIETNHAP"].Columns["MAVT"];
            conn.Dset.Tables["CHITIETNHAP"].PrimaryKey = primaryKeys2;// thiet lap khoa chinh cho bang chi tiết phiếu nhập
        }
        public void createTable_PhieuNhap()
        {
            // tao 1 table tren Dataset
            string strSQL = "select * from PHIEUNHAP";
            ada_PhieuNhap = conn.getDataAdapter(strSQL, "PHIEUNHAP");
            primaryKeys1[0] = conn.Dset.Tables["PHIEUNHAP"].Columns["MAPN"];
            conn.Dset.Tables["PHIEUNHAP"].PrimaryKey = primaryKeys1;// thiet lap khoa chinh cho bang chi tiết phiếu nhập
        }
        public void createTable_TonKho()
        {
            // tao 1 table tren Dataset
            string strSQL = "select * from KHO";
            ada_TonKho = conn.getDataAdapter(strSQL, "KHO");
            primaryKeys3[0] = conn.Dset.Tables["KHO"].Columns["MAVT"];
            conn.Dset.Tables["KHO"].PrimaryKey = primaryKeys3;// thiet lap khoa chinh cho bang chi tiết phiếu nhập
        }
        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }
        public void HienThi()
        {
            string strSQL = "select CT.MAPN[Mã PN],CT.MAVT[Mã VT],TENVT[Tên VT],PN.MANV[Mã nhân viên],TENNV[Tên nhân viên],DONGIANHAP[Đơn giá],SOLUONG[Số lượng],CT.DVT[DVT],NGAYNHAP[Ngày nhập],MANCC[Mã NCC] from CHITIETNHAP CT,PHIEUNHAP PN,NHANVIEN NV,VATTU VT WHERE CT.MAPN = PN.MAPN AND CT.MAVT = VT.MAVT AND NV.MANV = PN.MANV AND CT.MAPN = '" + txtMaPN.Text.Trim() + "'";
            DataSet ds = conn.GrdSource(strSQL);
            grdNhapHang.DataSource = ds.Tables[0];
            grdNhapHang.Refresh();
        }
        private void btnNhap_Click(object sender, EventArgs e)
        {
            
            try
            {
                string mapn = txtMaPN.Text.Trim();
                string tenncc = cboMaNCC.SelectedValue.ToString();
                string ngaynhap = DateTime.ParseExact(pckNgayNhap.Text, "MM/dd/yyyy", null).ToString("yyyy/MM/dd");
                string manv = cboNV.SelectedValue.ToString();
                if (cboMaNCC.Text == "")
                {
                    MessageBox.Show("Hãy chọn nhà cung cấp!", "Chú ý!");
                    return;
                }
                if (pckNgayNhap.Text == "")
                {
                    MessageBox.Show("Hãy chọn ngày nhập!", "Chú ý!");
                    return;
                }
                if (cboNV.Text == "")
                {
                    MessageBox.Show("Hãy chọn nhân viên!", "Chú ý!");
                    return;
                }
                DataRow dr = conn.Dset.Tables["PHIEUNHAP"].Rows.Find(mapn);
                if (dr != null)
                {
                    MessageBox.Show("Phiếu nhập này đã tồn tại ");
                    return;
                }
                DataRow newRow1 = conn.Dset.Tables["PHIEUNHAP"].NewRow();
                newRow1["MAPN"] = mapn;
                newRow1["NGAYNHAP"] = ngaynhap;
                newRow1["MANCC"] = tenncc;
                newRow1["THANHTIEN"] = "0";
                newRow1["MANV"] = manv;

                conn.Dset.Tables["PHIEUNHAP"].Rows.Add(newRow1);

                SqlCommandBuilder builder1 = new SqlCommandBuilder(ada_PhieuNhap);
                ada_PhieuNhap.Update(conn.Dset, "PHIEUNHAP");
                HienThi();
                groupCTNhap.Enabled = true;
                groupPhieuNhap.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = true;
                btnHoanTat.Enabled = false;
                btnInHD.Enabled = false;
                btnGhi.Enabled = true;
                tableLayoutPanel6.Enabled = true;
                //groupChiTietHDN.Enabled = true;
                //btnThem_Click(sender, e);
                //cboMaMatH.Select();
                return;
            }
            catch
            {
                MessageBox.Show("Lỗi định dạng ngày !!\n Xin vui lòng cài đặt lại ngày hệ thống tháng/ngày/năm!!");
                return;
            }
        }

        private void frmNhapVT_Load(object sender, EventArgs e)
        {
            createTable_CTNhap();
            createTable_PhieuNhap();
            createTable_TonKho();
            load();
            HienThi();

            string sql1 = "select * from NHACUNGCAP";
            DataTable dt1 = conn.getDataTable(sql1, "NHACUNGCAP");
            cboMaNCC.DataSource = dt1;
            cboMaNCC.DisplayMember = "TENNCC";
            cboMaNCC.ValueMember = "MANCC";
            cboMaNCC.Text = "";

            string sql2 = "select * from TAIKHOAN,NHANVIEN where TAIKHOAN.MANV = NHANVIEN.MANV AND ID='" + frmDangNhap.ID_USER + "'";
            DataTable dt2 = conn.getDataTable(sql2, "TAIKHOAN,NHANVIEN");
            cboNV.DataSource = dt2;
            cboNV.DisplayMember = "TENNV";
            cboNV.ValueMember = "MANV";

            loadVatTu();
        }
        public void loadVatTu()
        {
            string sql3 = "select * from VATTU";
            DataSet dt3 = conn.GrdSource(sql3);
            cboMaVT.DataSource = dt3.Tables[0];
            cboMaVT.DisplayMember = "TENVT";
            cboMaVT.ValueMember = "MAVT";
            cboMaVT.Text = "";
            cboMaVT.Refresh();
        }
        public void load()
        {
            groupCTNhap.Enabled = false;
            groupPhieuNhap.Enabled = false;
            btnGhi.Enabled = false;
            btnHoanTat.Enabled = false;
            btnInHD.Enabled = false;
            btnNhap.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            groupPhieuNhap.Enabled = true;
            btnNhap.Enabled = true;
            txtMaPN.Clear();
            txtTongTien.Clear();
            cboMaVT.Text = "";
            cboMaNCC.Text = "";
            txtDonViTinh.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            taoPN();
            btnThem.Enabled = false;
            btnInHD.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int tinhTongTien()
        {
            int tong = 0;
            string sql = "select * from CHITIETNHAP where MAPN='" + txtMaPN.Text + "'";
            SqlDataReader dr = conn.getReader(sql);
            while (dr.Read())
            {
                tong = tong + int.Parse(dr["SOLUONG"].ToString()) * int.Parse(dr["DONGIANHAP"].ToString());
            }
            dr.Close();
            conn.ClosedConnection();
            return tong;

        }
        private void btnGhi_Click(object sender, EventArgs e)
        {
            try
            {
                string mapn = txtMaPN.Text.Trim();
                string mavt = cboMaVT.SelectedValue.ToString();
                string dongia = txtDonGia.Text.Trim();
                string soluong = txtSoLuong.Text.Trim();
                string dvt = txtDonViTinh.Text.Trim();
                if (txtSoLuong.Text == "0" || txtSoLuong.Text == "")
                {
                    MessageBox.Show("Số lượng không phù hợp!!", "Chú ý!");
                    return;
                }
                // kiểm tra vật tư đó có trong phiếu nhập đó chưa
                DataRow dr1 = conn.Dset.Tables["CHITIETNHAP"].Rows.Find(new object[] { mapn, mavt });
                if (dr1 != null)// nếu có rồi hiện ra thông báo
                {
                    MessageBox.Show("Vat tu nay da co trong phieu nhap");
                    return;
                }
                // nếu chưa thì thêm vào chi tiết nhập
                DataRow newRow = conn.Dset.Tables["CHITIETNHAP"].NewRow();
                newRow["MAPN"] = mapn;
                newRow["MAVT"] = mavt;
                newRow["DONGIANHAP"] = dongia;
                newRow["SOLUONG"] = soluong;
                newRow["DVT"] = dvt;
                // them vao bang chitietnhap 
                conn.Dset.Tables["CHITIETNHAP"].Rows.Add(newRow);
                //cap nhat lai chi tiet nhap
                SqlCommandBuilder builder = new SqlCommandBuilder(ada_ChiTietNhap);
                ada_ChiTietNhap.Update(conn.Dset, "CHITIETNHAP");
                // cập nhật lại thành tiền của phiếu nhập đó
                DataRow dr4 = conn.Dset.Tables["PHIEUNHAP"].Rows.Find(mapn);
                dr4["THANHTIEN"] = tinhTongTien().ToString();
                SqlCommandBuilder builder4 = new SqlCommandBuilder(ada_PhieuNhap);
                ada_PhieuNhap.Update(conn.Dset, "PHIEUNHAP");
                txtTongTien.Text = tinhTongTien().ToString();
                // kiểm tra xem kho có vật tư đó chưa
                DataRow drkho = conn.Dset.Tables["KHO"].Rows.Find(mavt);
                if (drkho != null)// nếu có rồi thì cập nhật lại số lượng nhập và tổng số lượng của vật tư đó
                {
                    string sql1 = "select * from KHO where MAVT ='" + mavt + "'";
                    int slnhap = 0;
                    int tongsl = 0;
                    SqlDataReader drsl = conn.getReader(sql1);
                    while (drsl.Read())
                    {
                        slnhap = int.Parse(drsl["SLNHAP"].ToString());// lấy số lượng nhập
                        tongsl = int.Parse(drsl["TONGSOLUONG"].ToString());
                    }
                    drsl.Close();
                    conn.ClosedConnection();
                    
                    string capnhatslnhap = (slnhap + int.Parse(soluong)).ToString();
                    string capnhattongsl = (tongsl + int.Parse(soluong)).ToString();
                    // cập nhật lại số lượng nhập và tổng số lượng của vật tư đó
                    drkho["SLNHAP"] = capnhatslnhap;
                    drkho["TONGSOLUONG"] = capnhattongsl;

                    SqlCommandBuilder builderkho = new SqlCommandBuilder(ada_TonKho);
                    ada_TonKho.Update(conn.Dset, "KHO");
                    MessageBox.Show("Cập nhật kho thành công");
                    HienThi();
                    btnHoanTat.Enabled = true;
                    cboMaVT.Text = "";
                    txtDonGia.Clear();
                    txtSoLuong.Clear();
                    txtDonViTinh.Clear();                 
                }
                else// nếu không có vật tư kho thì thêm vật tư vào kho
                {
                    DataRow themkho = conn.Dset.Tables["KHO"].NewRow();
                    themkho["MAVT"] = mavt;
                    themkho["TONGSOLUONG"] = soluong;
                    themkho["SLNHAP"] = soluong;
                    themkho["SLBAN"] = "0";

                    conn.Dset.Tables["KHO"].Rows.Add(themkho);

                    SqlCommandBuilder builderkho = new SqlCommandBuilder(ada_TonKho);
                    ada_TonKho.Update(conn.Dset, "KHO");
                    MessageBox.Show("Cập nhật kho thành công");
                    HienThi();
                    btnHoanTat.Enabled = true;
                    cboMaVT.Text = "";
                    txtDonGia.Clear();
                    txtSoLuong.Clear();
                    txtDonViTinh.Clear();
                }
                return;
            }
            catch
            {
                MessageBox.Show("Lỗi!\n Xin vui lòng thử lại hoặc kiểm tra lại !!", "Thông báo!");
                return;
            }

        }

        private void cboMaVT_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string sql = "select * from VATTU where MAVT ='" + cboMaVT.SelectedValue.ToString() + "'";
                SqlDataReader dr = conn.getReader(sql);
                while (dr.Read())
                {
                    txtDonViTinh.Text = dr["DVT"].ToString();
                    txtDonGia.Text = dr["DONGIA"].ToString();
                }
                dr.Close();
                conn.ClosedConnection();
                txtSoLuong.Clear();
                txtSoLuong.Focus();
                if (cboMaVT.Text.Trim() != "" && demcboMAVT >= 2)
                {
                    btnGhi.Enabled = true;
                    btnXoa.Enabled = false;
                    btnSua.Enabled = false;
                    demcboMAVT++;
                }
                else
                {
                    btnGhi.Enabled = false;
                    //btnXoa.Enabled = true;
                    //btnSua.Enabled = true;
                    demcboMAVT++;
                }
                return;
            }
            catch
            {
                return;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string mapn = txtMaPN.Text.Trim();
                    string mavt = cboMaVT.SelectedValue.ToString();
                    string dongia = txtDonGia.Text.Trim();
                    string soluong = txtSoLuong.Text.Trim();
                    string dvt = txtDonViTinh.Text.Trim();
                    int slcu = laySoLuongCu();// lấy số lượng cũ của hồi nảy nhập
                    // kiểm tra chi tiết nhập có vật tư đó trong phiếu nhập chưa
                    DataRow dr1 = conn.Dset.Tables["CHITIETNHAP"].Rows.Find(new object[] { mapn, mavt });
                    if (dr1 == null) // nếu không có hiển thị ra thông báo
                    {
                        MessageBox.Show("Vat tu nay không co trong phieu nhap");
                        return;
                    }
                    // nếu có cập nhật lại số lượng chi tiết nhập
                    dr1["SOLUONG"] = soluong;

                    //cap nhat lai chi tiet nhap
                    SqlCommandBuilder builder = new SqlCommandBuilder(ada_ChiTietNhap);
                    ada_ChiTietNhap.Update(conn.Dset, "CHITIETNHAP");
                    // cập nhật lại thành tiền của phiếu nhập đó
                    DataRow dr4 = conn.Dset.Tables["PHIEUNHAP"].Rows.Find(mapn);
                    dr4["THANHTIEN"] = tinhTongTien().ToString();
                    SqlCommandBuilder builder4 = new SqlCommandBuilder(ada_PhieuNhap);
                    ada_PhieuNhap.Update(conn.Dset, "PHIEUNHAP");
                    txtTongTien.Text = tinhTongTien().ToString();
                    // cập nhật lại kho sau khi sửa
                    int slht = int.Parse(soluong);// lấy sô lượng hiện tại

                    // tìm vật tư đó trong kho để cập nhật lại 
                    DataRow drkho = conn.Dset.Tables["KHO"].Rows.Find(mavt);

                    string sql1 = "select * from KHO where MAVT ='" + mavt + "'";
                    int slnhap = 0;
                    int tongsl = 0;
                    SqlDataReader drsl = conn.getReader(sql1);
                    while (drsl.Read())
                    {
                        slnhap = int.Parse(drsl["SLNHAP"].ToString());// lấy số lượng nhập
                        tongsl = int.Parse(drsl["TONGSOLUONG"].ToString());// lấy tổng số lượng
                    }
                    drsl.Close();
                    conn.ClosedConnection();

                    int capnhatsl = slht - slcu;
                    string updateSLNHAP = (slnhap + capnhatsl).ToString();//tính số lượng nhập lại sau khi update
                    string updateTONGSL = (tongsl + capnhatsl).ToString();// tính tổng số lượng lại sau khi update
                    drkho["SLNHAP"] = updateSLNHAP;
                    drkho["TONGSOLUONG"] = updateTONGSL;
                    // cập nhật lại trong kho
                    SqlCommandBuilder builderkho = new SqlCommandBuilder(ada_TonKho);
                    ada_TonKho.Update(conn.Dset, "KHO");
                    MessageBox.Show("Cập nhật kho thành công");
                    HienThi();
                    cboMaVT.Text = "";
                    txtDonViTinh.Clear();
                    txtDonGia.Clear();
                    txtSoLuong.Clear();
                    return;
                }
                catch
                {
                    MessageBox.Show("Lỗi!\n Xin vui lòng thử lại hoặc kiểm tra lại !!", "Thông báo!");
                    return;
                }
            }
        }
        public int laySoLuongCu()//  hàm lấy số lượng củ của vật tư đó trong phiếu nhập
        {
            int slcu = 0;
            string sql = "select * from CHITIETNHAP where MAPN='" + txtMaPN.Text + "' and MAVT='" + cboMaVT.SelectedValue.ToString() + "'";
            SqlDataReader dr = conn.getReader(sql);
            while (dr.Read())
            {
                slcu = int.Parse(dr["SOLUONG"].ToString());
            }
            dr.Close();
            conn.ClosedConnection();
            return slcu;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    if (groupPhieuNhap.Enabled == false)
                    {
                        string mapn = txtMaPN.Text.Trim();
                        string mavt = cboMaVT.SelectedValue.ToString();
                        string dongia = txtDonGia.Text.Trim();
                        string soluong = txtSoLuong.Text.Trim();
                        string dvt = txtDonViTinh.Text.Trim();
                        int slcu = laySoLuongCu();// lấy số lượng cũ
                        // kiểm tra vật tư đó có trong phiếu nhật chưa
                        DataRow dr1 = conn.Dset.Tables["CHITIETNHAP"].Rows.Find(new object[] { mapn, mavt });
                        if (dr1 == null)// nếu không thì hiển thị thông báo
                        {
                            string sql = "select count(*) from CHITIETNHAP where MAPN='" + mapn + "'";
                            bool kq = conn.checkForExistence(sql);
                            //nếu không có thì xóa lun phiếu nhập này
                            if (kq == false)
                            {
                                groupPhieuNhap.Enabled = true;
                                groupCTNhap.Enabled = false;
                                DataRow dr = conn.Dset.Tables["PHIEUNHAP"].Rows.Find(mapn);
                                if (dr == null)
                                {
                                    MessageBox.Show("Phiếu nhập này không tồn tại ");
                                    return;
                                }
                                else
                                {
                                    dr.Delete();

                                    SqlCommandBuilder builder1 = new SqlCommandBuilder(ada_PhieuNhap);
                                    ada_PhieuNhap.Update(conn.Dset, "PHIEUNHAP");
                                    txtMaPN.Clear();
                                    cboMaNCC.Text = "";
                                    cboMaVT.Text = "";
                                    txtDonViTinh.Clear();
                                    txtSoLuong.Clear();
                                    txtDonGia.Clear();
                                    txtTongTien.Clear();
                                    load();

                                    HienThi();
                                    MessageBox.Show("Xóa phiếu nhập thành công");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            //xóa vật tư trong chi tiết nhập
                            dr1.Delete();

                            //cap nhat lai chi tiet nhap
                            SqlCommandBuilder builder = new SqlCommandBuilder(ada_ChiTietNhap);
                            ada_ChiTietNhap.Update(conn.Dset, "CHITIETNHAP");

                            //DataRow drxoa = conn.Dset.Tables["CHITIETNHAP,PHIEUNHAP,NHANVIEN,VATTU"].Rows.Find(new object[] { mapn, mavt, tennv });
                            //drxoa.Delete();
                            //SqlCommandBuilder builderxoa = new SqlCommandBuilder(ada_ChiTiet);


                            // cập nhật lại tổng tiền trong phiếu nhập
                            DataRow dr4 = conn.Dset.Tables["PHIEUNHAP"].Rows.Find(mapn);
                            dr4["THANHTIEN"] = tinhTongTien().ToString();
                            SqlCommandBuilder builder4 = new SqlCommandBuilder(ada_PhieuNhap);
                            ada_PhieuNhap.Update(conn.Dset, "PHIEUNHAP");
                            txtTongTien.Text = tinhTongTien().ToString();
                            // cập nhật lại kho sau khi xóa
                            // Tìm vật tư trong kho để cập nhật
                            DataRow drkho = conn.Dset.Tables["KHO"].Rows.Find(mavt);
                            //lấy số lượng nhập và tổng số lượng của vật tư đó trong kho
                            string sql1 = "select * from KHO where MAVT ='" + mavt + "'";
                            int slnhap = 0;
                            int tongsl = 0;
                            SqlDataReader drsl = conn.getReader(sql1);
                            while (drsl.Read())
                            {
                                slnhap = int.Parse(drsl["SLNHAP"].ToString());// lấy số lượng nhập
                                tongsl = int.Parse(drsl["TONGSOLUONG"].ToString());// lấy tổng số lượng
                            }
                            drsl.Close();
                            conn.ClosedConnection();

                            string updateSLNHAP = (slnhap - slcu).ToString();
                            string updateTONGSL = (tongsl - slcu).ToString();
                            // cập nhật lại trong bảng
                            drkho["SLNHAP"] = updateSLNHAP;
                            drkho["TONGSOLUONG"] = updateTONGSL;
                            // cập nhật lại trong cơ sở dữ liệu
                            SqlCommandBuilder builderkho = new SqlCommandBuilder(ada_TonKho);
                            ada_TonKho.Update(conn.Dset, "KHO");
                            MessageBox.Show("Cập nhật kho thành công");
                            HienThi();
                            // kiểm tra xem phiếu nhập này còn vật tư không
                            string sqlkt = "select count(*) from CHITIETNHAP where MAPN='" + mapn + "'";
                            bool kq1 = conn.checkForExistence(sqlkt);
                            //nếu không có thì xóa lun phiếu nhập này
                            if (kq1 == false)
                            {
                                groupPhieuNhap.Enabled = true;
                                groupCTNhap.Enabled = false;
                                DataRow dr = conn.Dset.Tables["PHIEUNHAP"].Rows.Find(mapn);
                                if (dr == null)
                                {
                                    MessageBox.Show("Phiếu nhập này không tồn tại ");
                                    return;
                                }
                                dr.Delete();

                                SqlCommandBuilder builder1 = new SqlCommandBuilder(ada_PhieuNhap);
                                ada_PhieuNhap.Update(conn.Dset, "PHIEUNHAP");
                                txtMaPN.Clear();
                                cboMaNCC.Text = "";
                                cboMaVT.Text = "";
                                txtDonViTinh.Clear();
                                txtSoLuong.Clear();
                                txtDonGia.Clear();
                                txtTongTien.Clear();
                                load();

                                HienThi();
                                MessageBox.Show("Xóa phiếu nhập thành công");

                            }
                            cboMaVT.Text = "";
                            txtDonViTinh.Clear();
                            txtDonGia.Clear();
                            txtSoLuong.Clear();
                            return;
                        }
                        return;
                    }
                    return;
                }
                catch
                {
                    MessageBox.Show("Lỗi!\n Xin vui lòng thử lại hoặc kiểm tra lại !!", "Thông báo!");
                    return;
                }
            }
        }

        private void grdNhapHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                cboMaVT.Text = grdNhapHang.Rows[index].Cells[2].Value.ToString();
                txtDonGia.Text = grdNhapHang.Rows[index].Cells[5].Value.ToString();
                txtDonViTinh.Text = grdNhapHang.Rows[index].Cells[7].Value.ToString();
                txtSoLuong.Text = grdNhapHang.Rows[index].Cells[6].Value.ToString();
                btnGhi.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                return;
            }
            catch
            {
                return;
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            if (txtMaPN.Text == "")
            {
                MessageBox.Show("Hãy nhập mã hóa đơn để in!", "Thông báo!");
                txtMaPN.Select();
                return;
            }
            frmInPhieuNhap pn = new frmInPhieuNhap(txtMaPN.Text);
            pn.ShowDialog();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtTongTien.Text, System.Globalization.NumberStyles.AllowThousands);
                txtTongTien.Text = String.Format(culture, "{0:N0}", value);
                txtTongTien.Select(txtTongTien.Text.Length, 0);
            }
            catch
            {
                return;
            }
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtDonGia.Text, System.Globalization.NumberStyles.AllowThousands);
                txtDonGia.Text = String.Format(culture, "{0:N0}", value);
                txtDonGia.Select(txtDonGia.Text.Length, 0);
            }
            catch
            {
                return;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hoàn tất phiếu nhập này không?\n Sau khi hoàn tất bạn không được sửa hoặc xóa trên phiếu này nữa !!", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                btnThem.Enabled = true;
                groupCTNhap.Enabled = false;
                groupPhieuNhap.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnHoanTat.Enabled = false;
                btnInHD.Enabled = true;
                btnGhi.Enabled = false;
                tableLayoutPanel6.Enabled = false;
            }
        }

        private void frmNhapVT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnHoanTat.Enabled == true || btnXoa.Enabled == true || btnSua.Enabled == true || btnGhi.Enabled == true)
            {
                MessageBox.Show("Bạn chưa hoàn tất hóa đơn này!!", "Chú ý!!");
                e.Cancel = true;
            }
            if (btnHoanTat.Enabled == false && btnXoa.Enabled == false && btnSua.Enabled == false && btnGhi.Enabled == false)
            {
                DialogResult r;
                r = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void btnLS_Click(object sender, EventArgs e)
        {
            frmLichSuPhieuNhap frm = new frmLichSuPhieuNhap();
            frm.ShowDialog();
        }

        private void txtDonViTinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDonViTinh.Text.Trim() != "")
                {
                    btnGhi.Enabled = true;
                    btnXoa.Enabled = false;
                    btnSua.Enabled = false;
                }
                else
                {
                    btnGhi.Enabled = false;
                    btnXoa.Enabled = false;
                    btnSua.Enabled = false;
                }
                return;
            }
            catch
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                frmVatTu frm = new frmVatTu();
                frm.ShowDialog();
                loadVatTu();
                return;
            }
            catch
            {
                return;
            }
        }
    }
}

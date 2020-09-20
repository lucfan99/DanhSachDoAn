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
using System.Globalization;
using System.Text.RegularExpressions;

namespace QL_VatLieuXayDung
{
    public partial class frmBanVT : Form
    {
        KETNOI conn = new KETNOI();
        SqlDataAdapter ada_KH = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        SqlDataAdapter ada_HOADON = new SqlDataAdapter();
        DataColumn[] primaryKeyHD = new DataColumn[1];
        SqlDataAdapter ada_CTHOADON = new SqlDataAdapter();
        DataColumn[] primaryKeyCTHD = new DataColumn[2];
        SqlDataAdapter ada_TonKho = new SqlDataAdapter();
        DataColumn[] primaryKeyKHO = new DataColumn[1];
        int demcboMAVT = 0;
        public frmBanVT()
        {
            InitializeComponent();
        }
        public void createTable_HOADON()
        {
            string sql = "select * from HOADON";
            ada_HOADON = conn.getDataAdapter(sql, "HOADON");
            primaryKeyHD[0] = conn.Dset.Tables["HOADON"].Columns["MAHD"];
            conn.Dset.Tables["HOADON"].PrimaryKey = primaryKeyHD;
        }
        public void createTable_CTHOADON()
        {
            string sql = "select * from CHITIETHOADON";
            ada_CTHOADON = conn.getDataAdapter(sql, "CHITIETHOADON");
            primaryKeyCTHD[0] = conn.Dset.Tables["CHITIETHOADON"].Columns["MAHD"];
            primaryKeyCTHD[1] = conn.Dset.Tables["CHITIETHOADON"].Columns["MAVT"];
            conn.Dset.Tables["CHITIETHOADON"].PrimaryKey = primaryKeyCTHD;
        }
        public void createTable_TonKho()
        {
            // tao 1 table tren Dataset
            string strSQL = "select * from KHO";
            ada_TonKho = conn.getDataAdapter(strSQL, "KHO");
            primaryKeyKHO[0] = conn.Dset.Tables["KHO"].Columns["MAVT"];
            conn.Dset.Tables["KHO"].PrimaryKey = primaryKeyKHO;// thiet lap khoa chinh cho bang chi tiết phiếu nhập
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
        public void taoMaHoaDon()
        {
            string sql = "SELECT MAX(RIGHT(MAHD, 8)) FROM HOADON";
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
                txtMaHD.Text = "HD00000001";
            }
            else
            {
                int m = int.Parse(ma);
                if (m >= 0 && m < 9)
                {
                    ma = "HD0000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                if (m >= 9)
                {
                    ma = "HD000000" + Convert.ToInt32(Convert.ToInt32(ma) + 1);
                }
                txtMaHD.Text = ma;
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
        public void ClearDL()
        {
            txtDiaChiKH.Clear();
            txtMaHD.Clear();
            txtMaKH.Clear();
            txtSDTKH.Clear();
            txtTienKhach.Clear();
            txtTienThua.Clear();
            txtTongTien.Clear();
            cboTenKH.Text = "";
        }
        public void load_Begin()
        {
            btnGhi.Enabled = false;
            btnThanhToan.Enabled = false;
            btnInHD.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            tablePanelChiTiet.Enabled = false;
            tablepanelHoaDon.Enabled = false;
            btnThemKH.Enabled = true;
            btnLuuKH.Enabled = false;
            //groupBox1.Enabled = false;
            //groupBox2.Enabled = false;
            //groupBox3.Enabled = false;
            //btnXuatHang.Enabled = false;

        }

        private void frmBanVT_Load(object sender, EventArgs e)
        {
            createTable_HOADON();
            createTable_CTHOADON();
            createTable_TonKho();
            load_Begin();
            createTable_KhachHang();
            HienThi();
          
            string strSQL = "SELECT * FROM KHACHHANG";
            DataTable dt = conn.getDataTable(strSQL, "KHACHHANG");
            cboTenKH.DataSource = dt;
            cboTenKH.DisplayMember = "TENKH";
            cboTenKH.ValueMember = "MAKH";

            cboTenKH.Text = "";
            txtMaKH.Clear();
            txtSDTKH.Clear();
            txtDiaChiKH.Clear();
            txtMaKH.Focus();


            string sql3 = "select * from VATTU";
            DataTable dt3 = conn.getDataTable(sql3, "VATTU");
            cboMaVT.DataSource = dt3;
            cboMaVT.DisplayMember = "TENVT";
            cboMaVT.ValueMember = "MAVT";
            cboMaVT.Text = "";

            pckNgayXuat.Text = DateTime.Now.Date.ToString();

            string s = "SELECT * from NHANVIEN,TAIKHOAN WHERE NHANVIEN.MANV = TAIKHOAN.MANV AND ID ='" + frmDangNhap.ID_USER + "'";
            DataTable dt2 = conn.getDataTable(s, "NHANVIEN,TAIKHOAN");
            cboNhanVien.DataSource = dt2;
            cboNhanVien.ValueMember = "MANV";
            cboNhanVien.DisplayMember = "TENNV";
        }

        private void cboTenKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string str = "SELECT * FROM KHACHHANG where MAKH =N'" + cboTenKH.SelectedValue.ToString() + "'";
                SqlDataReader dr = conn.getReader(str);
                while (dr.Read())
                {
                    txtMaKH.Text = dr["MAKH"].ToString();
                    txtDiaChiKH.Text = dr["DIACHI"].ToString();
                    txtSDTKH.Text = dr["SDT"].ToString();
                }
                dr.Close();
                conn.ClosedConnection();
                btnThemKH.Enabled = true;
                btnLuuKH.Enabled = false;
                return;
            }
            catch
            {
                return;
            }
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            cboTenKH.Text = "";
            txtDiaChiKH.Clear();
            txtSDTKH.Clear();
            taoMaKH();
            txtDiaChiKH.Enabled = true;
            txtSDTKH.Enabled = true;
            cboTenKH.Focus();
            btnThemKH.Enabled = false;
            btnLuuKH.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            

            // kiem tra hop le nhap lieu
            try
            {
                string makh = txtMaKH.Text.Trim();
                string tenkh = cboTenKH.Text.Trim();
                string diachi = txtDiaChiKH.Text.Trim();
                string sdt = txtSDTKH.Text.Trim();
                if (txtMaKH.Text == "")
                {
                    MessageBox.Show("Hãy nhập Mã KH!", "Chú ý!");
                    return;
                }
                if (cboTenKH.Text == "")
                {
                    MessageBox.Show("Hãy nhập Tên KH!", "Chú ý!");
                    return;
                }
                if (txtDiaChiKH.Text == "")
                {
                    MessageBox.Show("Hãy nhập Địa KH!", "Chú ý!");
                    return;
                }
                if (IsValidPhone(txtSDTKH.Text.Trim()) == false)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ!!\n Xin vui lòng nhập lại!", "Chú ý");
                    txtSDTKH.Clear();
                    txtSDTKH.Focus();
                    return;
                }
                if (txtSDTKH.TextLength != 10)
                {
                    MessageBox.Show("Số điện thoại phải đủ 10 số!!\n Xin vui lòng nhập lại!", "Chú ý");
                    txtSDTKH.Clear();
                    txtSDTKH.Focus();
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
                btnThemKH.Enabled = true;
                btnLuuKH.Enabled = false;
                cboTenKH.Text = tenkh;
                return;
            }
            catch
            {
                MessageBox.Show("Loi!!");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearDL();
            taoMaHoaDon();
            tablepanelHoaDon.Enabled = true;
            tablePanelChiTiet.Enabled = false;
            btnThem.Enabled = false;
            btnInHD.Enabled = false;
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
                    txtDonGia.Text = ((float)int.Parse(dr["DONGIA"].ToString()) * 0.1 + int.Parse(dr["DONGIA"].ToString())).ToString();
                    txtSoLuong.Text = "1";
                }
                dr.Close();
                conn.ClosedConnection();
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
        public void HienThi()
        {
            string sql = "select CTHD.MAHD[Mã HD],CTHD.MAVT[Mã VT],TENVT[Tên VT],CTHD.DVT[DVT],SLBAN[Số lượng],DONGIABAN[Đơn giá] FROM HOADON HD,CHITIETHOADON CTHD,VATTU VT WHERE HD.MAHD = CTHD.MAHD AND VT.MAVT = CTHD.MAVT AND CTHD.MAHD ='" + txtMaHD.Text.Trim() + "'"; 
            DataSet ds = conn.GrdSource(sql);
            dgVDSMua.DataSource = ds.Tables[0];
            dgVDSMua.Refresh();
        }
        private void btnXuatHang_Click(object sender, EventArgs e)
        {
            try
            {
                string mahd = txtMaHD.Text.Trim();
                string manv = cboNhanVien.SelectedValue.ToString();
                string ngaynhap = DateTime.ParseExact(pckNgayXuat.Text, "MM/dd/yyyy", null).ToString("yyyy/MM/dd");
                string makh = txtMaKH.Text.Trim();
                if (txtMaHD.Text == "")
                {
                    MessageBox.Show("Hãy nhập mã hóa đơn!", "Chú ý!");
                    txtMaHD.Select();
                    return;
                }
                if (cboNhanVien.Text == "")
                {
                    MessageBox.Show("Hãy chọn nhà cung cấp!", "Chú ý!");
                    cboNhanVien.Select();
                    return;
                }
                if (pckNgayXuat.Text == "")
                {
                    MessageBox.Show("Hãy chọn ngày nhập!", "Chú ý!");
                    pckNgayXuat.Select();
                    return;
                }
                if (txtMaKH.Text == "")
                {
                    MessageBox.Show("Hãy chọn  khách hàng!", "Chú ý!");
                    txtMaKH.Select();
                    return;
                }
                DataRow dr = conn.Dset.Tables["HOADON"].Rows.Find(mahd);
                if (dr != null)
                {
                    MessageBox.Show("Hóa đơn này đã tồn tại ");
                    return;
                }
                DataRow themhd = conn.Dset.Tables["HOADON"].NewRow();
                themhd["MAHD"] = mahd;
                themhd["NGAYHD"] = ngaynhap;
                themhd["TINHTRANGHD"] = "CHƯA THANH TOÁN";
                themhd["MAKH"] = makh;
                themhd["MANV"] = manv;
                themhd["TONGTIEN"] = "0";

                conn.Dset.Tables["HOADON"].Rows.Add(themhd);

                SqlCommandBuilder builder1 = new SqlCommandBuilder(ada_HOADON);
                ada_HOADON.Update(conn.Dset, "HOADON");
                HienThi();
                tablePanelChiTiet.Enabled = true;
                btnXoa.Enabled = true;
                btnGhi.Enabled = true;
                tablepanelHoaDon.Enabled = false;
                return;
            }
            catch
            {
                MessageBox.Show("Lỗi định dạng ngày !!\n Xin vui lòng cài đặt lại ngày hệ thống tháng/ngày/năm!!");
                return;
            }
           
        }
        public double tinhTongTien()
        {
            double tong = 0;
            string sql = "select * from CHITIETHOADON where MAHD='" + txtMaHD.Text + "'";
            SqlDataReader dr = conn.getReader(sql);
            while (dr.Read())
            {
                tong = tong + double.Parse(dr["SLBAN"].ToString()) * double.Parse(dr["DONGIABAN"].ToString());
            }
            dr.Close();
            conn.ClosedConnection();
            return tong;
        }
        private void btnGhi_Click(object sender, EventArgs e)
        {
            try
            {
                string mahd = txtMaHD.Text.Trim();
                string mavt = cboMaVT.SelectedValue.ToString();
                string dongia = txtDonGia.Text.Trim();
                string soluong = txtSoLuong.Text.Trim();
                string dvt = txtDonViTinh.Text.Trim();
                if (txtSoLuong.Value.ToString() == "" || txtSoLuong.Value.ToString() == "0")
                {
                    MessageBox.Show("Số lượng bạn chọn không phù hợp!!", "Chú ý!");
                    return;
                }
                // kiểm tra trong kho có vật tư đó không
                DataRow drkho = conn.Dset.Tables["KHO"].Rows.Find(mavt);
                if (drkho != null)//nếu có
                {
                    string sql1 = "select * from KHO where MAVT ='" + mavt + "'";
                    int tongsl = 0;
                    int slban = 0;
                    SqlDataReader drsl = conn.getReader(sql1);
                    while (drsl.Read())
                    {
                        tongsl = int.Parse(drsl["TONGSOLUONG"].ToString());//lấy tổng số lượng của vật tư đó để kiểm tra xem còn hàng không
                        slban = int.Parse(drsl["SLBAN"].ToString()); // lấy số lượng bán
                    }
                    drsl.Close();
                    conn.ClosedConnection();
                    if (tongsl > 0 && tongsl >= int.Parse(soluong))//nếu còn hàng
                    {
                        //tiếp theo kiểm tra xem trong chi tiết hóa đơn đó có vật tư của hóa đơn đó chưa
                        DataRow dr = conn.Dset.Tables["CHITIETHOADON"].Rows.Find(new object[] { mahd, mavt });
                        if (dr != null)//nếu có thì hiển thị thông báo
                        {
                            MessageBox.Show("Vat tu nay da co trong hóa đơn");
                            return;
                        }
                        //còn lại thì thêm vào chi tiết hóa đơn
                        DataRow them = conn.Dset.Tables["CHITIETHOADON"].NewRow();
                        them["MAHD"] = mahd;
                        them["MAVT"] = mavt;
                        them["DONGIABAN"] = dongia;
                        them["SLBAN"] = soluong;
                        them["DVT"] = dvt;
                        // them vao bang chi tiết hóa đơn 
                        conn.Dset.Tables["CHITIETHOADON"].Rows.Add(them);
                        //cap nhat lai chi hóa đơn
                        SqlCommandBuilder builder = new SqlCommandBuilder(ada_CTHOADON);
                        ada_CTHOADON.Update(conn.Dset, "CHITIETHOADON");
                        //cập nhật lại tổng tiền của hóa đơn đó
                        DataRow dr1 = conn.Dset.Tables["HOADON"].Rows.Find(mahd);
                        dr1["TONGTIEN"] = tinhTongTien().ToString();
                        SqlCommandBuilder builder1 = new SqlCommandBuilder(ada_HOADON);
                        ada_HOADON.Update(conn.Dset, "HOADON");
                        txtTongTien.Text = tinhTongTien().ToString();

                        string capnhatTONGSL = (tongsl - int.Parse(soluong)).ToString();
                        string capnhatSLBAN = (slban + int.Parse(soluong)).ToString();
                        // cập nhật vào bảng
                        drkho["TONGSOLUONG"] = capnhatTONGSL;
                        drkho["SLBAN"] = capnhatSLBAN;
                        // cập nhật trên cơ sở dữ liệu
                        SqlCommandBuilder builderkho = new SqlCommandBuilder(ada_TonKho);
                        ada_TonKho.Update(conn.Dset, "KHO");
                        MessageBox.Show("Thêm vật tư thành công!!");

                        HienThi();
                        btnSua.Enabled = true;
                        btnXoa.Enabled = true;
                        btnThanhToan.Enabled = true;
                        cboMaVT.Text = "";
                        txtSoLuong.TextAlign = 0;
                        txtDonGia.Clear();
                        txtDonViTinh.Clear();
                        groupBox6.Enabled = true;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Hết hàng!! (Trong kho chỉ còn : "+ tongsl+")");
                        txtSoLuong.Value = tongsl;
                        txtSoLuong.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Vật tư này không có trong kho");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Lỗi!\n Xin vui lòng thử lại hoặc kiểm tra lại !!", "Thông báo!");
                return;
            }
        }
        public int laySoLuongCu()
        {
            int slcu = 0;
            string sql = "select * from CHITIETHOADON where MAHD='" + txtMaHD.Text + "' and MAVT='" + cboMaVT.SelectedValue.ToString() + "'";
            SqlDataReader dr = conn.getReader(sql);
            while (dr.Read())
            {
                slcu = int.Parse(dr["SLBAN"].ToString());
            }
            dr.Close();
            conn.ClosedConnection();
            return slcu;

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string mahd = txtMaHD.Text.Trim();
                    string mavt = cboMaVT.SelectedValue.ToString();
                    string dongia = txtDonGia.Text.Trim();
                    string soluong = txtSoLuong.Text.Trim();
                    string dvt = txtDonViTinh.Text.Trim();
                    int slcu = laySoLuongCu();// lấy số lượng cũ hồi nảy vừa nhập
                    // kiểm tra trong kho có vật tư đó không
                    DataRow drkho = conn.Dset.Tables["KHO"].Rows.Find(mavt);
                    if (drkho != null)//nếu có
                    {
                        string sql1 = "select * from KHO where MAVT ='" + mavt + "'";
                        int tongsl = 0;
                        int slban = 0;
                        SqlDataReader drsl = conn.getReader(sql1);
                        while (drsl.Read())
                        {
                            tongsl = int.Parse(drsl["TONGSOLUONG"].ToString());//lấy tổng số lượng của vật tư đó để kiểm tra xem còn hàng không
                            slban = int.Parse(drsl["SLBAN"].ToString()); // lấy số lượng bán
                        }
                        drsl.Close();
                        conn.ClosedConnection();
                        if (tongsl > 0 && tongsl >= int.Parse(soluong))//nếu còn hàng
                        {
                            //tiếp theo kiểm tra xem trong chi tiết hóa đơn đó có vật tư của hóa đơn đó chưa
                            DataRow dr1 = conn.Dset.Tables["CHITIETHOADON"].Rows.Find(new object[] { mahd, mavt });
                            if (dr1 == null)// nếu không có
                            {
                                MessageBox.Show("Vật tư này không tồn tại trong hóa đơn");
                                return;
                            }
                            //còn lại là có thì chỉnh sửa lại số lượng bán
                            dr1["SLBAN"] = soluong;


                            //cap nhat lai chi tiet nhap
                            SqlCommandBuilder builder = new SqlCommandBuilder(ada_CTHOADON);
                            ada_CTHOADON.Update(conn.Dset, "CHITIETHOADON");
                            // cập nhật tổng tiền lại hóa đơn
                            DataRow dr4 = conn.Dset.Tables["HOADON"].Rows.Find(mahd);
                            dr4["TONGTIEN"] = tinhTongTien().ToString();
                            SqlCommandBuilder builder4 = new SqlCommandBuilder(ada_HOADON);
                            ada_HOADON.Update(conn.Dset, "HOADON");
                            //lấy tổng tiền hóa đơn đưa ra textbox
                            txtTongTien.Text = tinhTongTien().ToString();

                            // cập nhật lại kho sau khi sửa
                            int slht = int.Parse(soluong);
                            int slcapnhat = slht - slcu;// lấy số lượng hiện tại trừ cho số lượng cũ = số lượng cập nhật
                            string capnhatTONGSL = (tongsl + slcapnhat).ToString();// lấy tổng số lượng trong kho cộng với số lượng cập nhật
                            string capnhatSLBAN = (slban + slcapnhat).ToString();// lấy số lượng bán cộng với số lượng cập nhật
                            // cập nhật lên bảng kho
                            drkho["TONGSOLUONG"] = capnhatTONGSL;
                            drkho["SLBAN"] = capnhatSLBAN;
                            // cập nhật lại kho
                            SqlCommandBuilder builderkho = new SqlCommandBuilder(ada_TonKho);
                            ada_TonKho.Update(conn.Dset, "KHO");
                            MessageBox.Show("Sửa thành công");
                            cboMaVT.Text = "";
                            txtSoLuong.TextAlign = 0;
                            txtDonViTinh.Clear();
                            txtDonGia.Clear();
                            btnGhi.Enabled = false;
                            HienThi();
                        }
                        else
                        {
                            MessageBox.Show("Hết hàng!!(Trong kho chỉ còn : "+tongsl+")");
                            txtSoLuong.Value = tongsl;
                            txtSoLuong.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vật tư này không có trong kho");
                        return;
                    }

                }
                catch
                {
                    MessageBox.Show("Lỗi!\n Xin vui lòng thử lại hoặc kiểm tra lại !!", "Thông báo!");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa vật tư này khỏi hóa đơn không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    if (tablepanelHoaDon.Enabled == false)
                    {
                        string mahd = txtMaHD.Text.Trim();
                        string mavt = cboMaVT.SelectedValue.ToString();
                        string dongia = txtDonGia.Text.Trim();
                        string soluong = txtSoLuong.Text.Trim();
                        string dvt = txtDonViTinh.Text.Trim();
                        int slcu = laySoLuongCu();//lấy số lượng cũ  
                        string sql1 = "select * from KHO where MAVT ='" + mavt + "'";
                        int tongsl = 0;
                        int slban = 0;
                        SqlDataReader drsl = conn.getReader(sql1);
                        while (drsl.Read())
                        {
                            tongsl = int.Parse(drsl["TONGSOLUONG"].ToString());// lấy tổng số lượng trong kho
                            slban = int.Parse(drsl["SLBAN"].ToString());// lấy số lượng bán
                        }
                        drsl.Close();
                        conn.ClosedConnection();

                        //kiểm tra chi tiết hóa đơn có vật tư đó trong hóa đơn đó chưa
                        DataRow dr1 = conn.Dset.Tables["CHITIETHOADON"].Rows.Find(new object[] { mahd, mavt });
                        if (dr1 == null)//nếu không có thi xoa hoa don nay
                        {
                            string sqlkt = "select count(*) from CHITIETHOADON where MAHD='" + mahd + "' AND MAVT ='" + mavt + "'";
                            bool kq1 = conn.checkForExistence(sqlkt);
                            //nếu không có thì xóa lun hóa đơn này này
                            if (kq1 == false)
                            {
                                tablepanelHoaDon.Enabled = true;
                                tablePanelChiTiet.Enabled = false;
                                DataRow dr = conn.Dset.Tables["HOADON"].Rows.Find(mahd);
                                if (dr == null)
                                {
                                    MessageBox.Show("Phiếu nhập này không tồn tại ");
                                    return;
                                }
                                dr.Delete();

                                SqlCommandBuilder builder1 = new SqlCommandBuilder(ada_HOADON);
                                ada_HOADON.Update(conn.Dset, "HOADON");
                                MessageBox.Show("Xóa phiếu nhập thành công");

                                txtSoLuong.TextAlign = 0;
                                load_Begin();
                                btnThem.Enabled = true;
                                return;
                            }
                        }
                        //có thì xóa vật tư đó ra khỏi hóa đơn 
                        dr1.Delete();


                        //cap nhat lai chi tiet hóa đơn
                        SqlCommandBuilder builder = new SqlCommandBuilder(ada_CTHOADON);
                        ada_CTHOADON.Update(conn.Dset, "CHITIETHOADON");

                        //cập nhật lại tổng tiền của hóa đơn đó
                        DataRow dr4 = conn.Dset.Tables["HOADON"].Rows.Find(mahd);
                        dr4["TONGTIEN"] = tinhTongTien().ToString();
                        SqlCommandBuilder builder4 = new SqlCommandBuilder(ada_HOADON);
                        ada_HOADON.Update(conn.Dset, "HOADON");
                        txtTongTien.Text = tinhTongTien().ToString();
                        // cập nhật lại kho sau khi xóa
                        // tìm vật tư đó trong kho
                        DataRow drkho = conn.Dset.Tables["KHO"].Rows.Find(mavt);
                        string capnhatTONGSL = (tongsl + slcu).ToString();
                        string capnhatSLBAN = (slban - slcu).ToString();
                        // cập nhật lên bảng kho
                        drkho["TONGSOLUONG"] = capnhatTONGSL;
                        drkho["SLBAN"] = capnhatSLBAN;
                        // cập nhật bên cơ sở dữ liệu
                        SqlCommandBuilder builderkho = new SqlCommandBuilder(ada_TonKho);
                        ada_TonKho.Update(conn.Dset, "KHO");
                        MessageBox.Show("Xóa thành công");

                        HienThi();

                        // kiểm tra xem chi tiết hóa đơn  này còn vật tư không
                        string sql = "select count(*) from CHITIETHOADON where MAHD='" + mahd + "'";
                        bool kq = conn.checkForExistence(sql);
                        //nếu không có thì xóa lun hóa đơn này này
                        if (kq == false)
                        {
                            tablepanelHoaDon.Enabled = true;
                            tablePanelChiTiet.Enabled = false;
                            DataRow dr = conn.Dset.Tables["HOADON"].Rows.Find(mahd);
                            if (dr == null)
                            {
                                MessageBox.Show("Phiếu nhập này không tồn tại ");
                                return;
                            }
                            dr.Delete();

                            SqlCommandBuilder builder1 = new SqlCommandBuilder(ada_HOADON);
                            ada_HOADON.Update(conn.Dset, "HOADON");
                            MessageBox.Show("Xóa phiếu nhập thành công");

                            txtSoLuong.TextAlign = 0;
                            load_Begin();
                            btnThem.Enabled = true;
                            return;
                        }
                        txtSoLuong.TextAlign = 0;
                        cboMaVT.Text = "";
                        txtDonViTinh.Clear();
                        txtDonGia.Clear();
                        return;
                    }
                    return;
                }
                catch
                {
                    MessageBox.Show("Lỗi!\n Xin vui lòng thử lại hoặc kiểm tra lại !!", "Thông báo!");
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // nếu nhấn thanh toán thì khách hàng không được sửa hóa đơn hay hủy hóa đơn này 
            //khóa tất cả các button xóa sửa ghi. chỉ được thêm hóa đơn mới
            if (MessageBox.Show("Bạn có chắc muốn thanh toán không ?\n Sau khi thanh toán khách hàng không được hủy hay sửa đơn hàng", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string tongtien = txtTongTien.Text;
                    string thaytongtien = "";
                    if (txtTienKhach.Text == "")
                    {
                        MessageBox.Show("Hãy nhập tiền khách đưa!!", "Chú ý!");
                        txtTienKhach.Focus();
                        return;
                    }
                    for (int i = 0; i < tongtien.Length; i++)
                    {
                        if (tongtien[i] != ',')
                        {
                            thaytongtien += tongtien[i];
                        }
                    }
                    string tienkhach = txtTienKhach.Text;
                    string thaytienkhach = "";
                    for (int i = 0; i < tienkhach.Length; i++)
                    {
                        if (tienkhach[i] != ',')
                        {
                            thaytienkhach += tienkhach[i];
                        }
                    }
                    double tt1 = double.Parse(thaytienkhach) - double.Parse(thaytongtien);
                    if (tt1 < 0)
                    {
                        MessageBox.Show("Khách hàng thanh toán chưa đủ!!\n Bạn còn thiếu " + tt1 + " VNĐ", "Lưu ý!!");
                        txtTienKhach.Clear();
                        txtTienKhach.Focus();
                        return;
                    }

                    string tt = tt1.ToString();
                    DataRow dr = conn.Dset.Tables["HOADON"].Rows.Find(txtMaHD.Text.Trim());
                    dr["TINHTRANGHD"] = "ĐÃ THANH TOÁN";
                    SqlCommandBuilder builder = new SqlCommandBuilder(ada_HOADON);
                    ada_HOADON.Update(conn.Dset, "HOADON");
                    MessageBox.Show("Thanh toán thành công");
                    txtTienThua.Text = tt;
                    btnThanhToan.Enabled = false;
                    btnInHD.Enabled = true;
                    btnThem.Enabled = true;
                    btnGhi.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    groupBox6.Enabled = false;
                    return;
                }
                catch
                {
                    MessageBox.Show("Lỗi!\n Xin vui lòng thử lại hoặc kiểm tra lại !!", "Thông báo!");
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtDonGia.Text, System.Globalization.NumberStyles.Currency);
                txtDonGia.Text = String.Format(culture, "{0:N0}", value);
                txtDonGia.Select(txtDonGia.Text.Length, 0);
                return;
            }
            catch
            {
                return;
            }
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtTongTien.Text, System.Globalization.NumberStyles.Currency);
                txtTongTien.Text = String.Format(culture, "{0:N0}", value);
                txtTongTien.Select(txtTongTien.Text.Length, 0);
                return;
            }
            catch
            {
                return;
            }
           
        }

        private void txtTienKhach_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtTienKhach.Text, System.Globalization.NumberStyles.Currency);
                txtTienKhach.Text = String.Format(culture, "{0:N0}", value);
                txtTienKhach.Select(txtTienKhach.Text.Length, 0);
                return;
            }
            catch
            {
                return;
            }
           
        }

        private void txtTienThua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtTienThua.Text, System.Globalization.NumberStyles.AllowThousands);
                txtTienThua.Text = String.Format(culture, "{0:N0}", value);
                txtTienThua.Select(txtTienThua.Text.Length, 0);
                return;
            }
            catch
            {
                return;
            }
                
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ctr = (Control)sender;
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(ctr, "Bạn chỉ có thể nhập số");
                return;
            }
            else
            {
                errorProvider1.Clear();
                return;
            }
        }

        private void txtTienKhach_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ctr = (Control)sender;
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(ctr, "Bạn chỉ có thể nhập số");
                return;
            }
            else
            {
                errorProvider1.Clear();
                return;
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            frmInPhieuXuatVT ds = new frmInPhieuXuatVT(txtMaHD.Text);
            ds.ShowDialog();
        }

        private void dgVDSMua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                cboMaVT.Text = dgVDSMua.Rows[index].Cells[2].Value.ToString();
                txtDonViTinh.Text = dgVDSMua.Rows[index].Cells[3].Value.ToString();
                txtSoLuong.Text = dgVDSMua.Rows[index].Cells[4].Value.ToString();
                txtDonGia.Text = dgVDSMua.Rows[index].Cells[5].Value.ToString();
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

        private void tablepanelHoaDon_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ctr = (Control)sender;
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(ctr, "Bạn chỉ có thể nhập số");
                return;
            }
            else
            {
                errorProvider1.Clear();
                return;
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

        private void frmBanVT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnThanhToan.Enabled == true || btnXoa.Enabled == true || btnSua.Enabled == true || btnGhi.Enabled == true)
            {
                MessageBox.Show("Bạn chưa hoàn tất hóa đơn này!!", "Chú ý!!");
                e.Cancel = true;
            }
            if (btnThanhToan.Enabled == false && btnXoa.Enabled == false && btnSua.Enabled == false && btnGhi.Enabled == false)
            {
                DialogResult r;
                r = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void txtDonViTinh_TextChanged(object sender, EventArgs e)
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
        }

        private void btnLS_Click(object sender, EventArgs e)
        {
            Form frm = frmTrangChu.ActiveForm;
            foreach (Form f in frm.MdiChildren)
            {
                if (f.Name == "frmLichSuHoaDonXuat")
                {
                    f.Activate();
                    return;
                }
            }

            frmLichSuHoaDonXuat frmNH = new frmLichSuHoaDonXuat();

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }
    }
}

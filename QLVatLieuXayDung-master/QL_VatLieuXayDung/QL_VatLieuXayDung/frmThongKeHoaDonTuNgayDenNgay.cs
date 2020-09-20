using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_VatLieuXayDung
{
    public partial class frmThongKeHoaDonTuNgayDenNgay : Form
    {
        KETNOI conn = new KETNOI();
        public frmThongKeHoaDonTuNgayDenNgay()
        {
            InitializeComponent();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            //string ngaybd = DateTime.ParseExact(dateTimePicker1.Text, "MM/dd/yyyy", null).ToString("yyyy-MM-dd");
            //string ngaykt = DateTime.ParseExact(dateTimePicker2.Text, "MM/dd/yyyy", null).ToString("yyyy-MM-dd");
            //InHDTuNgayDenNgay rpt = new InHDTuNgayDenNgay();
            //string sql = "select * from HOADON where left(NGAYHD,10) between '" + ngaybd + "' and '" + ngaykt + "'";
            //DataSet ds = conn.GrdSource(sql);
            //rpt.SetDataSource(ds.Tables[0]);
            //crystalReportViewer1.ReportSource = rpt;
            //crystalReportViewer1.RefreshReport();
        }
    }
}

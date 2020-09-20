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
    public partial class frmThongKeHoaDon : Form
    {
        public frmThongKeHoaDon()
        {
            InitializeComponent();
        }

        private void frmThongKeHoaDon_Load(object sender, EventArgs e)
        {

        }

        private void pckNgay_ValueChanged(object sender, EventArgs e)
        {
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                string ngay = DateTime.ParseExact(pckNgay.Text.Trim(), "MM/dd/yyyy", null).ToString("yyyy-MM-dd");
                frmInThongKeHDNgay frm = new frmInThongKeHDNgay(ngay);
                frm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Lỗi định dạng ngày !!\n Xin vui lòng cài đặt lại ngày hệ thống tháng/ngày/năm!!");
                return;
            }
        }
    }
}

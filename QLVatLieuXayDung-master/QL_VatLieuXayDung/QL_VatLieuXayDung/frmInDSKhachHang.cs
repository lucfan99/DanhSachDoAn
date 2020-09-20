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
    public partial class frmInDSKhachHang : Form
    {
        KETNOI conn = new KETNOI();
        public frmInDSKhachHang()
        {
            InitializeComponent();
        }

        private void frmInDSKhachHang_Load(object sender, EventArgs e)
        {
            InDSKhachHang rpt = new InDSKhachHang();
            rpt.DataSourceConnections[0].SetConnection(conn.ServerName1, conn.DataBase, conn.StrUser, conn.StrPass);
            //rpt.SetDatabaseLogon(frmKetNoiHeThong.Luu.user, frmKetNoiHeThong.Luu.pass);
            rpt.Refresh();
            ReportDSKhachHang.ReportSource = rpt;
            ReportDSKhachHang.DisplayToolbar = true;
            ReportDSKhachHang.DisplayStatusBar = false;
            ReportDSKhachHang.Refresh();
        }
    }
}

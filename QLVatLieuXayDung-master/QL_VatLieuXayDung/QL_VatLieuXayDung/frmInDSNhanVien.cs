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
    public partial class frmInDSNhanVien : Form
    {
        KETNOI conn = new KETNOI();
        public frmInDSNhanVien()
        {
            InitializeComponent();
        }

        private void frmInDSNhanVien_Load(object sender, EventArgs e)
        {
            InDSNhanVien rpt = new InDSNhanVien();
            rpt.DataSourceConnections[0].SetConnection(conn.ServerName1, conn.DataBase, conn.StrUser, conn.StrPass);
            //rpt.SetDatabaseLogon(frmKetNoiHeThong.Luu.user, frmKetNoiHeThong.Luu.pass);
            rpt.Refresh();
            ReportDSNhanVien.ReportSource = rpt;
            ReportDSNhanVien.DisplayToolbar = true;
            ReportDSNhanVien.DisplayStatusBar = false;
            ReportDSNhanVien.Refresh();
        }
    }
}

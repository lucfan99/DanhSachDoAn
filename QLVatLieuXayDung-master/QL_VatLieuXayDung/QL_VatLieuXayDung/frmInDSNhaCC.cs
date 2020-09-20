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
    public partial class frmInDSNhaCC : Form
    {
        KETNOI conn = new KETNOI();
        public frmInDSNhaCC()
        {
            InitializeComponent();
        }

        private void frmInDSNhaCC_Load(object sender, EventArgs e)
        {
            InDSNhaCC rpt = new InDSNhaCC();
            rpt.DataSourceConnections[0].SetConnection(conn.ServerName1, conn.DataBase, conn.StrUser, conn.StrPass);
            //rpt.SetDatabaseLogon(frmKetNoiHeThong.Luu.user, frmKetNoiHeThong.Luu.pass);
            rpt.Refresh();
            ReportDSNhaCC.ReportSource = rpt;
            ReportDSNhaCC.DisplayToolbar = true;
            ReportDSNhaCC.DisplayStatusBar = false;
            ReportDSNhaCC.Refresh();
        }
    }
}

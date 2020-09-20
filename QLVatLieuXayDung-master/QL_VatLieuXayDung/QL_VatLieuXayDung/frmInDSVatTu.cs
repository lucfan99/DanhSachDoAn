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
    public partial class frmInDSVatTu : Form
    {
        KETNOI conn = new KETNOI();
        public frmInDSVatTu()
        {
            InitializeComponent();
        }

        private void frmInDSVatTu_Load(object sender, EventArgs e)
        {
            InDSVatTu rpt = new InDSVatTu();
            rpt.DataSourceConnections[0].SetConnection(conn.ServerName1, conn.DataBase, conn.StrUser, conn.StrPass);
            //rpt.SetDatabaseLogon(frmKetNoiHeThong.Luu.user, frmKetNoiHeThong.Luu.pass);
            rpt.Refresh();
            ReportDSVatTu.ReportSource = rpt;
            ReportDSVatTu.DisplayToolbar = true;
            ReportDSVatTu.DisplayStatusBar = false;
            ReportDSVatTu.Refresh();
        }
    }
}

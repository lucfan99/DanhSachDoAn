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
    public partial class frmDSKho : Form
    {
        KETNOI conn = new KETNOI();
        public frmDSKho()
        {
            InitializeComponent();
        }

        private void frmDSKho_Load(object sender, EventArgs e)
        {
            InDSKho rpt = new InDSKho();
            rpt.DataSourceConnections[0].SetConnection(conn.ServerName1, conn.DataBase,conn.StrUser, conn.StrPass);
            rpt.Refresh();
            ReportDSKho.ReportSource = rpt;
            ReportDSKho.DisplayToolbar = true;
            ReportDSKho.DisplayStatusBar = false;
            ReportDSKho.Refresh();
        }
    }
}

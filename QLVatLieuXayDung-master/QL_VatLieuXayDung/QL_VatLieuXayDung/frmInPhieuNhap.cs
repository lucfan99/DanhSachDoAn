using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace QL_VatLieuXayDung
{
    public partial class frmInPhieuNhap : Form
    {
        KETNOI conn = new KETNOI();
        private string p = "";

        public frmInPhieuNhap(string ma)
        {
            InitializeComponent();
            this.p = ma;
        }

        private void frmInPhieuNhap_Load(object sender, EventArgs e)
        {
            InPhieuNhap rpt = new InPhieuNhap();
            rpt.DataSourceConnections[0].SetConnection(conn.ServerName1, conn.DataBase, conn.StrUser, conn.StrPass);
            //rpt.SetDatabaseLogon(frmKetNoiHeThong.Luu.user, frmKetNoiHeThong.Luu.pass);
            rpt.Refresh();
            ParameterFieldDefinitions pFielDefinitions = rpt.DataDefinition.ParameterFields;
            ParameterFieldDefinition pfdMaPN = pFielDefinitions["pMaPN"];
            ParameterDiscreteValue pdvMalop = new ParameterDiscreteValue();
            pdvMalop.Value = p;
            pfdMaPN.CurrentValues.Clear();
            pfdMaPN.CurrentValues.Add(pdvMalop);
            pfdMaPN.ApplyCurrentValues(pfdMaPN.CurrentValues);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.Refresh();
        }
    }
}

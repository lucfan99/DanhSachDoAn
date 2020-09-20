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
    public partial class frmInPhieuXuatVT : Form
    {
        KETNOI conn = new KETNOI();
        public static string p = "";
        public frmInPhieuXuatVT(string ma)
        {
            InitializeComponent();
            p = ma;
        }

        private void frmInPhieuXuatVT_Load(object sender, EventArgs e)
        {
            InPhieuXuatVT rpt = new InPhieuXuatVT();
            rpt.DataSourceConnections[0].SetConnection(conn.ServerName1, conn.DataBase, conn.StrUser, conn.StrPass);
            //rpt.SetDatabaseLogon(frmKetNoiHeThong.Luu.user, frmKetNoiHeThong.Luu.pass);
            ParameterFieldDefinitions pFielDefinitions = rpt.DataDefinition.ParameterFields;
            ParameterFieldDefinition pfdMaHD = pFielDefinitions["pMaHD"];
            ParameterDiscreteValue pdvMalop = new ParameterDiscreteValue();
            pdvMalop.Value = p;
            pfdMaHD.CurrentValues.Clear();
            pfdMaHD.CurrentValues.Add(pdvMalop);
            pfdMaHD.ApplyCurrentValues(pfdMaHD.CurrentValues);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.Refresh();
        }
    }
}

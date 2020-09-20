using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class frmInThongKeHDNgay : Form
    {
        public static string p = "";
        KETNOI conn = new KETNOI();
        public frmInThongKeHDNgay(string ngay)
        {
            InitializeComponent();
            p = ngay;
        }

        private void frmInThongKeHDNgay_Load(object sender, EventArgs e)
        {
            InDSHoaDonTheoNgay rpt = new InDSHoaDonTheoNgay();
            rpt.DataSourceConnections[0].SetConnection(conn.ServerName1, conn.DataBase, conn.StrUser, conn.StrPass);
            //rpt.SetDatabaseLogon(frmKetNoiHeThong.Luu.user, frmKetNoiHeThong.Luu.pass);
            rpt.Refresh();
            ParameterFieldDefinitions pFielDefinitions = rpt.DataDefinition.ParameterFields;
            ParameterFieldDefinition pfdMaPN = pFielDefinitions["pNgayHD"];
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

namespace QL_VatLieuXayDung
{
    partial class frmInDSNhanVien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ReportDSNhanVien = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ReportDSNhanVien
            // 
            this.ReportDSNhanVien.ActiveViewIndex = -1;
            this.ReportDSNhanVien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportDSNhanVien.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportDSNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportDSNhanVien.Location = new System.Drawing.Point(0, 0);
            this.ReportDSNhanVien.Name = "ReportDSNhanVien";
            this.ReportDSNhanVien.ShowExportButton = false;
            this.ReportDSNhanVien.ShowGroupTreeButton = false;
            this.ReportDSNhanVien.ShowParameterPanelButton = false;
            this.ReportDSNhanVien.ShowRefreshButton = false;
            this.ReportDSNhanVien.Size = new System.Drawing.Size(774, 343);
            this.ReportDSNhanVien.TabIndex = 0;
            this.ReportDSNhanVien.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmInDSNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 343);
            this.Controls.Add(this.ReportDSNhanVien);
            this.Name = "frmInDSNhanVien";
            this.Text = "Danh sách nhân viên";
            this.Load += new System.EventHandler(this.frmInDSNhanVien_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReportDSNhanVien;
    }
}
namespace QL_VatLieuXayDung
{
    partial class frmInDSNhaCC
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
            this.ReportDSNhaCC = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ReportDSNhaCC
            // 
            this.ReportDSNhaCC.ActiveViewIndex = -1;
            this.ReportDSNhaCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportDSNhaCC.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportDSNhaCC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportDSNhaCC.Location = new System.Drawing.Point(0, 0);
            this.ReportDSNhaCC.Name = "ReportDSNhaCC";
            this.ReportDSNhaCC.ShowExportButton = false;
            this.ReportDSNhaCC.ShowGroupTreeButton = false;
            this.ReportDSNhaCC.ShowParameterPanelButton = false;
            this.ReportDSNhaCC.ShowRefreshButton = false;
            this.ReportDSNhaCC.Size = new System.Drawing.Size(656, 374);
            this.ReportDSNhaCC.TabIndex = 0;
            this.ReportDSNhaCC.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmInDSNhaCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 374);
            this.Controls.Add(this.ReportDSNhaCC);
            this.Name = "frmInDSNhaCC";
            this.Text = "Danh sách nhà cung cấp";
            this.Load += new System.EventHandler(this.frmInDSNhaCC_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReportDSNhaCC;
    }
}
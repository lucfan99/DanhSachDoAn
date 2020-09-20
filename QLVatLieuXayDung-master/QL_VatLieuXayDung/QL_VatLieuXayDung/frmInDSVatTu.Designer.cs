namespace QL_VatLieuXayDung
{
    partial class frmInDSVatTu
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
            this.ReportDSVatTu = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ReportDSVatTu
            // 
            this.ReportDSVatTu.ActiveViewIndex = -1;
            this.ReportDSVatTu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportDSVatTu.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportDSVatTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportDSVatTu.Location = new System.Drawing.Point(0, 0);
            this.ReportDSVatTu.Name = "ReportDSVatTu";
            this.ReportDSVatTu.ShowExportButton = false;
            this.ReportDSVatTu.ShowGroupTreeButton = false;
            this.ReportDSVatTu.ShowParameterPanelButton = false;
            this.ReportDSVatTu.ShowRefreshButton = false;
            this.ReportDSVatTu.Size = new System.Drawing.Size(680, 343);
            this.ReportDSVatTu.TabIndex = 0;
            this.ReportDSVatTu.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmInDSVatTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 343);
            this.Controls.Add(this.ReportDSVatTu);
            this.Name = "frmInDSVatTu";
            this.Text = "Danh sách vật tư";
            this.Load += new System.EventHandler(this.frmInDSVatTu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReportDSVatTu;
    }
}
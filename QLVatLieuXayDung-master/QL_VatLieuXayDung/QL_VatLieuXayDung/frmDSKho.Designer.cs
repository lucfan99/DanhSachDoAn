namespace QL_VatLieuXayDung
{
    partial class frmDSKho
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
            this.ReportDSKho = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ReportDSKho
            // 
            this.ReportDSKho.ActiveViewIndex = -1;
            this.ReportDSKho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportDSKho.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportDSKho.DisplayStatusBar = false;
            this.ReportDSKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportDSKho.Location = new System.Drawing.Point(0, 0);
            this.ReportDSKho.Name = "ReportDSKho";
            this.ReportDSKho.ShowExportButton = false;
            this.ReportDSKho.ShowGroupTreeButton = false;
            this.ReportDSKho.ShowParameterPanelButton = false;
            this.ReportDSKho.ShowRefreshButton = false;
            this.ReportDSKho.Size = new System.Drawing.Size(715, 344);
            this.ReportDSKho.TabIndex = 0;
            this.ReportDSKho.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmDSKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 344);
            this.Controls.Add(this.ReportDSKho);
            this.Name = "frmDSKho";
            this.Text = "Danh sách kho";
            this.Load += new System.EventHandler(this.frmDSKho_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReportDSKho;
    }
}
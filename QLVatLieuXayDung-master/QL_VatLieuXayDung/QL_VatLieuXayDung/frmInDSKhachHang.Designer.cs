namespace QL_VatLieuXayDung
{
    partial class frmInDSKhachHang
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
            this.ReportDSKhachHang = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ReportDSKhachHang
            // 
            this.ReportDSKhachHang.ActiveViewIndex = -1;
            this.ReportDSKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportDSKhachHang.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportDSKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportDSKhachHang.Location = new System.Drawing.Point(0, 0);
            this.ReportDSKhachHang.Name = "ReportDSKhachHang";
            this.ReportDSKhachHang.ShowExportButton = false;
            this.ReportDSKhachHang.ShowGroupTreeButton = false;
            this.ReportDSKhachHang.ShowParameterPanelButton = false;
            this.ReportDSKhachHang.ShowRefreshButton = false;
            this.ReportDSKhachHang.Size = new System.Drawing.Size(656, 357);
            this.ReportDSKhachHang.TabIndex = 0;
            this.ReportDSKhachHang.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmInDSKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 357);
            this.Controls.Add(this.ReportDSKhachHang);
            this.Name = "frmInDSKhachHang";
            this.Text = "Danh sách khách hàng";
            this.Load += new System.EventHandler(this.frmInDSKhachHang_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReportDSKhachHang;
    }
}
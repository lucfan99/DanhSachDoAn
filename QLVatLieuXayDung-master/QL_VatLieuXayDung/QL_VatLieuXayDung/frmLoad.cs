using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace QL_VatLieuXayDung
{
    public partial class frmLoad : Form
    {
        public frmLoad()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                rectangleShape2.Width += 3;
                if (rectangleShape2.Width >= 640)
                {
                    timer1.Stop();
                    this.Hide();
                    frmDangNhap dn = new frmDangNhap();
                    dn.ShowDialog();
                    this.Close();
                }
                if (rectangleShape2.Width >= 350 || rectangleShape2.Width == 500)
                {
                    rectangleShape2.Width += 30;
                }
            }
            catch(Exception)
            {
                return;
            }
        }

        private void frmLoad_Load(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
        }
    }
}

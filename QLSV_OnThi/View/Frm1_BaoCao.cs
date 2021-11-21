using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSV_OnThi.Report;

namespace QLSV_OnThi.View
{
    public partial class Frm1_BaoCao : Form
    {
        public Frm1_BaoCao()
        {
            InitializeComponent();
        }

        private void Frm1_BaoCao_Load(object sender, EventArgs e)
        {
            BaoCao bc = new BaoCao();
            bc.SetDatabaseLogon("sa", "Admin1234", "101.96.66.219,8013", "QLSinhVien");
            crv.ReportSource = bc;
        }
    }
}

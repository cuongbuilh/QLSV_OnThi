using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSV_OnThi.Utils;

namespace QLSV_OnThi.View
{
    public partial class Frm1_TimKiem : Form
    {
        private AdoUtils adoUtils;
        public Frm1_TimKiem()
        {
            InitializeComponent();
            adoUtils = new AdoUtils();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            string maLop = txt_MaLop.Text;
            string tenLop = txt_TenLop.Text;
            bool byName = chk_TenLop.Checked;
            bool byID = chk_MaLop.Checked;

            string sql =
                "select SINHVIEN.MALOP,TENLOP,MASV,HOTEN,GIOITINH,NGAYSINH,NOISINH,SINHVIEN.MAKHOA,MAHE,CVHT from SINHVIEN join LOP on SINHVIEN.MALOP = LOP.MALOP where 1=1";

            if (byName)
            {
                sql = sql + " and LOP.TENLOP like '%"+tenLop+"%'";
            }

            if (byID)
            {
                sql = sql + " and LOP.MALOP like '%" + maLop + "%'";
            }

            data_TimKiem.DataSource = adoUtils.GetDataTable(sql);
        }

        private void Frm1_TimKiem_Load(object sender, EventArgs e)
        {
            string sql =
                "select SINHVIEN.MALOP,TENLOP,MASV,HOTEN,GIOITINH,NGAYSINH,NOISINH,SINHVIEN.MAKHOA,MAHE,CVHT from SINHVIEN join LOP on SINHVIEN.MALOP = LOP.MALOP where 1=1";
            data_TimKiem.DataSource = adoUtils.GetDataTable(sql);
        }
    }
}

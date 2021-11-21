using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSV_OnThi.Utils;

namespace QLSV_OnThi.View
{
    public partial class Frm1_DangNhap : Form
    {
        public Frm1_DangNhap()
        {
            InitializeComponent();
        }

        private void Frm1_DangNhap_Load(object sender, EventArgs e)
        {
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            // lấy dữ liệu từ form
            string taiKhoan = txt_TenDangNhap.Text;
            string matKhau = txt_MatKhau.Text;

            // sql
            string selectFomat =
                "select * from HETHONG where BINARY_CHECKSUM(TENDN) = BINARY_CHECKSUM('{0}') and BINARY_CHECKSUM(MATKHAU) = BINARY_CHECKSUM('{1}') ";
            string sql = String.Format(selectFomat, taiKhoan, matKhau);

            // kiểm tra
            AdoUtils adoUtils = new AdoUtils();
            bool logedIn = adoUtils.ExcuteReader(sql).Read();

            // mở form
            if (logedIn)
            {
                MDI1_QLSV frm = new MDI1_QLSV();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "message");
            }
        }
    }
}
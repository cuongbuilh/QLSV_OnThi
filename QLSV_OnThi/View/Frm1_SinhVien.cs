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
    public partial class Frm1_SinhVien : Form
    {
        private AdoUtils adoUtils;
        public Frm1_SinhVien()
        {
            InitializeComponent();
            adoUtils = new AdoUtils();
        }

        private void Frm1_SinhVien_Load(object sender, EventArgs e)
        {
            LoadDataToForm();
            BindingData();
        }

        private void LoadDataToForm()
        {
            data_SinhVien.DataSource = adoUtils.GetDataTable("select * from SINHVIEN");

            cbo_Khoa.DataSource = adoUtils.GetDataTable("select * from KHOAVIEN");
            cbo_Khoa.DisplayMember = "TENKHOA";
            cbo_Khoa.ValueMember = "MAKHOA";

            cbo_He.DataSource = adoUtils.GetDataTable("select * from HEDAOTAO");
            cbo_He.DisplayMember = "TENHE";
            cbo_He.ValueMember = "MAHE";

            cbo_Lop.DataSource = adoUtils.GetDataTable("select * from LOP");
            cbo_Lop.DisplayMember = "TENLOP";
            cbo_Lop.ValueMember = "MALOP";

            List<string> gt = new List<string>();
            gt.Add("0");
            gt.Add("1");
            cbo_GioiTinh.DataSource = gt;

            BindingData();
        }

        private void BindingData()
        {
            txt_HoTen.DataBindings.Clear();
            txt_HoTen.DataBindings.Add("text", data_SinhVien.DataSource, "HOTEN");

            txt_MaSinhVien.DataBindings.Clear();
            txt_MaSinhVien.DataBindings.Add("text", data_SinhVien.DataSource, "MASV");

            txt_NoiSinh.DataBindings.Clear();
            txt_NoiSinh.DataBindings.Add("text", data_SinhVien.DataSource, "NOISINH");

            date_NgaySinh.DataBindings.Clear();
            date_NgaySinh.DataBindings.Add("text", data_SinhVien.DataSource, "NGAYSINH");

            cbo_He.DataBindings.Clear();
            cbo_He.DataBindings.Add("text", data_SinhVien.DataSource, "MAHE");

            cbo_Lop.DataBindings.Clear();
            cbo_Lop.DataBindings.Add("text", data_SinhVien.DataSource, "MALOP");

            cbo_Khoa.DataBindings.Clear();
            cbo_Khoa.DataBindings.Add("text", data_SinhVien.DataSource, "MAKHOA");

         
            cbo_GioiTinh.DataBindings.Clear();
            cbo_GioiTinh.DataBindings.Add("text", data_SinhVien.DataSource, "GIOITINH");

        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            txt_HoTen.Text = "";
            txt_MaSinhVien.Text = "";
            txt_NoiSinh.Text = "";
            
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string msv = txt_MaSinhVien.Text;
            string prepare = "delete from SINHVIEN where MASV = '{0}'";
            string sql = String.Format(prepare, msv);

            DialogResult confirmDialogResult = MessageBox.Show("Bạn muốn xóa sinh viên với msv =  " + msv);
            if (confirmDialogResult == DialogResult.OK)
            {
                try
                {
                    adoUtils.Excute(sql);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            LoadDataToForm();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string msv = txt_MaSinhVien.Text;
            string hoTen = txt_HoTen.Text;
            string noiSinh = txt_NoiSinh.Text;
            DateTime ngaysinh = date_NgaySinh.Value;
            string gioiTinh = (string)cbo_GioiTinh.SelectedValue;
            string malop = (string)cbo_Lop.SelectedValue;
            string maKhoa = (string)cbo_Khoa.SelectedValue;
            string maHe = (string)cbo_He.SelectedValue;

            string prepapre =
                "update SINHVIEN set HOTEN = '{0}', GIOITINH = '{1}', NGAYSINH='{2}', NOISINH='{3}', MAKHOA='{4}', MALOP='{5}', MAHE = '{6}' where MASV='{7}';";

            string sql = String.Format(prepapre, hoTen, gioiTinh, ngaysinh.ToString(), noiSinh, maKhoa, malop, maHe, msv);

            try
            {
                adoUtils.Excute(sql);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            LoadDataToForm();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            // lấy thông tin từ form
            string msv = txt_MaSinhVien.Text;
            string hoTen = txt_HoTen.Text;
            string noiSinh = txt_NoiSinh.Text;
            DateTime ngaysinh = date_NgaySinh.Value;
            string gioiTinh = (string)cbo_GioiTinh.SelectedValue;
            string malop = (string)cbo_Lop.SelectedValue;
            string maKhoa = (string)cbo_Khoa.SelectedValue;
            string maHe = (string)cbo_He.SelectedValue;

            // kiểm tra trùng mã sv;
            string check_sql = "select * from SINHVIEN where MASV = '{0}'";
            bool msv_exsist = adoUtils.ExcuteReader(String.Format(check_sql, msv)).Read();

            if (msv_exsist)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Message");
                return;
            }


            // thêm sinh viên
            string prepare =
                "insert into SINHVIEN values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');";
            string sql = String.Format(prepare, msv, hoTen, gioiTinh, ngaysinh.ToString(), noiSinh, maKhoa, malop, maHe);

            try
            {
                adoUtils.Excute(sql);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            LoadDataToForm();
            
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

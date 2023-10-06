using QuanLyNhaSachPN.DAO;
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
namespace QuanLyNhaSachPN
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        public static string Taikhoan;
        Connect con = new Connect();
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            Taikhoan = txtTaiKhoan.Text;
            string query = string.Format("select * from NguoiDung where taikhoan = '{0}' and matkhau = '{1}'"
                , txtTaiKhoan.Text, txtMatKhau.Text);
            DataSet ds = con.LayDuLieu(query);
            if(txtTaiKhoan.Text == "" && txtMatKhau.Text =="")
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!");
            }
            else
            {
                if (txtTaiKhoan.Text == "")
                {
                    MessageBox.Show("Chưa nhập tài khoản!");
                }
                else if (txtMatKhau.Text == "")
                {
                    MessageBox.Show("Chưa nhập mật khẩu!");
                }
                else
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        MessageBox.Show("Đăng nhập thành công");
                        TrangChu frm = new TrangChu();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại");
                    }
                }
            }

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            DangKy frm = new DangKy();
            frm.Show();
            this.Hide();
        }
    }
}

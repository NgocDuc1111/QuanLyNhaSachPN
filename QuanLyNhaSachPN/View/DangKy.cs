using QuanLyNhaSachPN.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaSachPN
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }
        Connect con = new Connect();
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            DangNhap frm = new DangNhap();
            frm.Show();
            this.Hide();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string chucDanh;
            if(rbtnNhanVien.Checked)
            {
                chucDanh = rbtnNhanVien.Text;
            }
            else 
            {
                chucDanh = rbtnQuanLy.Text;
            }
            
            string checkTK = string.Format("select * from NguoiDung where taikhoan = '{0}'"
                , txtTaiKhoan.Text);
            DataSet ds = con.LayDuLieu(checkTK);
            if (ds.Tables[0].Rows.Count == 0)
            {
                if (txtMatKhau.Text == txtNhapLaiMatKhau.Text)
                {
                    string query = string.Format("insert into NguoiDung values('{0}','{1}','{2}')"
                    , txtTaiKhoan.Text, txtMatKhau.Text, chucDanh);
                    bool result = con.ThucThi(query);
                    if (result)
                    {
                        MessageBox.Show("Đăng kí thành công");
                        txtTaiKhoan.Text = "";
                        txtMatKhau.Text = "";
                        txtNhapLaiMatKhau.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Đăng kí thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Nhập lại mật khẩu không đúng");
                }
            }
            else
            {
                MessageBox.Show("Tài khoản đã tồn tại!");
            }    
        }
    }
}

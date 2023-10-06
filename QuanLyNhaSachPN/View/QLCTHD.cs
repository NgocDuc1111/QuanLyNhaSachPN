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

namespace QuanLyNhaSachPN.View
{
    public partial class QLCTHD : Form
    {
        Connect kn = new Connect();
        public QLCTHD()
        {
            InitializeComponent();
        }
        public void getdata()
        {
            string query = "Select * from CHITIETHOADON";
            DataSet ds = kn.LayDuLieu(query);
            dgvCTHD.DataSource = ds.Tables[0];
        }
        public void clear()
        {
            txtMahd.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMahd.Text = "";
            txtMahang.Text = "";
            txtSoluong.Text = "";
            txtGiatien.Text = "";
        }
        private void QLCTHD_Load(object sender, EventArgs e)
        {
            getdata();
        }

        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMahd.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;

                txtMahd.Text = dgvCTHD.Rows[r].Cells["MAHD"].Value.ToString();
                txtMahang.Text = dgvCTHD.Rows[r].Cells["MAHANG"].Value.ToString();
                txtSoluong.Text = dgvCTHD.Rows[r].Cells["SOLUONG"].Value.ToString();
                txtGiatien.Text = dgvCTHD.Rows[r].Cells["GIATIEN"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string checkQuery = string.Format("SELECT COUNT(*) FROM CHITIETHOADON WHERE MAHD = N'{0}'", txtMahd.Text);
            int existingRecords = (int)kn.LayDuLieu(checkQuery).Tables[0].Rows[0][0];
            if (string.IsNullOrWhiteSpace(txtMahd.Text) ||
                string.IsNullOrWhiteSpace(txtMahang.Text) ||
                string.IsNullOrWhiteSpace(txtGiatien.Text) ||
                string.IsNullOrWhiteSpace(txtSoluong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return; // Dừng thực hiện khi chưa nhập đủ thông tin
            }
            string query = string.Format("INSERT into CHITIETHOADON values(N'{0}',N'{1}',N'{2}',N'{3}')",
                txtMahd.Text,
                txtMahang.Text,
                txtSoluong.Text,
                txtGiatien.Text
                    );
            if (existingRecords > 0)
            {
                // Mã hàng đã tồn tại, hiển thị thông báo lỗi
                MessageBox.Show("Mã hàng đã tồn tại. Vui lòng nhập mã hàng khác.");
            }
            else
            {
                DataSet ds = kn.LayDuLieu(query);
                bool kt = kn.ThucThi(query);
                if (kt == true)
                {
                    MessageBox.Show("Thêm thành công");
                    btnReset.PerformClick();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = string.Format("update CHITIETHOADON set " +
                "MAHANG = N'{1}', SOLUONG=N'{2}', GIATIEN = N'{3}' where MAHD=N'{0}'",
                txtMahd.Text,
                txtMahang.Text,
                txtSoluong.Text,
                txtGiatien.Text
                );
            DataSet ds = kn.LayDuLieu(query);
            bool kt = kn.ThucThi(query);
            if (kt == true)
            {
                MessageBox.Show("Sửa thành công");
                btnReset.PerformClick();
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string query = string.Format("Delete CHITIETHOADON where MAHD=N'{0}'", txtMahd.Text);
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataSet ds = kn.LayDuLieu(query);
                bool kt = kn.ThucThi(query);
                if (kt)
                {
                    MessageBox.Show("Xóa thành công");
                    btnReset.PerformClick();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            getdata();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = string.Format("select * from CHITIETHOADON where " +
                "MAHD like N'%{0}%' or " +
                "MAHANG like N'%{0}%' or " +
                "SOLUONG like N'%{0}%' or " +
                "GIATIEN like N'%{0}%' ",
               txtTim.Text
               );
            try
            {
                DataSet ds = kn.LayDuLieu(query);

                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvCTHD.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("No data found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}

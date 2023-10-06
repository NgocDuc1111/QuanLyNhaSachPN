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
    public partial class QLHang : Form
    {
        Connect kn = new Connect();
        public QLHang()
        {
            InitializeComponent();
        }
        public void getdata()
        {
            string query = "select * from HANG";
            DataSet ds = kn.LayDuLieu(query);
            dgvQLhang.DataSource = ds.Tables[0];
        }
        public void clear()
        {
            txtMahang.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMahang.Text = "";
            txtTenhang.Text = "";
            txtDVT.Text = "";
            txtSoluong.Text = "";
            txtDongia.Text = "";
            txtTim.Text = "";
        }
        private void QLHang_Load(object sender, EventArgs e)
        {
            getdata();
        }

        private void dgvQLhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMahang.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;

                txtMahang.Text = dgvQLhang.Rows[r].Cells["MAHANG"].Value.ToString();
                txtTenhang.Text = dgvQLhang.Rows[r].Cells["TENHANG"].Value.ToString();
                txtDVT.Text = dgvQLhang.Rows[r].Cells["DONVITINH"].Value.ToString();
                txtSoluong.Text = dgvQLhang.Rows[r].Cells["SOLUONG"].Value.ToString();
                txtDongia.Text = dgvQLhang.Rows[r].Cells["DONGIA"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string checkQuery = string.Format("SELECT COUNT(*) FROM HANG WHERE MAHANG = N'{0}'", txtMahang.Text);
            int existingRecords = (int)kn.LayDuLieu(checkQuery).Tables[0].Rows[0][0];
            if (string.IsNullOrWhiteSpace(txtMahang.Text) ||
                string.IsNullOrWhiteSpace(txtTenhang.Text) ||
                string.IsNullOrWhiteSpace(txtDVT.Text) ||
                string.IsNullOrWhiteSpace(txtSoluong.Text) ||
                string.IsNullOrWhiteSpace(txtDongia.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return; // Dừng thực hiện khi chưa nhập đủ thông tin
            }
            string query = string.Format("insert into HANG values(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}')",
                txtMahang.Text,
                txtTenhang.Text,
                txtSoluong.Text,
                txtDVT.Text,
                txtDongia.Text
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
            string query = string.Format("update HANG set " +
                "TENHANG=N'{1}', SOLUONG=N'{2}', DONVITINH=N'{3}', DONGIA=N'{4}' where MAHANG=N'{0}'",
                txtMahang.Text,
                txtTenhang.Text,
                txtSoluong.Text,
                txtDVT.Text,
                txtDongia.Text
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
            string query = string.Format("Delete HANG where MAHANG=N'{0}'", txtMahang.Text);
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
            string query = string.Format("select * from HANG where " +
                "MAHANG like N'%{0}%' or " +
                "TENHANG like N'%{0}%' or " +
                "SOLUONG like N'%{0}%' or " +
                "DONVITINH like N'%{0}%' or " +
                "DONGIA like N'%{0}%' ",
                txtTim.Text
                );
            try
            {
                DataSet ds = kn.LayDuLieu(query);

                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvQLhang.DataSource = ds.Tables[0];
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

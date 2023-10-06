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
    public partial class QLNCC : Form
    {
        Connect kn = new Connect();
        public QLNCC()
        {
            InitializeComponent();
        }
        public void getdata()
        {
            string query = "Select * from NHACUNGCAP";
            DataSet ds = kn.LayDuLieu(query);
            dgvNCC.DataSource = ds.Tables[0];
        }
        public void clear()
        {
            txtMaNCC.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtSDT.Text = "";
            rtxDiachi.Text = "";
        }
        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaNCC.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;

                txtMaNCC.Text = dgvNCC.Rows[r].Cells["MANCC"].Value.ToString();
                txtTenNCC.Text = dgvNCC.Rows[r].Cells["TENNCC"].Value.ToString();
                txtSDT.Text = dgvNCC.Rows[r].Cells["SDT"].Value.ToString();
                rtxDiachi.Text = dgvNCC.Rows[r].Cells["DIACHI"].Value.ToString();
            }
        }

        private void QLNCC_Load(object sender, EventArgs e)
        {
            getdata();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string checkQuery = string.Format("SELECT COUNT(*) FROM NHACUNGCAP WHERE MANCC = N'{0}'", txtMaNCC.Text);
            int existingRecords = (int)kn.LayDuLieu(checkQuery).Tables[0].Rows[0][0];
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text) ||
                string.IsNullOrWhiteSpace(txtTenNCC.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(rtxDiachi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return; // Dừng thực hiện khi chưa nhập đủ thông tin
            }

            string query = string.Format("insert into NHACUNGCAP values(N'{0}',N'{1}','{2}',N'{3}')",
                txtMaNCC.Text,
                txtTenNCC.Text,
                rtxDiachi.Text,
                txtSDT.Text
                    );
            if (existingRecords > 0)
            {
                // Mã nhân viên đã tồn tại, hiển thị thông báo lỗi
                MessageBox.Show("Mã nhà cung cấp đã tồn tại. Vui lòng nhập mã khác.");
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
            string query = string.Format("update NHACUNGCAP set TENNCC=N'{1}', DIACHI=N'{2}', SDT=N'{3}' where MANCC=N'{0}'",
               txtMaNCC.Text,
                txtTenNCC.Text,
                rtxDiachi.Text,
                txtSDT.Text
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
            string query = string.Format("DELETE FROM NHACUNGCAP WHERE MANCC=N'{0}'", txtMaNCC.Text);
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataSet ds = kn.LayDuLieu(query);
                bool kt = kn.ThucThi(query);

                if (kt==true)
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            string query = string.Format("select * from NHACUNGCAP where " +
                "MANCC like N'%{0}%' or " +
                "TENNCC like N'%{0}%' or " +
                "DIACHI like N'%{0}%' or " +
                "SDT like N'%{0}%' ",
                txtTim.Text
                );
            try
            {
                DataSet ds = kn.LayDuLieu(query);

                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvNCC.DataSource = ds.Tables[0];
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            clear();
            getdata();
        }
    }
}

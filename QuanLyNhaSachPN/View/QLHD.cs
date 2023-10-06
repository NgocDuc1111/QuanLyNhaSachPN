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
    public partial class QLHD : Form
    {
        Connect kn = new Connect();
        public QLHD()
        {
            InitializeComponent();
        }
        public void getdata()
        {
            string query = "select * from HOADON";
            DataSet ds = kn.LayDuLieu(query);
            dgvHoaDon.DataSource = ds.Tables[0];
        }
        public void clear()
        {
            txtMaHD.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = true;

            txtMaHD.Text = "";
            txtMaNV.Text = "";
            dtpNglap.Value = DateTime.Now;
            txtThanhtien.Text = "";
            txtTim.Text = "";
        }
        private void QLHD_Load(object sender, EventArgs e)
        {
            getdata();
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaHD.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;

                txtMaHD.Text = dgvHoaDon.Rows[r].Cells["MAHD"].Value.ToString();
                txtMaNV.Text = dgvHoaDon.Rows[r].Cells["MANV"].Value.ToString();
                dtpNglap.Text = dgvHoaDon.Rows[r].Cells["NGAYLAP"].Value.ToString();
                txtThanhtien.Text = dgvHoaDon.Rows[r].Cells["THANHTIEN"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string checkQuery = string.Format("SELECT COUNT(*) FROM HOADON = N'{0}'", txtMaHD.Text);
            int existingRecords = (int)kn.LayDuLieu(checkQuery).Tables[0].Rows[0][0];
            if (string.IsNullOrWhiteSpace(txtMaHD.Text) ||
                string.IsNullOrWhiteSpace(txtMaNV.Text) ||
                string.IsNullOrWhiteSpace(txtThanhtien.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return; // Dừng thực hiện khi chưa nhập đủ thông tin
            }
            string query = string.Format("insert into HOADON values(N'{0}',N'{1}',N'{2}',N'{3}')",
                txtMaHD.Text,
                txtMaNV.Text,
                dtpNglap.Value.ToString("yyyy/MM/dd"),
                txtThanhtien.Text
                    );
            if (existingRecords > 0)
            {
                // Mã hóa đơn đã tồn tại, hiển thị thông báo lỗi
                MessageBox.Show("Mã hóa đơn đã tồn tại. Vui lòng nhập mã hóa đơn khác.");
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
            string query = string.Format("update HOADON set " +
                "MANV=N'{1}', NGAYLAP=N'{2}', THANHTIEN=N'{3}' where MAHD=N'{0}'",
                txtMaHD.Text,
                txtMaNV.Text,
                dtpNglap.Value.ToString("yyyy/MM/dd"),
                txtThanhtien.Text
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
            string query = string.Format("Delete HOADON where MAHD=N'{0}'", txtMaHD.Text);
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            string query = string.Format("select * from HOADON where " +
                "MAHD like N'%{0}%' or " +
                "MANV like N'%{0}%' or " +
                "NGAYLAP like N'%{0}%' or " +
                "THANHTIEN like N'%{0}%' ",
                txtTim.Text
                );
            try
            {
                DataSet ds = kn.LayDuLieu(query);

                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvHoaDon.DataSource = ds.Tables[0];
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

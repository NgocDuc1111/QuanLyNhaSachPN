using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyNhaSachPN.DAO;
namespace QuanLyNhaSachPN
{
    public partial class QLNV : Form
    {
        Connect kn = new Connect();
        public QLNV()
        {
            InitializeComponent();
        }
        public void getdata()
        {
            string query = "select * from NHANVIEN";
            DataSet ds = kn.LayDuLieu(query);
            dgvNhanvien.DataSource = ds.Tables[0];
        }
        public void clear()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtManv.Enabled = true;

            txtManv.Text = "";
            txtTennv.Text = "";
            txtDiachi.Text = "";
            txtluong.Text = "";
            txtSDT.Text = "";
            txtTim.Text = "";
            dtpNgSinh.Value = DateTime.Now;
        }
        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtManv.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;

                txtManv.Text = dgvNhanvien.Rows[r].Cells["MANV"].Value.ToString();
                txtTennv.Text = dgvNhanvien.Rows[r].Cells["TENNV"].Value.ToString();
                dtpNgSinh.Text = dgvNhanvien.Rows[r].Cells["NGAYSINH"].Value.ToString();
                txtDiachi.Text = dgvNhanvien.Rows[r].Cells["DIACHI"].Value.ToString();
                txtSDT.Text = dgvNhanvien.Rows[r].Cells["SDT"].Value.ToString();
                txtluong.Text = dgvNhanvien.Rows[r].Cells["LUONG"].Value.ToString();
                string gioiTinhValue = dgvNhanvien.Rows[r].Cells["GIOITINH"].Value.ToString();
                if (gioiTinhValue == "Nam")
                {
                    // Đặt RadioButton "Nam" (assumed radio button name)
                    rdbtnNam.Checked = true;
                    rdbtnNu.Checked = false;
                }
                else if (gioiTinhValue == "Nữ")
                {
                    // Đặt RadioButton "Nữ" (assumed radio button name)
                    rdbtnNam.Checked = false;
                    rdbtnNu.Checked = true;
                }

            }
        }

        private void QLNV_Load(object sender, EventArgs e)
        {
            getdata();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string checkQuery = string.Format("SELECT COUNT(*) FROM NhanVien WHERE MaNV = N'{0}'", txtManv.Text);
            int existingRecords = (int)kn.LayDuLieu(checkQuery).Tables[0].Rows[0][0];
            if (string.IsNullOrWhiteSpace(txtManv.Text) ||
                string.IsNullOrWhiteSpace(txtTennv.Text) ||
                (rdbtnNam.Checked == false && rdbtnNu.Checked == false) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtDiachi.Text) ||
                string.IsNullOrWhiteSpace(txtluong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return; // Dừng thực hiện khi chưa nhập đủ thông tin
            }

            string GioiTinh = rdbtnNam.Checked ? "Nam" : (rdbtnNu.Checked ? "Nữ" : "");
            string query = string.Format("insert into NHANVIEN values(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}')",
                txtManv.Text,
                txtTennv.Text,
                dtpNgSinh.Value.ToString("yyyy/MM/dd"),
                GioiTinh,
                txtDiachi.Text,
                txtSDT.Text,
                txtluong.Text
                    );
            if (existingRecords > 0)
            {
                // Mã nhân viên đã tồn tại, hiển thị thông báo lỗi
                MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập mã nhân viên khác.");
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
            string GioiTinh = rdbtnNam.Checked ? "Nam" : (rdbtnNu.Checked ? "Nữ" : "");
            string query = string.Format("update NHANVIEN set TENNV=N'{1}', NGAYSINH=N'{2}', GIOITINH=N'{3}', DIACHI=N'{4}', SDT=N'{5}',LUONG=N'{6}' where MANV=N'{0}'",
                txtManv.Text,
                txtTennv.Text,
                dtpNgSinh.Value.ToString("yyyy/MM/dd"),
                GioiTinh,
                txtDiachi.Text,
                txtSDT.Text,
                txtluong.Text
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
            string query = string.Format("DELETE FROM NHANVIEN WHERE MANV=N'{0}'", txtManv.Text);
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
            string query = string.Format("select * from NHANVIEN where " +
                "MANV like N'%{0}%' or " +
                "TENNV like N'%{0}%' or " +
                "NGAYSINH like N'%{0}%' or " +
                "GIOITINH like N'%{0}%' or " +
                "DIACHI like N'%{0}%' or " +
                "SDT like N'%{0}%' or " +
                "LUONG like N'%{0}%' ",
                txtTim.Text
                );
            try
            {
                DataSet ds = kn.LayDuLieu(query);

                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvNhanvien.DataSource = ds.Tables[0];
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

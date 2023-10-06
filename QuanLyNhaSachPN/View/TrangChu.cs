using QuanLyNhaSachPN.DAO;
using QuanLyNhaSachPN.View;
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
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }
        Connect con = new Connect();
        private void btnQLNV_Click(object sender, EventArgs e)
        {
            QLNV frm = new QLNV();
            frm.Show();
            this.Hide();
        }

        private void btnQLBH_Click(object sender, EventArgs e)
        {
            QLBH frm = new QLBH();
            frm.Show();
            this.Hide();
        }

        private void btnQLK_Click(object sender, EventArgs e)
        {
            QLKho frm = new QLKho();
            frm.Show();
            this.Hide();
        }

        private void btnQLNCC_Click(object sender, EventArgs e)
        {
            QLNCC frm = new QLNCC();
            frm.Show();
            this.Hide();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            BaoCao frm = new BaoCao();
            frm.Show();
            this.Hide();
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLNV frm = new QLNV();
            frm.Show();
            this.Hide();
        }

        private void quảnLýBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLBH frm = new QLBH();
            frm.Show();
            this.Hide();
        }

        private void quảnLýKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLKho frm = new QLKho();
            frm.Show();
            this.Hide();
        }

        private void quảnLýNhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLNCC frm = new QLNCC();
            frm.Show();
            this.Hide();
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaoCao frm = new BaoCao();
            frm.Show();
            this.Hide();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DangNhap frm = new DangNhap();
            frm.Show();
            this.Hide();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string CheckQH()
        {
            //Lấy tên tài khoản từ login
            string tk = DangNhap.Taikhoan;

            //Kiểm tra chức danh của tài khoản
            string sql = string.Format("select CHUCDANH from NGUOIDUNG where TAIKHOAN = '{0}'", tk);
            DataSet ds = con.LayDuLieu(sql);

            //Trả về kết quả của hàm Check quyền hạn
            return ds.Tables[0].Rows[0]["CHUCDANH"].ToString();
        }
        private void TrangChu_Load(object sender, EventArgs e)
        {
            if(CheckQH() == "Nhan Vien")
            {
                btnQLNCC.Enabled = false;
                btnQLNV.Enabled = false;
                btnThongKe.Enabled = false;
            }    
        }
    }
}

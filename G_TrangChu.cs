using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project1
{
    public partial class G_TrangChu : Form
    {
        public G_TrangChu(string role)
        {
            InitializeComponent();

            // Vai trò
            if (role != "Admin")
            {
                btnNhanVien.Enabled = false;
                btnThongKe.Enabled = false;
            }

            // Thiết lập form để có thể thay đổi kích thước khi chạy
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private Form formMain;
        private void openForm(Form form)
        {
            if (formMain != null)
            {
                formMain.Close();
            }
            formMain = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnl_Than.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }
        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            openForm(new G_DanhMuc());
            lblHome.Text = btnDanhMuc.Text;
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            openForm(new G_SanPham());
            lblHome.Text = btnSanPham.Text;
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            openForm(new G_KhangHang());
            lblHome.Text = btnKhachHang.Text;
        }

        private void btnNhaNCC_Click(object sender, EventArgs e)
        {
            openForm(new G_NhaCungCap());
            lblHome.Text = btnNhaNCC.Text;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            openForm(new G_NhanVien());
            lblHome.Text = btnNhanVien.Text;
        }

        private void pic_logo_Click(object sender, EventArgs e)
        {
            if (formMain != null)
            {
                formMain.Close();
            }
            lblHome.Text = "Home";
        }

        private void btnHoaDonNhap_Click(object sender, EventArgs e)
        {
            openForm(new G_HoaDonNhap());
            lblHome.Text= btnHoaDonNhap.Text;
        }

        private void btnHoaDonBan_Click(object sender, EventArgs e)
        {
            openForm (new G_HoaDonBan());
            lblHome.Text=btnHoaDonBan.Text;
        }   
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            openForm(new G_ThongKe());
            lblHome.Text = btnThongKe.Text;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult x = MessageBox.Show("Bạn chắc chắn muốn thoát?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (x == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Mời bạn sử dụng tiếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

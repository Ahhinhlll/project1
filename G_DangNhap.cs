using BUS;
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
    public partial class G_DangNhap : Form
    {
        B_DangNhap bus_dn = new B_DangNhap();
        public G_DangNhap()
        {
            InitializeComponent();
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTK.Text;
            string password = txtMK.Text;
            if (bus_dn.Login(username, password))
            {
                string role = bus_dn.SetQuyen(username);

                this.Hide();
                G_TrangChu Form = new G_TrangChu(role);
                Form.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đang nhập hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTK.Text = "";
                txtMK.Text = "";
            }
        }

        private void ckbHienThi_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbHienThi.Checked)
                txtMK.PasswordChar = '\0';
            else txtMK.PasswordChar = '*';
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            G_DoiPass pass = new G_DoiPass(txtTK.Text);
            pass.Show();
        }
    }
}
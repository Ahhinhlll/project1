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
    public partial class G_DoiPass : Form
    {
        private B_DangNhap doiMK = new B_DangNhap();

        public G_DoiPass(string tenTaiKhoan)
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string tenTK = txtTK.Text;
            string mkMoi = txtMK.Text;
            string nhapLaiMK = txtMK_Lai.Text;    
            if (lblCatch.Text == txtCatch.Text)
            {
                if (mkMoi == nhapLaiMK)
                {
                    doiMK.DoiPass(tenTK, mkMoi);
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMK.Text = "";
                    txtTK.Text = "";
                    txtMK_Lai.Text = "";
                    lblCatch.Text = "";
                    txtCatch.Text = "";
                }
                else
                {
                    MessageBox.Show("Mật khẩu mới và nhập lại mật khẩu không trùng khớp!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Thông tin không đúng, vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.OnLoad(e);
            }
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            this.Hide();
            G_DangNhap dangNhap = new G_DangNhap();
            dangNhap.Show();
        }

        private void G_DoiPass_Load(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        private void GenerateCaptcha()
        {
            Random rand = new Random();
            int num = rand.Next(6, 8);
            string captcha = "";
            int totl = 0;
            do
            {
                int chr = rand.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    captcha = captcha + (char)chr;
                    totl++;
                    if (totl == num)
                        break;
                }
            } while (true);
            lblCatch.Text = captcha;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtMK.PasswordChar = '\0';
                txtMK_Lai.PasswordChar = '\0';
            }
            else
            {
                txtMK.PasswordChar = '*';
                txtMK_Lai.PasswordChar = '*';
            }
        }
    }
}

using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace project1
{
    public partial class G_NhanVien : Form
    {
        NhanVien nv = new NhanVien();
        B_NhanVien bus_nv = new B_NhanVien();   
        public G_NhanVien()
        {
            InitializeComponent();
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
        }

        private void G_NhanVien_Load(object sender, EventArgs e)
        {
            dgvNhanVien.DataSource = bus_nv.GetData();
            dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns[2].HeaderText = "Giới tính";
            dgvNhanVien.Columns[3].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[4].HeaderText = "Số điện thoại";
            dgvNhanVien.Columns[5].HeaderText = "Email";
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow chon = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = chon.Cells[0].Value.ToString();
                txtTenNV.Text = chon.Cells[1].Value.ToString();
                cbbSex.Text = chon.Cells[2].Value.ToString();
                txtDiaChi.Text = chon.Cells[3].Value.ToString();
                txtSDT.Text = chon.Cells[4].Value.ToString();
                txtEmail.Text = chon.Cells[5].Value.ToString();
                txtMaNV.Enabled = false;
            }
        }
        private bool kiemtraSDT(string kt)
        {
            if (kt.Length != 10 || kt[0] != '0')
                return false;
            for (int i = 0; i < kt.Length; i++)
            {
                if (!char.IsDigit(kt[i]))
                    return false;
            }
            return true;          
        }
        private bool KiemTraEmail(string email)
        {
            string pattern = @"^\w+([\.-]?\w+)*@gmail.com$";
            return Regex.IsMatch(email, pattern);
        }
        private void lammoi()
        {
            txtMaNV.Enabled = true;
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            cbbSex.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            dgvNhanVien.DataSource = bus_nv.GetData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lammoi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            nv.MaNV = txtMaNV.Text;
            nv.TenNV = txtTenNV.Text;
            nv.GioiTinh = cbbSex.SelectedItem?.ToString();
            nv.DiaChi = txtDiaChi.Text;
            nv.SDT = txtSDT.Text;
            nv.Email = txtEmail.Text;

            if (cbbSex.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn giới tính có sẵn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!nv.MaNV.StartsWith("NV"))
            {
                MessageBox.Show("Mã nhân viên phải có 'NV' đứng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(nv.MaNV) || string.IsNullOrEmpty(nv.TenNV) || string.IsNullOrEmpty(nv.DiaChi) || string.IsNullOrEmpty(nv.SDT) || string.IsNullOrEmpty(nv.Email))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (bus_nv.kiemtramatrung(nv.MaNV) > 0)
            {
                MessageBox.Show("Mã đã tồn tại, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!kiemtraSDT(nv.SDT))
            {
                MessageBox.Show("Số điện thoại bắt đầu = 0 và độ dài = 10, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!KiemTraEmail(nv.Email))
            {
                MessageBox.Show("Email phải có định dạng ...@gmail.com, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (bus_nv.ThemNV(nv))
                {
                    MessageBox.Show("Bản ghi đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lammoi();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            nv.MaNV = txtMaNV.Text;
            nv.TenNV = txtTenNV.Text;
            nv.GioiTinh = cbbSex.SelectedItem?.ToString();
            nv.DiaChi = txtDiaChi.Text;
            nv.SDT = txtSDT.Text;
            nv.Email = txtEmail.Text;

            if (cbbSex.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn giới tính có sẵn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(nv.MaNV) || string.IsNullOrEmpty(nv.TenNV) || string.IsNullOrEmpty(nv.DiaChi) || string.IsNullOrEmpty(nv.SDT) || string.IsNullOrEmpty(nv.Email))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!kiemtraSDT(nv.SDT))
            {
                MessageBox.Show("Số điện thoại bắt đầu = 0 và độ dài = 10, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!KiemTraEmail(nv.Email))
            {
                MessageBox.Show("Email phải có định dạng ...@gmail.com, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (bus_nv.SuaNV(nv))
                {
                    MessageBox.Show("Bản ghi đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lammoi();
                }
            }
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            nv.MaNV = txtMaNV.Text;
            if (string.IsNullOrEmpty(nv.MaNV))
            {
                MessageBox.Show("Vui lòng chọn mục cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult x = MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x == DialogResult.Yes)
                {
                    if (bus_nv.XoaNV(nv))
                    {
                        MessageBox.Show("Bản ghi đã được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lammoi();
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mục cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            nv.MaNV = txtTimKiem.Text;
            nv.TenNV = txtTimKiem.Text;
            dgvNhanVien.DataSource = bus_nv.TimKiem(nv);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
           Close();
        }
    }
}

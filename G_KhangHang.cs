using BUS;
using DTO;
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
    public partial class G_KhangHang : Form
    {
        B_KhachHang bus_kh = new B_KhachHang();
        KhachHang kh = new KhachHang();
        public G_KhangHang()
        {
            InitializeComponent();
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;
        }


        private void G_KhangHang_Load(object sender, EventArgs e)
        {
            dgvKhachHang.DataSource = bus_kh.GetData();
            dgvKhachHang.Columns[0].HeaderText = "Mã khách hàng";
            dgvKhachHang.Columns[1].HeaderText = "Tên khách hàng";
            dgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns[3].HeaderText = "Sô điện thoại";
        }
      
        private void lammoi()
        {
            txtMaKH.Enabled = true;
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtTimKiem.Text = "";
            dgvKhachHang.DataSource= bus_kh.GetData();
        }
        private bool kiemtraSDT(string kt)
        {
            if (kt.Length !=10 || kt[0] != '0')
                return false;
            for (int i = 1; i < kt.Length; i++)
            {
                if (!char.IsDigit(kt[i]))
                    return false;
            }
            return true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lammoi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            kh.MaKH = txtMaKH.Text;
            kh.TenKH = txtTenKH.Text;
            kh.DiaChi = txtDiaChi.Text;
            kh.SDT = txtSDT.Text;
            if (bus_kh.kiemtramatrung(kh.MaKH) > 0)
            {
                MessageBox.Show("Mã đã tồn tại, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!kh.MaKH.StartsWith("KH"))
            {
                MessageBox.Show("Mã khách hàng phải có 'KH' đứng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(kh.MaKH) || string.IsNullOrEmpty(kh.TenKH) || string.IsNullOrEmpty(kh.DiaChi) || string.IsNullOrEmpty(kh.SDT))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!kiemtraSDT(kh.SDT))
            {
                MessageBox.Show("Số điện thoại không hợp lệ, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (bus_kh.ThemKH(kh))
                {
                    MessageBox.Show("Bản ghi đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lammoi();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            kh.MaKH = txtMaKH.Text;
            kh.TenKH = txtTenKH.Text;
            kh.DiaChi = txtDiaChi.Text;
            kh.SDT = txtSDT.Text;
            if (string.IsNullOrEmpty(kh.MaKH) || string.IsNullOrEmpty(kh.TenKH) || string.IsNullOrEmpty(kh.DiaChi) || string.IsNullOrEmpty(kh.SDT))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!kiemtraSDT(kh.SDT))
            {
                MessageBox.Show("Số điện thoại không hợp lệ, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (bus_kh.SuaKH(kh))
                {
                    MessageBox.Show("Bản ghi đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lammoi();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            kh.MaKH=txtMaKH.Text;
            if (string.IsNullOrEmpty(kh.MaKH))
            {
                MessageBox.Show("Vui lòng chọn mục cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult x = MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x== DialogResult.Yes)
                {
                    if (bus_kh.XoaKH(kh))
                    {
                        MessageBox.Show("Bản ghi đã được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lammoi();
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            kh.MaKH = txtTimKiem.Text;
            kh.TenKH = txtTimKiem.Text;
            dgvKhachHang.DataSource = bus_kh.TimKiem(kh);
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow chon = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = chon.Cells[0].Value.ToString();
                txtTenKH.Text = chon.Cells[1].Value.ToString();
                txtDiaChi.Text = chon.Cells[2].Value.ToString();
                txtSDT.Text = chon.Cells[3].Value.ToString();
                txtMaKH.Enabled = false;
            }
        }

      
    }
}

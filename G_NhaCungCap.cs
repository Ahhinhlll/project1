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
    public partial class G_NhaCungCap : Form
    {
        B_NhaCungCap bus_ncc = new B_NhaCungCap();
        NhaCungCap ncc = new NhaCungCap();
        public G_NhaCungCap()
        {
            InitializeComponent();
            dgvNhaNCC.CellClick += dgvNhaNCC_CellClick;
        }

        private void G_NhaCungCap_Load(object sender, EventArgs e)
        {
            dgvNhaNCC.DataSource = bus_ncc.GetData();
            dgvNhaNCC.Columns[0].HeaderText = "Mã nhà cung cấp";
            dgvNhaNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvNhaNCC.Columns[2].HeaderText = "Địa chỉ";
            dgvNhaNCC.Columns[3].HeaderText = "Sô điện thoại";
        }

        private void lammoi()
        {
            txtMaNCC.Enabled = true;
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtTimKiem.Text = "";
            dgvNhaNCC.DataSource = bus_ncc.GetData();
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
            ncc.MaNCC = txtMaNCC.Text;
            ncc.TenNCC = txtTenNCC.Text;
            ncc.DiaChi = txtDiaChi.Text;
            ncc.SDT = txtSDT.Text;
            if (bus_ncc.kiemtramatrung(ncc.MaNCC) > 0)
            {
                MessageBox.Show("Mã đã tồn tại, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!ncc.MaNCC.StartsWith("NCC"))
            {
                MessageBox.Show("Mã nhà cung cấp phải có 'NCC' đứng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(ncc.MaNCC) || string.IsNullOrEmpty(ncc.TenNCC) || string.IsNullOrEmpty(ncc.DiaChi) || string.IsNullOrEmpty(ncc.SDT))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!kiemtraSDT(ncc.SDT))
            {
                MessageBox.Show("Số điện thoại không hợp lệ, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (bus_ncc.ThemNCC(ncc))
                {
                    MessageBox.Show("Bản ghi đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lammoi();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ncc.MaNCC = txtMaNCC.Text;
            ncc.TenNCC = txtTenNCC.Text;
            ncc.DiaChi = txtDiaChi.Text;
            ncc.SDT = txtSDT.Text;            
            if (string.IsNullOrEmpty(ncc.MaNCC) || string.IsNullOrEmpty(ncc.TenNCC) || string.IsNullOrEmpty(ncc.DiaChi) || string.IsNullOrEmpty(ncc.SDT))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!kiemtraSDT(ncc.SDT))
            {
                MessageBox.Show("Số điện thoại không hợp lệ, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (bus_ncc.SuaNCC(ncc))
                {
                    MessageBox.Show("Bản ghi đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lammoi();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ncc.MaNCC = txtMaNCC.Text;
            if (string.IsNullOrEmpty(ncc.MaNCC))
            {
                MessageBox.Show("Vui lòng chọn mục cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult x = MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x == DialogResult.Yes)
                {
                    if (bus_ncc.XoaNCC(ncc))
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
            ncc.MaNCC = txtTimKiem.Text;
            ncc.TenNCC = txtTimKiem.Text;
            dgvNhaNCC.DataSource = bus_ncc.TimKiem(ncc);
        }

        private void dgvNhaNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow chon = dgvNhaNCC.Rows[e.RowIndex];
                txtMaNCC.Text = chon.Cells[0].Value.ToString();
                txtTenNCC.Text = chon.Cells[1].Value.ToString();
                txtDiaChi.Text = chon.Cells[2].Value.ToString();
                txtSDT.Text = chon.Cells[3].Value.ToString();
                txtMaNCC.Enabled = false;
            }
        }
    }
}

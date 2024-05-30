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
    public partial class G_SanPham : Form
    {
        B_DanhMuc bus_dm = new B_DanhMuc();
        B_SanPham bus_sp = new B_SanPham(); 
        SanPham sp = new SanPham(); 
        public G_SanPham()
        {
            InitializeComponent();
            dgvSanPham.CellClick += dgvSanPham_CellClick;
        }

        private void G_SanPham_Load(object sender, EventArgs e)
        {
            dgvSanPham.DataSource = bus_sp.GetData();
            dgvSanPham.Columns[0].HeaderText = "Mã sản phẩm";
            dgvSanPham.Columns[1].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[2].HeaderText = "Mã danh mục";
            dgvSanPham.Columns[3].HeaderText = "Size";
            dgvSanPham.Columns[4].HeaderText = "Số lượng";
            dgvSanPham.Columns[5].HeaderText = "Đơn giá";
            // load danh mục sản phẩm
            cbbMaDM.DataSource = bus_dm.getData();           
            cbbMaDM.ValueMember = "maDM";
            cbbMaDM.DisplayMember = "tenDM";
        }

        private void lammoi()
        {
            txtMaSP.Enabled = true;
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            cbbMaDM.Text = "";
            cbbSize.Text = "";
            nudSoLuong.Value=0;
            txtDonGia.Text = "";
            dgvSanPham.DataSource = bus_sp.GetData();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            sp.maSP = txtMaSP.Text;
            sp.tenSP = txtTenSP.Text;
            sp.maDM = cbbMaDM.SelectedValue != null ? cbbMaDM.SelectedValue.ToString() : null;
            sp.size = cbbSize.SelectedItem != null ? cbbSize.SelectedItem.ToString() : null;
            sp.soLuong = (int)nudSoLuong.Value;
            sp.donGia = float.TryParse(txtDonGia.Text, out float donGia) ? donGia : 0;

           
            if (cbbMaDM.SelectedValue == null)
            {
                MessageBox.Show("Mã danh mục không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!sp.maSP.StartsWith("SP"))
            {
                MessageBox.Show("Mã sản phẩm phải theo định dạng 'SP'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbbSize.SelectedIndex == -1)
            {
                MessageBox.Show("Size không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(sp.maDM) || string.IsNullOrEmpty(sp.maSP) || string.IsNullOrEmpty(sp.tenSP) || string.IsNullOrEmpty(sp.size) )
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (sp.soLuong <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (sp.donGia <= 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (bus_sp.kiemtramatrung(sp.maSP) > 0)
            {
                MessageBox.Show("Mã đã tồn tại, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (bus_sp.ThemSP(sp))
            {
                MessageBox.Show("Bản ghi đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lammoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            sp.maSP = txtMaSP.Text;
            sp.tenSP = txtTenSP.Text;
            sp.maDM = cbbMaDM.SelectedValue != null ? cbbMaDM.SelectedValue.ToString() : null;
            sp.size = cbbSize.SelectedItem != null ? cbbSize.SelectedItem.ToString() : null;
            sp.soLuong = (int)nudSoLuong.Value;
            sp.donGia = float.TryParse(txtDonGia.Text, out float donGia) ? donGia : 0;


            if (cbbMaDM.SelectedValue == null)
            {
                MessageBox.Show("Mã danh mục không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbbSize.SelectedIndex == -1)
            {
                MessageBox.Show("Size không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(sp.maDM) || string.IsNullOrEmpty(sp.maSP) || string.IsNullOrEmpty(sp.tenSP) || string.IsNullOrEmpty(sp.size))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (sp.soLuong <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (sp.donGia <= 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (bus_sp.SuaSP(sp))
            {
                MessageBox.Show("Bản ghi đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lammoi();
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            sp.maSP = txtMaSP.Text;

            if (string.IsNullOrEmpty(sp.maSP))
            {
                MessageBox.Show("Vui lòng chọn mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult x = MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x == DialogResult.Yes)
                {
                    if (bus_sp.kiemtramatrung(sp.maSP) > 0)
                    {
                        if (bus_sp.XoaSP(sp))
                        {
                            MessageBox.Show("Bản ghi đã được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lammoi();
                        }
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
            sp.maSP = txtTimKiem.Text;
            sp.tenSP = txtTimKiem.Text;
            dgvSanPham.DataSource = bus_sp.TimKiem(sp);         
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lammoi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow chon= dgvSanPham.Rows[e.RowIndex];
                txtMaSP.Text = chon.Cells[0].Value.ToString();
                txtTenSP.Text = chon.Cells[1].Value.ToString();
                cbbMaDM.Text = chon.Cells[2].Value.ToString();
                cbbSize.Text = chon.Cells[3].Value.ToString();
                nudSoLuong.Text = chon.Cells[4].Value.ToString();
                txtDonGia.Text = chon.Cells[5].Value.ToString();
                txtMaSP.Enabled=false;
            }
        }


    }
}

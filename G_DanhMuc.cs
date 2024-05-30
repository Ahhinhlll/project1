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
    public partial class G_DanhMuc : Form
    {
         B_DanhMuc bus_dm = new B_DanhMuc();
         DanhMuc dm= new DanhMuc();
        public G_DanhMuc()
        {
            InitializeComponent();
            dgvDanhMuc.CellClick += dgvDanhMuc_CellClick;
        }
     

        private void dgvDanhMuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvDanhMuc.Rows[e.RowIndex];

                txtMaDM.Text = selectedRow.Cells[0].Value.ToString();
                txtTenDM.Text = selectedRow.Cells[1].Value.ToString();
            }
            
        }

        private void G_DanhMuc_Load(object sender, EventArgs e)
        {
            dgvDanhMuc.DataSource=bus_dm.getData();
            dgvDanhMuc.Columns[0].HeaderText = "Mã danh mục";
            dgvDanhMuc.Columns[1].HeaderText = "Tên danh mục";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            dm.maDM=txtMaDM.Text;
            dm.tenDM=txtTenDM.Text;
            if (bus_dm.kiemtramatrung(txtMaDM.Text) > 0)
            {
                MessageBox.Show("Mã đã tồn tại, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!dm.maDM.StartsWith("DM"))
            {
                MessageBox.Show("Mã danh mục theo định dạng 'DM'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(dm.maDM) || string.IsNullOrEmpty(dm.tenDM))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (bus_dm.ThemDM(dm))
                {
                    MessageBox.Show("Bản ghi đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lammoi();
                }
            }
        }
        private void lammoi()
        {
            txtMaDM.Enabled = true;
            txtMaDM.Text = "";
            txtTenDM.Text = "";
            txtTimKiem.Text = "";
            dgvDanhMuc.DataSource = bus_dm.getData();
        } 
        private void btnSua_Click(object sender, EventArgs e)
        {
            dm.maDM = txtMaDM.Text;
            dm.tenDM = txtTenDM.Text;         
            if (string.IsNullOrEmpty(dm.maDM) || string.IsNullOrEmpty(dm.tenDM))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (bus_dm.SuaDM(dm))
                {
                    MessageBox.Show("Bản ghi đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lammoi() ;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            dm.maDM = txtMaDM.Text; 

            if (string.IsNullOrEmpty(dm.maDM))
            {
                MessageBox.Show("Vui lòng chọn mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult x = MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x == DialogResult.Yes)
                {
                    if (bus_dm.kiemtramatrung(dm.maDM) > 0)
                    {
                        if (bus_dm.XoaDM(dm))
                        {
                            MessageBox.Show("Bản ghi đã được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lammoi();
                        }
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {   if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mục cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dm.maDM = txtTimKiem.Text;
            dm.tenDM = txtTimKiem.Text;
            dgvDanhMuc.DataSource = bus_dm.TimKiem(dm);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lammoi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvDanhMuc.Rows[e.RowIndex];
                txtMaDM.Text = selectedRow.Cells[0].Value.ToString();
                txtTenDM.Text = selectedRow.Cells[1].Value.ToString();
                txtMaDM.Enabled = false;
            }
        }
    }
}

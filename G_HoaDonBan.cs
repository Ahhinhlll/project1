using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project1
{
    public partial class G_HoaDonBan : Form
    {
        B_HoaDonBan bus_hdb = new B_HoaDonBan();
        B_ChiTietHDB bus_cthd = new B_ChiTietHDB();
        B_KhachHang bus_kh = new B_KhachHang();
        B_NhanVien bus_nv = new B_NhanVien();
        B_SanPham bus_sp = new B_SanPham();
        HoaDonBan hdb = new HoaDonBan();
        ChiTietHDB ctHDB = new ChiTietHDB();

        private int SoluongCu;
        public G_HoaDonBan()
        {
            InitializeComponent();
            dgvHoaDonBan.CellClick += dgvHoaDonBan_CellClick;
            dgvChiTietHDB.CellClick += dgvChiTietHDB_CellClick;
            cbbMaSP.SelectedIndexChanged += cbbMaSP_SelectedIndexChanged;
            G_HoaDonBan_Load();
        }

        void G_HoaDonBan_Load()
        {
            dgvHoaDonBan.DataSource = bus_hdb.GetData();
            dgvHoaDonBan.Columns[0].HeaderText = "Mã hóa đơn";
            dgvHoaDonBan.Columns[1].HeaderText = "Ngày bán";
            dgvHoaDonBan.Columns[2].HeaderText = "Mã khách hàng";
            dgvHoaDonBan.Columns[3].HeaderText = "Mã nhân viên";
            dgvHoaDonBan.Columns[4].HeaderText = "Tổng tiền";

            cbbMaKH.DataSource = bus_kh.GetData();
            cbbMaKH.ValueMember = "maKH";
            cbbMaKH.DisplayMember = "tenKH";

            cbbMaNV.DataSource = bus_nv.GetData();
            cbbMaNV.ValueMember = "maNV";
            cbbMaNV.DisplayMember = "tenNV";

            cbbMaSP.DataSource = bus_sp.GetData();
            cbbMaSP.ValueMember = "maSP";
            cbbMaNV.DisplayMember = "tenSP";
        }
        private void dgvHoaDonBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvHoaDonBan.Rows[e.RowIndex];
                txtMaHD.Text = selectedRow.Cells["maHDB"].Value.ToString();
                dtbNgay.Text = selectedRow.Cells["ngayban"].Value.ToString();
                cbbMaKH.SelectedValue = selectedRow.Cells["maKH"].Value;
                cbbMaNV.SelectedValue = selectedRow.Cells["maNV"].Value;
                lblTongTien.Text = string.Format("{0:#,##0} VND", selectedRow.Cells["tongtien"].Value);
                txtMaHD.Enabled = false;

                string maHDB = selectedRow.Cells[0].Value.ToString();
                DataTable dtChiTietHDB = bus_cthd.GetDataByMaHDB(maHDB);
                dgvChiTietHDB.DataSource = dtChiTietHDB;

                dgvChiTietHDB.Columns[0].HeaderText = "Mã hóa đơn";
                dgvChiTietHDB.Columns[1].HeaderText = "Mã sản phẩm";
                dgvChiTietHDB.Columns[2].HeaderText = "Số lượng bán";
                dgvChiTietHDB.Columns[3].HeaderText = "Đơn giá bán";
                dgvChiTietHDB.Columns[4].HeaderText = "Chiết khấu";
                dgvChiTietHDB.Columns[5].HeaderText = "Thành tiền";
            }
        }

        private void dgvChiTietHDB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvChiTietHDB.Rows[e.RowIndex];
                txtMaHD.Text = selectedRow.Cells["maHDB"].Value.ToString();
                cbbMaSP.SelectedValue = selectedRow.Cells["maSP"].Value.ToString();
                nudSoLuong.Text = selectedRow.Cells["soluongban"].Value.ToString();
                lblDonGia.Text = selectedRow.Cells["dongiaban"].Value.ToString();
                txtChietKhau.Text = selectedRow.Cells["chietkhau"].Value.ToString();
                lblThanhTien.Text = string.Format("{0:#,##0} VND", selectedRow.Cells["thanhtien"].Value);
                txtMaHD.Enabled = false;

                TinhTongTienHoaDon();

                if (int.TryParse(selectedRow.Cells["soluongban"].Value.ToString(), out int soluongnhap)) ;
                {
                    SoluongCu=soluongnhap;
                }


            }
        }
        private void TinhTongTienHoaDon()
        {
            float tongTien = 0;
            if (dgvChiTietHDB != null && dgvChiTietHDB.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvChiTietHDB.Rows)
                {
                    if (row.Cells[5] != null && row.Cells[5].Value != null)
                    {
                        float thanhTien;
                        if (float.TryParse(row.Cells[5].Value.ToString(), out thanhTien))
                        {
                            tongTien += thanhTien;
                        }
                    }
                }
            }
            lblTongTien.Text = string.Format("{0:#,##0} VND", tongTien);
        }
        private void lammoi()
        {
            txtMaHD.Enabled = true;
            txtMaHD.Text = "";
            dtbNgay.Text = "";
            cbbMaKH.Text = "";
            cbbMaNV.Text = "";
            lblTongTien.Text = "";
            cbbMaSP.Text = "";
            nudSoLuong.Value = 0;
            lblDonGia.Text = "";
            txtChietKhau.Text = "";
            lblThanhTien.Text = "";
            dgvHoaDonBan.DataSource = bus_hdb.GetData();
            dgvChiTietHDB.DataSource = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DateTime maxDate = DateTime.Now;
            DateTime minDate = new DateTime(1980, 1, 1);

            hdb.MaHDB = txtMaHD.Text;
            hdb.NgayBan = string.Format("{0}/{1}/{2}", dtbNgay.Value.Year, dtbNgay.Value.Month, dtbNgay.Value.Day);
            hdb.MaKH = cbbMaKH.SelectedValue != null ? cbbMaKH.SelectedValue.ToString() : null;
            hdb.MaNV = cbbMaNV.SelectedValue != null ? cbbMaNV.SelectedValue.ToString() : null;
            hdb.TongTien = 0;

            if (!hdb.MaHDB.StartsWith("HDB"))
            {
                MessageBox.Show("Mã hóa đơn bán phải có 'HDB' đứng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtbNgay.Value < minDate)
            {
                MessageBox.Show("Ngày nhập phải lớn hơn ngày 01/01/1980", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtbNgay.Value > maxDate)
            {
                MessageBox.Show("Ngày nhập không được lớn hơn ngày hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbbMaKH.SelectedValue == null)
            {
                MessageBox.Show("Mã khách hàng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbbMaNV.SelectedValue == null)
            {
                MessageBox.Show("Mã nhân viên không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(hdb.MaHDB) || string.IsNullOrEmpty(hdb.MaKH) || string.IsNullOrEmpty(hdb.MaNV))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bus_hdb.KiemTraMaTrung(hdb.MaHDB) > 0)
            {
                MessageBox.Show("Mã đã tồn tại, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bus_hdb.ThemHoaDonBan(hdb))
            {
                MessageBox.Show("Bản ghi đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lammoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DateTime maxDate = DateTime.Now;
            DateTime minDate = new DateTime(1980, 1, 1);

            hdb.MaHDB = txtMaHD.Text;
            hdb.NgayBan = string.Format("{0}/{1}/{2}", dtbNgay.Value.Year, dtbNgay.Value.Month, dtbNgay.Value.Day);
            hdb.MaKH = cbbMaKH.SelectedValue != null ? cbbMaKH.SelectedValue.ToString() : null;
            hdb.MaNV = cbbMaNV.SelectedValue != null ? cbbMaNV.SelectedValue.ToString() : null;
            hdb.TongTien = 0;

            if (dtbNgay.Value < minDate)
            {
                MessageBox.Show("Ngày nhập phải lớn hơn ngày 01/01/1980", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtbNgay.Value > maxDate)
            {
                MessageBox.Show("Ngày nhập không được lớn hơn ngày hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbbMaKH.SelectedValue == null)
            {
                MessageBox.Show("Mã khách hàng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbbMaNV.SelectedValue == null)
            {
                MessageBox.Show("Mã nhân viên không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(hdb.MaHDB) || string.IsNullOrEmpty(hdb.MaKH) || string.IsNullOrEmpty(hdb.MaNV))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bus_hdb.SuaHoaDonBan(hdb))
            {
                MessageBox.Show("Bản ghi đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lammoi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            hdb.MaHDB = txtMaHD.Text;
            if (string.IsNullOrEmpty(hdb.MaHDB))
            {
                MessageBox.Show("Vui lòng chọn mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bool KTraChiTietHDB = bus_cthd.KiemTraMaTrung(hdb.MaHDB) > 0;
                if (KTraChiTietHDB)
                {
                    MessageBox.Show("Phải xóa chi tiết hóa đơn trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult x = MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (x == DialogResult.Yes)
                    {
                        if (bus_hdb.KiemTraMaTrung(hdb.MaHDB) > 0)
                        {
                            if (bus_hdb.XoaHoaDonBan(hdb))
                            {
                                MessageBox.Show("Bản ghi đã được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lammoi();
                            }
                        }
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            string maHDB = txtMaHD.Text;
            ctHDB.MaHDB = maHDB;
            ctHDB.MaSP = cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null;
            ctHDB.SoLuong = (int)nudSoLuong.Value;
            ctHDB.DonGia = bus_sp.LayDonGia(ctHDB.MaSP);
            int soluongSP = bus_sp.Laysoluong(cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null);
            if (!float.TryParse(txtChietKhau.Text, out float chietKhau))
            {
                MessageBox.Show("Chiết khấu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ctHDB.ChietKhau = chietKhau;
            ctHDB.ThanhTien = (ctHDB.SoLuong * ctHDB.DonGia) - (ctHDB.SoLuong * ctHDB.DonGia * ctHDB.ChietKhau / 100);

            if (cbbMaSP.SelectedValue == null)
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(ctHDB.SoLuong > soluongSP)
            {
                MessageBox.Show("Số lượng sản phẩm không đủ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDB.SoLuong <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDB.DonGia <= 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDB.ChietKhau < 0 || ctHDB.ChietKhau > 100)
            {
                MessageBox.Show("Chiết khấu trong khoản 0->100%!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtMaHD.Text) || cbbMaSP.SelectedValue == null || string.IsNullOrEmpty(txtChietKhau.Text))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (bus_cthd.ThemChiTietHDB(ctHDB))
            {
                bus_cthd.TruSl(ctHDB.MaSP, ctHDB.SoLuong);
                MessageBox.Show("Chi tiết hóa đơn đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvChiTietHDB.DataSource = bus_cthd.GetDataByMaHDB(maHDB);
            }
        }

        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            string maHDB = txtMaHD.Text;
            ctHDB.MaHDB = maHDB;
            ctHDB.MaSP = cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null;
            ctHDB.SoLuong = (int)nudSoLuong.Value;
            int soluongSP = bus_sp.Laysoluong(cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null);
            ctHDB.DonGia = bus_sp.LayDonGia(ctHDB.MaSP);
            if (!float.TryParse(txtChietKhau.Text, out float chietKhau))
            {
                MessageBox.Show("Chiết khấu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ctHDB.ChietKhau = chietKhau;
            ctHDB.ThanhTien = (ctHDB.SoLuong * ctHDB.DonGia) - (ctHDB.SoLuong * ctHDB.DonGia * ctHDB.ChietKhau / 100);

            if (cbbMaSP.SelectedValue == null)
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDB.SoLuong > soluongSP)
            {
                MessageBox.Show("Số lượng sản phẩm không đủ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDB.SoLuong <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDB.DonGia <= 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDB.ChietKhau < 0 || ctHDB.ChietKhau > 100)
            {
                MessageBox.Show("Chiết khấu trong khoản 0->100%!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtMaHD.Text) || cbbMaSP.SelectedValue == null || string.IsNullOrEmpty(lblDonGia.Text) || string.IsNullOrEmpty(txtChietKhau.Text))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (bus_cthd.SuaChiTietHDB(ctHDB))
            {
                int soluongmoi = ctHDB.SoLuong - SoluongCu;
                bus_cthd.TruSl(ctHDB.MaSP, soluongmoi);
                MessageBox.Show("Chi tiết hóa đơn đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvChiTietHDB.DataSource = bus_cthd.GetDataByMaHDB(maHDB);
            }
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            string maHDB = txtMaHD.Text;
            string maSP = cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null;

            if (string.IsNullOrEmpty(maHDB) || string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Vui lòng chọn mã hóa đơn và mã sản phẩm để xóa chi tiết hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (bus_cthd.XoaChiTietHDB(maHDB, maSP))
                {
                    int soluong = int.Parse(nudSoLuong.Text);
                    bus_cthd.TruSl(maSP, -soluong);
                    MessageBox.Show("Đã xóa chi tiết hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   dgvChiTietHDB.DataSource = bus_cthd.GetDataByMaHDB(maHDB);
                }
                else
                {
                    MessageBox.Show("Không thể xóa chi tiết hóa đơn này. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mục cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            hdb.MaHDB = txtMaHD.Text;
            dgvHoaDonBan.DataSource = bus_hdb.TimKiem(hdb);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lammoi();
        }

        private void cbbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMaSP.SelectedValue != null)
            {
                string maSP = cbbMaSP.SelectedValue.ToString();
                lblDonGia.Text = bus_sp.LayDonGia(maSP).ToString();
            }

        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaHD.Text))
            {
                XuatHD xuatHDForm = new XuatHD(txtMaHD.Text);
                xuatHDForm.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

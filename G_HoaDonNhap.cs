using BUS;
using DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace project1
{
    public partial class G_HoaDonNhap : Form
    {
        B_HoaDonNhap bus_hdn = new B_HoaDonNhap();
        B_ChiTietHDN bus_cthd = new B_ChiTietHDN();
        B_NhaCungCap bus_ncc = new B_NhaCungCap();
        B_NhanVien bus_nv = new B_NhanVien();
        B_SanPham bus_sp = new B_SanPham();
        HoaDonNhap hdn = new HoaDonNhap();
        ChiTietHDN ctHDN = new ChiTietHDN();

        private int SoluongCu;
        public G_HoaDonNhap()
        {
            InitializeComponent();
            DgvHoaDonNhap.CellClick += DgvHoaDonNhap_CellClick;
            dgvChiTietHDN.CellClick += dgvChiTietHDN_CellClick;
            G_HoaDonNhap_Load();
        }

         void G_HoaDonNhap_Load()
         {
            DgvHoaDonNhap.DataSource = bus_hdn.GetData();
            DgvHoaDonNhap.Columns[0].HeaderText = "Mã hóa đơn";
            DgvHoaDonNhap.Columns[1].HeaderText = "Ngày nhập";
            DgvHoaDonNhap.Columns[2].HeaderText = "Mã nhà cung cấp";
            DgvHoaDonNhap.Columns[3].HeaderText = "Mã nhân viên";
            DgvHoaDonNhap.Columns[4].HeaderText = "Tổng tiền";

            cbbMaNCC.DataSource = bus_ncc.GetData();
            cbbMaNCC.ValueMember = "maNCC";
            cbbMaNCC.DisplayMember = "tenNCC";

            cbbMaNV.DataSource = bus_nv.GetData();
            cbbMaNV.ValueMember = "maNV";
            cbbMaNV.DisplayMember = "tenNV";

            cbbMaSP.DataSource = bus_sp.GetData();
            cbbMaSP.ValueMember = "maSP";
            cbbMaSP.DisplayMember = "tenSP";

         }      
        private void DgvHoaDonNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = DgvHoaDonNhap.Rows[e.RowIndex];
                txtMaHD.Text = selectedRow.Cells["maHDN"].Value.ToString();
                dtbNgay.Text = selectedRow.Cells["ngaynhap"].Value.ToString();
                cbbMaNCC.SelectedValue = selectedRow.Cells["maNCC"].Value;
                cbbMaNV.SelectedValue = selectedRow.Cells["maNV"].Value;
                lblTongTien.Text =  string.Format("{0:#,##0} VND", selectedRow.Cells["tongtien"].Value);
                txtMaHD.Enabled = false;

                string maHDN = selectedRow.Cells[0].Value.ToString();
                DataTable dtChiTietHDN = bus_cthd.GetDataByMaHDN(maHDN);
                dgvChiTietHDN.DataSource = dtChiTietHDN;

                dgvChiTietHDN.Columns[0].HeaderText = "Mã hóa đơn";
                dgvChiTietHDN.Columns[1].HeaderText = "Mã sản phẩm";
                dgvChiTietHDN.Columns[2].HeaderText = "Số lượng nhập";
                dgvChiTietHDN.Columns[3].HeaderText = "Đơn giá nhập";
                dgvChiTietHDN.Columns[4].HeaderText = "Chiết khấu";
                dgvChiTietHDN.Columns[5].HeaderText = "Thành tiền";
            }
        }

        private void dgvChiTietHDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 )
            {
                DataGridViewRow selectedRow = dgvChiTietHDN.Rows[e.RowIndex];
                txtMaHD.Text = selectedRow.Cells["maHDN"].Value.ToString();
                cbbMaSP.SelectedValue = selectedRow.Cells["maSP"].Value.ToString();
                nudSoLuong.Text = selectedRow.Cells["soluongnhap"].Value.ToString();
                txtDonGia.Text = selectedRow.Cells["dongianhap"].Value.ToString();
                txtChietKhau.Text = selectedRow.Cells["chietkhau"].Value.ToString();
                lblThanhTien.Text = string.Format("{0:#,##0} VND", selectedRow.Cells["thanhtien"].Value);

                TinhTongTienHoaDon();

                if (int.TryParse(selectedRow.Cells["soluongnhap"].Value.ToString(),out int soluongnhap))
                {
                    SoluongCu=soluongnhap;
                }
            }
        }

        private void lammoi()
        {
            txtMaHD.Enabled = true;
            txtMaHD.Text = "";
            dtbNgay.Text = "";
            cbbMaNCC.Text = "";
            cbbMaNV.Text = "";
            lblTongTien.Text = "";
            cbbMaSP.Text = "";
            nudSoLuong.Value = 0;
            txtDonGia.Text = "";
            txtChietKhau.Text = "";
            lblThanhTien.Text = "";
            DgvHoaDonNhap.DataSource= bus_hdn.GetData();
            dgvChiTietHDN.DataSource = "";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lammoi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mục cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            hdn.MaHDN = txtMaHD.Text;
            DgvHoaDonNhap.DataSource = bus_hdn.TimKiem(hdn);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DateTime maxDate = DateTime.Now;
            DateTime minDate = new DateTime(1980, 1, 1);

            hdn.MaHDN = txtMaHD.Text;
            hdn.NgayNhap = string.Format("{0}/{1}/{2}", dtbNgay.Value.Year, dtbNgay.Value.Month, dtbNgay.Value.Day);
            hdn.MaNCC = cbbMaNCC.SelectedValue != null ? cbbMaNCC.SelectedValue.ToString() : null;
            hdn.MaNV = cbbMaNV.SelectedValue != null ? cbbMaNV.SelectedValue.ToString() : null;
            hdn.TongTien = 0;
           
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
            if (cbbMaNCC.SelectedValue == null)
            {
                MessageBox.Show("Mã nhà cung cấp không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbbMaNV.SelectedValue == null)
            {
                MessageBox.Show("Mã nhân viên không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(hdn.MaHDN) || string.IsNullOrEmpty(hdn.MaNCC) || string.IsNullOrEmpty(hdn.MaNV))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bus_hdn.SuaHoaDonNhap(hdn))
            {
                MessageBox.Show("Bản ghi đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lammoi();
            }
        }

        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            string maHDN = txtMaHD.Text;
            ctHDN.MaHDN = maHDN;
            ctHDN.MaSP = cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null;
            ctHDN.SoLuong = (int)nudSoLuong.Value;
            ctHDN.DonGia = float.TryParse(txtDonGia.Text, out float donGia) ? donGia : 0;
            float DonGiaSP = bus_sp.LayDonGia(cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null);
            if (!float.TryParse(txtChietKhau.Text, out float chietKhau))
            {
                MessageBox.Show("Chiết khấu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ctHDN.ChietKhau = chietKhau;
            ctHDN.ThanhTien = (ctHDN.SoLuong * ctHDN.DonGia) - (ctHDN.SoLuong * ctHDN.DonGia * ctHDN.ChietKhau / 100);

            if (cbbMaSP.SelectedValue == null)
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDN.SoLuong <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDN.DonGia > DonGiaSP)
            {
                MessageBox.Show("Giá nhập phải nhỏ hơn giá bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDN.DonGia <= 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDN.ChietKhau < 0 || ctHDN.ChietKhau > 100)
            {
                MessageBox.Show("Chiết khấu trong khoản 0->100%!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtMaHD.Text) || string.IsNullOrEmpty(txtDonGia.Text) || string.IsNullOrEmpty(txtChietKhau.Text))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (bus_cthd.SuaChiTietHDN(ctHDN))
            {
                int soluongmoi = ctHDN.SoLuong - SoluongCu;
                bus_cthd.CongSL(ctHDN.MaSP, soluongmoi);
                MessageBox.Show("Chi tiết hóa đơn đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvChiTietHDN.DataSource = bus_cthd.GetDataByMaHDN(maHDN);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            hdn.MaHDN = txtMaHD.Text;
            if (string.IsNullOrEmpty(hdn.MaHDN))
            {
                MessageBox.Show("Vui lòng chọn mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bool KTraChiTietHDN = bus_cthd.KiemTraMaTrung(hdn.MaHDN) > 0;
                if (KTraChiTietHDN)
                {
                    MessageBox.Show("Phải xóa chi tiết hóa đơn trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult x = MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (x == DialogResult.Yes)
                    {
                        if (bus_hdn.KiemTraMaTrung(hdn.MaHDN) > 0)
                        {
                            if (bus_hdn.XoaHoaDonNhap(hdn))
                            {
                                MessageBox.Show("Bản ghi đã được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lammoi();
                            }
                        }
                    }
                }
            }
        }


        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            string maHDN = txtMaHD.Text;
            string maSP = cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null;

            if (string.IsNullOrEmpty(maHDN) || string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Vui lòng chọn mã hóa đơn và mã sản phẩm để xóa chi tiết hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (bus_cthd.XoaChiTietHDN(maHDN, maSP))
                {
                    int soluong = int.Parse(nudSoLuong.Text);
                    bus_cthd.CongSL(maSP, -soluong);
                    MessageBox.Show("Đã xóa chi tiết hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvChiTietHDN.DataSource = bus_cthd.GetDataByMaHDN(maHDN);
                }
                else
                {
                    MessageBox.Show("Không thể xóa chi tiết hóa đơn này. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void TinhTongTienHoaDon()
        {
            float tongTien = 0;
            if (dgvChiTietHDN != null && dgvChiTietHDN.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvChiTietHDN.Rows)
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


        private void btnThem_Click_1(object sender, EventArgs e)
        {
            DateTime maxDate = DateTime.Now;
            DateTime minDate = new DateTime(1980, 1, 1);

            hdn.MaHDN = txtMaHD.Text;
            hdn.NgayNhap = string.Format("{0}/{1}/{2}", dtbNgay.Value.Year, dtbNgay.Value.Month, dtbNgay.Value.Day);
            hdn.MaNCC = cbbMaNCC.SelectedValue != null ? cbbMaNCC.SelectedValue.ToString() : null;
            hdn.MaNV = cbbMaNV.SelectedValue != null ? cbbMaNV.SelectedValue.ToString() : null;
            hdn.TongTien = 0;

            if (!hdn.MaHDN.StartsWith("HDN"))
            {
                MessageBox.Show("Mã hóa đơn nhập phải có 'HDB' đứng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (cbbMaNCC.SelectedValue == null)
            {
                MessageBox.Show("Mã nhà cung cấp không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbbMaNV.SelectedValue == null)
            {
                MessageBox.Show("Mã nhân viên không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(hdn.MaHDN) || string.IsNullOrEmpty(hdn.MaNCC) || string.IsNullOrEmpty(hdn.MaNV))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bus_hdn.KiemTraMaTrung(hdn.MaHDN) > 0)
            {
                MessageBox.Show("Mã đã tồn tại, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bus_hdn.ThemHoaDonNhap(hdn))
            {
                MessageBox.Show("Bản ghi đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lammoi();
            }
        }

        private void btnThemCT_Click_1(object sender, EventArgs e)
        {
            string maHDN = txtMaHD.Text;
            ctHDN.MaHDN = maHDN;
            ctHDN.MaSP = cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null;
            ctHDN.SoLuong = (int)nudSoLuong.Value;
            ctHDN.DonGia = float.TryParse(txtDonGia.Text, out float donGia) ? donGia : 0;
            float DonGiaSP = bus_sp.LayDonGia(cbbMaSP.SelectedValue != null ? cbbMaSP.SelectedValue.ToString() : null);
            if (!float.TryParse(txtChietKhau.Text, out float chietKhau))
            {
                MessageBox.Show("Chiết khấu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ctHDN.ChietKhau = chietKhau;
            ctHDN.ThanhTien = (ctHDN.SoLuong * ctHDN.DonGia) - (ctHDN.SoLuong * ctHDN.DonGia * ctHDN.ChietKhau / 100);

            if (cbbMaSP.SelectedValue == null)
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(ctHDN.SoLuong <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(ctHDN.DonGia > DonGiaSP)
            {
                MessageBox.Show("Giá nhập phải nhỏ hơn giá bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDN.DonGia <= 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctHDN.ChietKhau < 0 || ctHDN.ChietKhau > 100)
            {
                MessageBox.Show("Chiết khấu trong khoản 0->100%!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtMaHD.Text) || string.IsNullOrEmpty(txtDonGia.Text) || string.IsNullOrEmpty(txtChietKhau.Text))
            {
                MessageBox.Show("Không được bỏ trống, nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (bus_cthd.ThemChiTietHDN(ctHDN))
            {

                bus_cthd.CongSL(ctHDN.MaSP,ctHDN.SoLuong);
                MessageBox.Show("Chi tiết hóa đơn đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvChiTietHDN.DataSource = bus_cthd.GetDataByMaHDN(maHDN);
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaHD.Text))
            {
                XuatHDN formHDN = new XuatHDN(txtMaHD.Text);
                formHDN.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

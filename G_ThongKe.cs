using BUS;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace project1
{
    public partial class G_ThongKe : Form
    {
        B_ThongKe bus_tk = new B_ThongKe();

        public G_ThongKe()
        {
            InitializeComponent();
        }

        private void G_ThongKe_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DateTime fromDate = dtpNgayBD.Value;
            DateTime toDate = dtpNgayKT.Value;

            DataTable dtTop5HoaDon = bus_tk.LayTopHoaDonBanChay(fromDate, toDate);

            HienThiDuLieu(dtTop5HoaDon);
        }

        private void HienThiDuLieu(DataTable dt)
        {
            ChartBDTron.Series.Clear();

            Series seriesCot = ChartBDCot.Series.FirstOrDefault();
            if (seriesCot != null)
                seriesCot.Points.Clear();
            else
            {
                seriesCot = new Series("Doanh thu") { ChartType = SeriesChartType.Column };
                ChartBDCot.Series.Add(seriesCot);
            }

            // Tạo chuỗi "Số sản phẩm đã bán" nếu chưa tồn tại
            Series seriesSPDaBan = ChartBDTron.Series.FirstOrDefault(s => s.Name == "Số sản phẩm đã bán");
            if (seriesSPDaBan == null)
            {
                seriesSPDaBan = new Series("Số sản phẩm đã bán") { ChartType = SeriesChartType.Pie };
                ChartBDTron.Series.Add(seriesSPDaBan);
            }

            decimal tongDoanhThu = 0;
            int tongSoSanPham = 0;

            foreach (DataRow row in dt.Rows)
            {
                string tenSanPham = row["tenSP"].ToString();
                int soLuongBan = Convert.ToInt32(row["SoLuongBan"]);
                DateTime ngayBan = Convert.ToDateTime(row["ngayban"]);
                decimal doanhThu = Convert.ToDecimal(row["DoanhThu"]);

                seriesCot.Points.AddXY(ngayBan.ToString("dd/MM/yyyy"), doanhThu);

                tongDoanhThu += doanhThu;
                tongSoSanPham += soLuongBan;

                // Thêm dữ liệu vào chuỗi "Số sản phẩm đã bán"
                DataPoint dp = new DataPoint();
                dp.AxisLabel = soLuongBan.ToString();
                dp.YValues = new double[] { soLuongBan };
                dp.LegendText = tenSanPham;
                seriesSPDaBan.Points.Add(dp); // Sử dụng seriesSPDaBan thay vì ChartBDTron.Series["Số sản phẩm đã bán"]
            }

            lblDoanhThu.Text = tongDoanhThu.ToString();
            lblSoSanPham.Text = tongSoSanPham.ToString();

            LoadDGVThongTin();
        }


        private void LoadDGVThongTin()
        {
            dgvThongTin.Columns.Clear();
            dgvThongTin.Columns.Add("maSP", "Mã sản phẩm");
            dgvThongTin.Columns.Add("tenSP", "Tên sản phẩm");
            dgvThongTin.Columns.Add("size", "Kích thước");
            dgvThongTin.Columns.Add("soLuong", "Số lượng");
            dgvThongTin.Columns.Add("donGia", "Đơn giá");

            foreach (DataPoint dp in ChartBDTron.Series["Số sản phẩm đã bán"].Points)
            {
                string tenSanPham = dp.LegendText;

                DataRow sp = bus_tk.LayThongTinSanPhamTheoTen(tenSanPham);

                if (sp != null)
                {
                    string maSP = sp["maSP"].ToString();
                    string size = sp["size"].ToString();
                    int soLuong = Convert.ToInt32(sp["soluong"]);
                    decimal donGia = Convert.ToDecimal(sp["dongia"]);

                    dgvThongTin.Rows.Add(maSP, tenSanPham, size, soLuong, donGia);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

using BUS;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace project1
{
    public partial class XuatHD : Form
    {
        private string maHDB;
        B_ChiTietHDB bus_cthd = new B_ChiTietHDB();
        B_HoaDonBan bus_hdb = new B_HoaDonBan();

        public XuatHD(string maHDB)
        {
            InitializeComponent();
            this.maHDB = maHDB;
        }

        private void XuatHD_Load(object sender, EventArgs e)
        {
            try
            {
                reportViewerHD.LocalReport.ReportEmbeddedResource = "project1.ReportHD.rdlc";

                DataTable hdbData = bus_hdb.GetData().AsEnumerable()
                    .Where(row => row.Field<string>("maHDB") == maHDB)
                    .CopyToDataTable();

                DataTable cthdData = bus_cthd.GetData().AsEnumerable()
                    .Where(row => row.Field<string>("maHDB") == maHDB)
                    .CopyToDataTable();
                double tongTien = 0;
                foreach (DataRow cthdRow in cthdData.Rows)
                {
                    string maSP = cthdRow.Field<string>("maSP");
                    string tenSP = bus_cthd.GetTenSP(maSP);
                    cthdRow.SetField("maSP", tenSP);
                    tongTien += cthdRow.Field<double>("thanhtien");
                }

                string tenKH = "";
                string tenNV = "";
                DateTime ngayBan = DateTime.MinValue;
                

                if (hdbData.Rows.Count > 0)
                {
                    DataRow hdbRow = hdbData.Rows[0];

                    string maKH = hdbRow.Field<string>("maKH");
                    tenKH = bus_hdb.GetTenKH(maKH);
                    hdbRow.SetField("maKH", tenKH);

                    string maNV = hdbRow.Field<string>("maNV");
                    tenNV = bus_hdb.GetTenNV(maNV);
                    hdbRow.SetField("maNV", tenNV);
                    ngayBan = hdbRow.Field<DateTime>("ngayban");

                }

                ReportDataSource reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "dsHDB";
                reportDataSource1.Value = hdbData;

                ReportDataSource reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "dsChiTietHDB";
                reportDataSource2.Value = cthdData;

                ReportParameter reportParameter5 = new ReportParameter("maHDB", maHDB);
                ReportParameter reportParameter1 = new ReportParameter("tenKH", tenKH);
                ReportParameter reportParameter2 = new ReportParameter("tenNV", tenNV);
                ReportParameter reportParameter3 = new ReportParameter("ngayban", ngayBan.ToString("dd/MM/yyyy"));
                ReportParameter reportParameter4 = new ReportParameter("tongtien", tongTien.ToString());

                reportViewerHD.LocalReport.DataSources.Clear();
                reportViewerHD.LocalReport.SetParameters(new ReportParameter[] {
            reportParameter1, reportParameter2, reportParameter3, reportParameter4, reportParameter5 });

                reportViewerHD.LocalReport.DataSources.Add(reportDataSource1);
                reportViewerHD.LocalReport.DataSources.Add(reportDataSource2);

                this.reportViewerHD.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

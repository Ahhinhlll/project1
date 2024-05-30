using BUS;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project1
{
    public partial class XuatHDN : Form
    {
        private string maHDN;
        B_ChiTietHDN bus_cthd = new B_ChiTietHDN();
        B_HoaDonNhap bus_hdn = new B_HoaDonNhap();
        public XuatHDN(string maHDN)
        {
            InitializeComponent();
            this.maHDN = maHDN;         
        }

        private void XuatHDN_Load(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "project1.ReportHDN.rdlc";

                DataTable hdnData = bus_hdn.GetData().AsEnumerable()
                    .Where(row => row.Field<string>("maHDN") == maHDN)
                    .CopyToDataTable();

                DataTable cthdData = bus_cthd.GetData().AsEnumerable()
                    .Where(row => row.Field<string>("maHDN") == maHDN)
                    .CopyToDataTable();
              
                double tongTien = 0;
                foreach (DataRow cthdRow in cthdData.Rows)
                {
                    string maSP = cthdRow.Field<string>("maSP");
                    string tenSP = bus_cthd.GetTenSP(maSP);
                    cthdRow.SetField("maSP", tenSP);
                    tongTien += cthdRow.Field<double>("thanhtien");
                }

                string tenNCC = "";
                string tenNV = "";
                DateTime ngayNhap = DateTime.MinValue;


                if (hdnData.Rows.Count > 0)
                {
                    DataRow hdnRow = hdnData.Rows[0];

                    string maNCC = hdnRow.Field<string>("maNCC");
                    tenNCC = bus_hdn.GetTenNCC(maNCC);
                    hdnRow.SetField("maNCC", tenNCC);

                    string maNV = hdnRow.Field<string>("maNV");
                    tenNV = bus_hdn.GetTenNV(maNV);
                    hdnRow.SetField("maNV", tenNV);
                    ngayNhap = hdnRow.Field<DateTime>("ngaynhap");

                }

                ReportDataSource reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "dsHDN";
                reportDataSource1.Value = hdnData;

                ReportDataSource reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "dsChiTietHDN";
                reportDataSource2.Value = cthdData;

                ReportParameter reportParameter5 = new ReportParameter("maHDN", maHDN);
                ReportParameter reportParameter1 = new ReportParameter("tenNCC", tenNCC);
                ReportParameter reportParameter2 = new ReportParameter("tenNV", tenNV);
                ReportParameter reportParameter3 = new ReportParameter("ngaynhap", ngayNhap.ToString("dd/MM/yyyy"));
                ReportParameter reportParameter4 = new ReportParameter("tongtien", tongTien.ToString());

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] {
            reportParameter1, reportParameter2, reportParameter3, reportParameter4, reportParameter5 });

                reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource2);

                this.reportViewer1.RefreshReport();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   
        }
    }
}

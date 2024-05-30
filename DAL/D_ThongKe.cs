using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class D_ThongKe : dbConnect
    {
        public DataTable TopHoaDonBanChay(DateTime fromDate, DateTime toDate)
        {
            string sql = @"
                SELECT TOP 5 HD.ngayban, SP.tenSP, SUM(CTHD.soluongban) AS SoLuongBan, SUM(CTHD.soluongban * CTHD.dongiaban) AS DoanhThu
                FROM SanPham SP
                JOIN ChiTietHDB CTHD ON SP.maSP = CTHD.maSP
                JOIN HoaDonBan HD ON CTHD.maHDB = HD.maHDB
                WHERE HD.ngayban BETWEEN @FromDate AND @ToDate
                GROUP BY HD.ngayban, SP.tenSP
                ORDER BY DoanhThu DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            };

            return getData(sql, parameters);
        }
        public DataRow ThongTinSP(string tenSanPham)
        {
            string sql = "SELECT * FROM SanPham WHERE tenSP = @TenSanPham";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenSanPham", tenSanPham)
            };

            DataTable dt = getData(sql, parameters);

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

    }
}

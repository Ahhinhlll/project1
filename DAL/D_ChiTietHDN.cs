using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class D_ChiTietHDN : dbConnect
    {
        dbConnect connect = new dbConnect();

        public DataTable getdata()
        {
            string sql = "select * from ChiTietHDN";
            return connect.getData(sql);
        }

        public int kiemtramatrung(string ma)
        {
            string sql = $"select count(*) from ChiTietHDN where maHDN = '{ma}'";
            return connect.kiemtramatrung(ma, sql);
        }

        public bool ThemChiTietHDN(ChiTietHDN cthd)
        {
            if (KiemTraTonTai(cthd.MaHDN, cthd.MaSP))
            {
                return SuaChiTietHDN(cthd);
            }
            else
            {
                string sql = "INSERT INTO ChiTietHDN (maHDN, maSP, soluongnhap, dongianhap, chietkhau, thanhtien) VALUES (@maHDN, @maSP, @soluongnhap, @dongianhap, @chietkhau, @thanhtien)";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@maHDN", SqlDbType.VarChar) { Value = cthd.MaHDN },
                    new SqlParameter("@maSP", SqlDbType.VarChar) { Value = cthd.MaSP },
                    new SqlParameter("@soluongnhap", SqlDbType.Int) { Value = cthd.SoLuong },
                    new SqlParameter("@dongianhap", SqlDbType.Float) { Value = cthd.DonGia },
                    new SqlParameter("@chietkhau", SqlDbType.Float) { Value = cthd.ChietKhau },
                    new SqlParameter("@thanhtien", SqlDbType.Float) { Value = cthd.ThanhTien }
                };
                connect.ExecuteNonQuery(sql, parameters);
                return true;
            }
        }

        private bool KiemTraTonTai(string maHDN, string maSP)
        {
            string sql = $"SELECT COUNT(*) FROM ChiTietHDN WHERE maHDN = '{maHDN}' AND maSP = '{maSP}'";
            int count = connect.kiemtramatrung(maHDN, sql);
            return count > 0;
        }

        public bool SuaChiTietHDN(ChiTietHDN cthd)
        {
            string sql = "UPDATE ChiTietHDN SET soluongnhap = soluongnhap + (@soluongnhap - soluongnhap), dongianhap = @dongianhap, chietkhau = @chietkhau, thanhtien = @thanhtien WHERE maHDN = @maHDN AND maSP = @maSP";
            SqlParameter[] parameters =
            {
                new SqlParameter("@soluongnhap", SqlDbType.Int) { Value = cthd.SoLuong },
                new SqlParameter("@dongianhap", SqlDbType.Float) { Value = cthd.DonGia },
                new SqlParameter("@chietkhau", SqlDbType.Float) { Value = cthd.ChietKhau },
                new SqlParameter("@thanhtien", SqlDbType.Float) { Value = cthd.ThanhTien },
                new SqlParameter("@maHDN", SqlDbType.VarChar) { Value = cthd.MaHDN },
                new SqlParameter("@maSP", SqlDbType.VarChar) { Value = cthd.MaSP }
            };
            connect.ExecuteNonQuery(sql, parameters);
            return true;
        }

        public bool XoaChiTietHDN(string maHDN, string maSP)
        {
            string sql = $"DELETE FROM ChiTietHDN WHERE maHDN = '{maHDN}' AND maSP = '{maSP}'";
            connect.thucthisql(sql);
            return true;
        }


        public DataTable GetByMaHDN(string maHDN)
        {
            string sql = $"SELECT * FROM ChiTietHDN WHERE MaHDN = '{maHDN}'";
            return connect.getData(sql);
        }
        public string GetTenSanPham(string maSP)
        {
            string sql = $"SELECT tenSP FROM SanPham WHERE maSP = '{maSP}'";
            DataTable dt = connect.getData(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["tenSP"].ToString();
            }
            return null;
        }
        public bool CongSL(string maSP,int soluong)
        {
            string sql = "update SanPham set soluong = soluong + @soluongmoi where maSP = @maSP ";
            SqlParameter[] parameters =
            {
                new SqlParameter("@maSP",maSP),
                new SqlParameter("@soluongmoi",soluong),
            };
            connect.ExecuteNonQuery (sql, parameters);
            return true;    
        }

    }
}

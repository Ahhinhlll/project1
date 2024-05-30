using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class D_ChiTietHDB : dbConnect
    {
        dbConnect connect = new dbConnect();
        public DataTable getdata()
        {
            string sql = "select * from ChiTietHDB";
            return connect.getData(sql);
        }
        public int kiemtramatrung(string ma)
        {
            string sql = $"select count(*) from ChiTietHDB where maHDB = '{ma}'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemChiTietHDB(ChiTietHDB cthd)
        {
            if (KiemTraTonTai(cthd.MaHDB, cthd.MaSP))
            {
                return SuaChiTietHDB(cthd);
            }
            else
            {
                string sql = "INSERT INTO ChiTietHDB (maHDB, maSP, soluongban, dongiaban, chietkhau, thanhtien) VALUES (@maHDB, @maSP, @soluongban, @dongiaban, @chietkhau, @thanhtien)";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@maHDB", SqlDbType.VarChar) { Value = cthd.MaHDB },
                    new SqlParameter("@maSP", SqlDbType.VarChar) { Value = cthd.MaSP },
                    new SqlParameter("@soluongban", SqlDbType.Int) { Value = cthd.SoLuong },
                    new SqlParameter("@dongiaban", SqlDbType.Float) { Value = cthd.DonGia },
                    new SqlParameter("@chietkhau", SqlDbType.Float) { Value = cthd.ChietKhau },
                    new SqlParameter("@thanhtien", SqlDbType.Float) { Value = cthd.ThanhTien }
                };
                connect.ExecuteNonQuery(sql, parameters);
                return true;
            }
        }
        private bool KiemTraTonTai(string maHDB, string maSP)
        {
            string sql = $"SELECT COUNT(*) FROM ChiTietHDB WHERE maHDB = '{maHDB}' AND maSP = '{maSP}'";
            int count = connect.kiemtramatrung(maHDB, sql);
            return count > 0;
        }
        public bool SuaChiTietHDB(ChiTietHDB cthd)
        {
            string sql = "UPDATE ChiTietHDB SET soluongban = soluongban + (@soluongban - soluongban), dongiaban = @dongiaban, chietkhau = @chietkhau, thanhtien = @thanhtien WHERE maHDB = @maHDB AND maSP = @maSP";
            SqlParameter[] parameters =
            {
                new SqlParameter("@soluongban", SqlDbType.Int) { Value = cthd.SoLuong },
                new SqlParameter("@dongiaban", SqlDbType.Float) { Value = cthd.DonGia },
                new SqlParameter("@chietkhau", SqlDbType.Float) { Value = cthd.ChietKhau },
                new SqlParameter("@thanhtien", SqlDbType.Float) { Value = cthd.ThanhTien },
                new SqlParameter("@maHDB", SqlDbType.VarChar) { Value = cthd.MaHDB },
                new SqlParameter("@maSP", SqlDbType.VarChar) { Value = cthd.MaSP }
            };
            connect.ExecuteNonQuery(sql, parameters);
            return true;
        }
        public bool XoaChiTietHDB(string maHDB, string maSP)
        {
            string sql = $"DELETE FROM ChiTietHDB WHERE maHDB = '{maHDB}' AND maSP = '{maSP}'";
            connect.thucthisql(sql);
            return true;
        }
        public DataTable GetByMaHDB(string maHDB)
        {
            string sql = $"SELECT * FROM ChiTietHDB WHERE MaHDB = '{maHDB}'";
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
        public bool TruSL(string maSP, int soluong)
        {
            string sql = "update SanPham set soluong = soluong - @soluongmoi where maSP = @maSP ";
            SqlParameter[] parameters =
            {
                new SqlParameter("@maSP",maSP),
                new SqlParameter("@soluongmoi",soluong),
            };
            connect.ExecuteNonQuery(sql, parameters);
            return true;
        }

    }
}

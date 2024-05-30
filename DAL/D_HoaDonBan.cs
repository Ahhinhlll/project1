using DTO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class D_HoaDonBan : dbConnect
    {
        dbConnect connect = new dbConnect();

        public DataTable getData()
        {
            string sql = "Select * from HoaDonBan";
            return connect.getData(sql);
        }

        public int kiemtramatrung(string ma)
        {
            string sql = $"select count(*) from HoaDonBan where maHDB = '{ma}'";
            return connect.kiemtramatrung(ma, sql);
        }

        public bool ThemHDB(HoaDonBan hdb)
        {
            string sql = string.Format($"INSERT INTO HoaDonBan VALUES('{hdb.MaHDB}', '{hdb.NgayBan}', '{hdb.MaKH}', '{hdb.MaNV}', {hdb.TongTien})");
            thucthisql(sql);
            return true;
        }

        public bool SuaHDB(HoaDonBan hdb)
        {
            string sql = string.Format($"UPDATE HoaDonBan SET ngayban= '{hdb.NgayBan}', maKH= '{hdb.MaKH}', maNV= '{hdb.MaNV}', tongtien= {hdb.TongTien} WHERE maHDB = '{hdb.MaHDB}'");
            thucthisql(sql);
            return true;
        }

        public bool XoaHDN(HoaDonBan hdb)
        {
            string sql = string.Format($"DELETE FROM HoaDonBan WHERE maHDB = '{hdb.MaHDB}'");
            thucthisql(sql);
            return true;
        }

        public DataTable TimKiem(HoaDonBan hdb)
        {
            string sql = $"SELECT * FROM HoaDonBan WHERE maHDB LIKE '%{hdb.MaHDB}%'";
            return connect.getData(sql);
        }

        public string GetTenNhanVien(string maNV)
        {
            string sql = $"SELECT tenNV FROM NhanVien WHERE maNV = '{maNV}'";
            DataTable dt = connect.getData(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["tenNV"].ToString();
            }
            return null;
        }

        public string GetTenKhachHang(string maKH)
        {
            string sql = $"SELECT tenKH FROM KhachHang WHERE maKH = '{maKH}'";
            DataTable dt = connect.getData(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["tenKH"].ToString();
            }
            return null;
        }
      
    }
}

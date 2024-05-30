using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class D_HoaDonNhap : dbConnect
    {
        dbConnect connect = new dbConnect();
        public DataTable getData()
        {
            string sql = "Select * from HoaDonNhap";
            return connect.getData(sql);
        }
        public int kiemtramatrung(string ma)
        {
            string sql = $"select count(*) from HoaDonNhap where maHDN = '{ma}'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemHDN(HoaDonNhap hdn)
        {
            string sql = string.Format($"INSERT INTO HoaDonNhap VALUES('{hdn.MaHDN}', '{hdn.NgayNhap}', '{hdn.MaNCC}', '{hdn.MaNV}', {hdn.TongTien})");
            thucthisql(sql);
            return true;
        }
        public bool SuaHDN(HoaDonNhap hdn)
        {
            string sql = string.Format($"UPDATE HoaDonNhap SET ngaynhap= '{hdn.NgayNhap}', maNCC= '{hdn.MaNCC}', maNV= '{hdn.MaNV}', tongtien= {hdn.TongTien} WHERE maHDN = '{hdn.MaHDN}'");
            thucthisql(sql);
            return true;
        }
        public bool XoaHDN(HoaDonNhap hdn)
        {
            string sql = string.Format($"DELETE FROM HoaDonNhap WHERE maHDN = '{hdn.MaHDN}'");
            thucthisql(sql);
            return true;
        }
        public DataTable TimKiem(HoaDonNhap hdn)
        {
            string sql = $"SELECT * FROM HoaDonNhap WHERE maHDN LIKE '%{hdn.MaHDN}%' ";
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

        public string GetTenNhaCungCap(string maNCC)
        {
            string sql = $"SELECT tenNCC FROM NhaCungCap WHERE maNCC = '{maNCC}'";
            DataTable dt = connect.getData(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["tenNCC"].ToString();
            }
            return null;
        }
    }
}

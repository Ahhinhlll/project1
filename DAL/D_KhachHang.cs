using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class D_KhachHang : dbConnect
    {
        dbConnect connect = new dbConnect();
        public DataTable getData()
        {
            string sql = "Select * from KhachHang";
            return connect.getData(sql);
        }
        public int kiemtramatrung(string ma)
        {
            string sql = $"select count(*) from KhachHang where maKH = '{ma}'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemKH(KhachHang kh)
        {
            string sql = string.Format($"INSERT INTO KhachHang VALUES('{kh.MaKH}', N'{kh.TenKH}', N'{kh.DiaChi}', '{kh.SDT}')");
            thucthisql(sql);
            return true;
        }
        public bool SuakH(KhachHang kh)
        {
            string sql = string.Format($"UPDATE KhachHang SET tenKH= N'{kh.TenKH}', diachi= N'{kh.DiaChi}', sdt= '{kh.SDT}' WHERE maKH = '{kh.MaKH}'");
            thucthisql(sql);
            return true;
        }
        public bool XoaKH(KhachHang kh)
        {
            string sql = string.Format($"DELETE FROM KhachHang WHERE maKH = '{kh.MaKH}'");
            thucthisql(sql);
            return true;
        }
        public DataTable TimKiem(KhachHang kh)
        {
            string sql = $"SELECT * FROM KhachHang WHERE maKH LIKE '%{kh.MaKH}%' OR tenKH LIKE N'%{kh.TenKH}%'";
            return connect.getData(sql);
        }

       
    }
}

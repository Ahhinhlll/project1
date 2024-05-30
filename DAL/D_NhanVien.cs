using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class D_NhanVien : dbConnect
    {
        dbConnect connect = new dbConnect();

        public DataTable GetData()
        {
            string sql = "select * from NhanVien";
            return connect.getData(sql);
        }
        public int kiemtramatrung(string ma)
        {
            string sql = $"select count(*) from NhanVien where maNV = '{ma}'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemNV(NhanVien nv)
        {
            string sql = string.Format($"insert into NhanVien values ('{nv.MaNV}', N'{nv.TenNV}',N'{nv.GioiTinh}',N'{nv.DiaChi}','{nv.SDT}','{nv.Email}')");
            thucthisql(sql);
            return true;
        }
        public bool SuaNV(NhanVien nv)
        {
            string sql = string.Format($"update NhanVien set tenNV=N'{nv.TenNV}',gioitinh=N'{nv.GioiTinh}',diachi=N'{nv.DiaChi}',sdt='{nv.SDT}',email='{nv.Email}' where maNV ='{nv.MaNV}'");
            thucthisql(sql);
            return true;
        }
        public bool XoaNV(NhanVien nv)
        {
            string sql = string.Format($"delete from NhanVien where maNV = '{nv.MaNV}'");
            thucthisql(sql);
            return true;
        }
        public DataTable TimKiem(NhanVien nv)
        {
            string sql = $"SELECT * FROM NhanVien WHERE maNV LIKE '%{nv.MaNV}%' OR tenNV LIKE N'%{nv.TenNV}%'";
            return connect.getData(sql);
        }

       
    }
}

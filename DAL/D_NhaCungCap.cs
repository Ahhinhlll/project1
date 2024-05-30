using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class D_NhaCungCap : dbConnect
    {
        dbConnect connect = new dbConnect();
        public DataTable getData()
        {
            string sql = "Select * from NhaCungCap";
            return connect.getData(sql);
        }
        public int kiemtramatrung(string ma)
        {
            string sql = $"select count(*) from NhaCungCap where maNCC = '{ma}'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemNCC(NhaCungCap ncc)
        {
            string sql = string.Format($"INSERT INTO NhaCungCap VALUES('{ncc.MaNCC}', N'{ncc.TenNCC}', N'{ncc.DiaChi}', '{ncc.SDT}')");
            thucthisql(sql);
            return true;
        }
        public bool SuaNCC(NhaCungCap ncc)
        {
            string sql = string.Format($"UPDATE NhaCungCap SET tenNCC= N'{ncc.TenNCC}', diachi= N'{ncc.DiaChi}', sdt= '{ncc.SDT}' WHERE maNCC = '{ncc.MaNCC}'");
            thucthisql(sql);
            return true;
        }
        public bool XoaNCC(NhaCungCap ncc)
        {
            string sql = string.Format($"DELETE FROM NhaCungCap WHERE maNCC = '{ncc.MaNCC}'");
            thucthisql(sql);
            return true;
        }
        public DataTable TimKiem(NhaCungCap ncc)
        {
            string sql = $"SELECT * FROM NhaCungCap WHERE maNCC LIKE '%{ncc.MaNCC}%' OR tenNCC LIKE N'%{ncc.TenNCC}%'";
            return connect.getData(sql);
        }
    }
}

using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class D_DanhMuc : dbConnect
    {
        //chứa mọi xử lý liên quan đến CSDL
        //xử lý lấy dữ liệu để đưa ra dgv
        //khởi tạo đối tượng thuộc lớp DBConnect
        dbConnect connect = new dbConnect();
        public DataTable getData()
        {
            string sql = "Select * from DanhMuc";
            return connect.getData(sql);
        }
        public int kiemtramatrung(string ma)
        {
            string sql = $"select count(*) from DanhMuc where maDM = '{ma}'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemDM(DanhMuc dm)
        {
            string sql = string.Format($"INSERT INTO DanhMuc VALUES('{dm.maDM}', N'{dm.tenDM}')");
            thucthisql(sql);
            return true;
        }
        public bool SuaDM(DanhMuc dm)
        {
            string sql = string.Format($"UPDATE DanhMuc SET tenDM= N'{dm.tenDM}' WHERE maDM = '{dm.maDM}'");
            thucthisql(sql);
            return true;
        }
        public bool XoaDM(DanhMuc dm)
        {
            string sql = string.Format( $"DELETE FROM DanhMuc WHERE maDM = '{dm.maDM}'");
            thucthisql(sql);
            return true;
        }
        public DataTable TimKiem(DanhMuc dm)
        {
            string sql = $"SELECT * FROM DanhMuc WHERE maDM LIKE '%{dm.maDM}%' OR tenDM LIKE N'%{dm.tenDM}%'";
            return connect.getData(sql);
        }

    }
}

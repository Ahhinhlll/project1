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
    public class D_SanPham : dbConnect
    {
        //chứa mọi xử lý liên quan đến CSDL
        //xử lý lấy dữ liệu để đưa ra dgv
        //khởi tạo đối tượng thuộc lớp DBConnect
        dbConnect connect = new dbConnect();
        public DataTable getData()
        {
            string sql = "Select * from SanPham";
            return connect.getData(sql);//chứa dữ liệu bảng SanPham
        }
        public int kiemtramatrung(string ma)
        {
            string sql = $"select count(*) from SanPham where maSP = '{ma}'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemSP(SanPham sp)
        {
            string sql = string.Format($"INSERT INTO SanPham VALUES('{sp.maSP}', N'{sp.tenSP}', '{sp.maDM}', '{sp.size}', {sp.soLuong}, {sp.donGia})");
            thucthisql(sql);
            return true;
        }
        public bool SuaSP(SanPham sp)
        {
            string sql = string.Format($"UPDATE SanPham SET tenSP= N'{sp.tenSP}', maDM= '{sp.maDM}', size= '{sp.size}', soluong= {sp.soLuong}, dongia= {sp.donGia} WHERE maSP = '{sp.maSP}'");
            thucthisql(sql);
            return true;
        }
        public bool XoaSP(SanPham sp)
        {
            string sql = string.Format($"DELETE FROM SanPham WHERE maSP = '{sp.maSP}'");
            thucthisql(sql);
            return true;
        }
        public DataTable TimKiem(SanPham sp)
        {
            string sql = $"SELECT * FROM SanPham WHERE maSP LIKE '%{sp.maSP}%' OR tenSP LIKE N'%{sp.tenSP}%'";
            return connect.getData(sql);
        }
        public float LayDonGia(string maSP)
        {
            float donGia = 0;
            string sql = $"SELECT donGia FROM SanPham WHERE maSP = '{maSP}'";
            DataTable dt = connect.getData(sql);
            if (dt.Rows.Count > 0)
            {
                if (float.TryParse(dt.Rows[0]["donGia"].ToString(), out donGia))
                {
                    return donGia;
                }
            }
            return donGia;
        }
        public int Laysoluong(string ma)
        {
            string sql = $"SELECT Soluong FROM SanPham WHERE Masp = '{ma}'";
            DataTable result = connect.getData(sql);

            if (result != null && result.Rows.Count > 0)
            {
                int soLuong = Convert.ToInt32(result.Rows[0]["Soluong"]);
                return soLuong;
            }
            else
            {
                return 0;
            }
        }
        

        //public bool Capnhatsl(string maSP, float slThayDoi)
        //{
        //    string query = "UPDATE SanPham SET soluong = soluong + @slThayDoi WHERE maSP = @maSP";
        //    SqlParameter[] sqlParameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@slThayDoi", slThayDoi),
        //        new SqlParameter("@maSP", maSP)
        //    };
        //    connect.ExecuteNonQuery(query, sqlParameters);
        //    return true;
        //}

        //public bool CapnhatSuaSL(string maSP, float slThayDoi)
        //{
        //    string query = "UPDATE SanPham SET soluong = @slThayDoi - soluong WHERE maSP = @maSP";
        //    SqlParameter[] sqlParameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@slThayDoi", slThayDoi),
        //        new SqlParameter("@maSP", maSP)
        //    };
        //    connect.ExecuteNonQuery(query, sqlParameters);
        //    return true;
        //}
    }
}

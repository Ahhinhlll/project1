using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class D_DangNhap : dbConnect
    {
        dbConnect Connect = new dbConnect();
        public bool Login(string Tentk, string Matkhau)
        {

            string sql = string.Format($"SELECT * FROM DangNhap WHERE Tentk = N'{Tentk}' AND Matkhau = N'{Matkhau}'");
            DataTable result = Connect.getData(sql);
            return result.Rows.Count > 0;
        }
        public string SetQuyen(string tenTK)
        {
            string sql = $"SELECT Quyen FROM DangNhap WHERE Tentk = N'{tenTK}'";
            DataTable result = Connect.getData(sql);
            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["Quyen"].ToString();
            }
            else
            {
                return null;
            }
        }
         public bool DoiPass(string tenTK, string makhau)
         {
            string sql = $"UPDATE DangNhap set matkhau ='{makhau}' WHERE tenTK = '{tenTK}'";
            thucthisql(sql);
            return true;
         }
       
    }
}
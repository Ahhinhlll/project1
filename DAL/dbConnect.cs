using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class dbConnect
    {
        public SqlConnection con;
        public SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string chuoikn = @"Data Source=LAPTOP-PHDK9G4G\SQLEXPRESS;Initial Catalog=DOAN_1;Integrated Security=True";

        public void ketnoi()
        {
            con = new SqlConnection(chuoikn);
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        public void dongkn()
        {
            if (con != null && con.State == ConnectionState.Open)
                con.Close();
        }

        public int thucthisql(string sql)
        {
            ketnoi();
            cmd = new SqlCommand(sql, con);
            int affectedRows = cmd.ExecuteNonQuery();
            dongkn();
            return affectedRows;
        }

        public DataTable getData(string sql, SqlParameter[] parameters = null)
        {
            ketnoi();
            try
            {
                cmd = new SqlCommand(sql, con);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            finally
            {
                dongkn();
            }
            return dt;
        }

        public int kiemtramatrung(string ma, string sql)
        {
            ketnoi();
            int i;
            cmd = new SqlCommand(sql, con);
            i = (int)cmd.ExecuteScalar();
            dongkn();
            return i;
        }

        public void ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            ketnoi();
            using (SqlCommand command = new SqlCommand(sql, con))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.ExecuteNonQuery();
            }
            dongkn();
        }
    }
}

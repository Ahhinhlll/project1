using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class B_DangNhap
    {
        private D_DangNhap DangNhap = new D_DangNhap();

        public bool Login(string tenTK, string Matkhau)
        {
            return DangNhap.Login(tenTK, Matkhau);
        }
        public string SetQuyen(string Tentk)
        {
            return DangNhap.SetQuyen(Tentk);
        }
        public bool DoiPass(string tenTK, string makhau)
        {
            return DangNhap.DoiPass(tenTK, makhau);
        }
    }
}
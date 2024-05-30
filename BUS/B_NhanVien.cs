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
    public class B_NhanVien
    {
        D_NhanVien nhanVien = new D_NhanVien();
        public DataTable GetData()
        {
            return nhanVien.GetData();
        }
        public int kiemtramatrung(string ma)
        {
            return nhanVien.kiemtramatrung(ma);
        }
        public bool ThemNV(NhanVien nv)
        {
            return nhanVien.ThemNV(nv);
        }
        public bool SuaNV(NhanVien nv)
        {
            return nhanVien.SuaNV(nv);
        }
        public bool XoaNV(NhanVien nv)
        {
            return nhanVien.XoaNV(nv);
        }
        public DataTable TimKiem(NhanVien nv)
        {
            return nhanVien.TimKiem(nv);
        }
       
    }
}

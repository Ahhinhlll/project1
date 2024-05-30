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
    public class B_SanPham
    {
        D_SanPham sanPham = new D_SanPham();

        public DataTable GetData()
        {
            return sanPham.getData();
        }
        public int kiemtramatrung(string ma)
        {
            return sanPham.kiemtramatrung(ma);
        }
        public bool ThemSP(SanPham sp)
        {
            return sanPham.ThemSP(sp);
        }
        public bool SuaSP(SanPham sp)
        {
            return sanPham.SuaSP(sp);
        }
        public bool XoaSP(SanPham sp)
        {
            return sanPham.XoaSP(sp);
        }
        public DataTable TimKiem(SanPham sp)
        {
            return sanPham.TimKiem(sp);
        }
        public float LayDonGia(string maSP)
        {
            return sanPham.LayDonGia(maSP);
        }
        public int Laysoluong(string maSP)
        {
            return sanPham.Laysoluong(maSP);
        }

        
        //public bool Capnhatsl(string maSP, float slThayDoi)
        //{
        //    return sanPham.Capnhatsl(maSP, slThayDoi);
        //}

        //public bool CapnhatSuaSL(string maSP, float slThayDoi)
        //{
        //    return sanPham.CapnhatSuaSL(maSP, slThayDoi);
        //}
    }
}

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
    public class B_ChiTietHDN
    {
        private D_ChiTietHDN dal_cthd = new D_ChiTietHDN();
        

        public DataTable GetData()
        {
            return dal_cthd.getdata();
        }
        public int KiemTraMaTrung(string ma)
        {
            return dal_cthd.kiemtramatrung(ma);
        }
        public bool ThemChiTietHDN(ChiTietHDN cthd)
        {       
            return dal_cthd.ThemChiTietHDN(cthd);
        }

        public bool SuaChiTietHDN(ChiTietHDN cthd)
        {
            return dal_cthd.SuaChiTietHDN(cthd);
        }

        public bool XoaChiTietHDN(string maHDN, string maSP)
        {
            return dal_cthd.XoaChiTietHDN(maHDN, maSP);
        }
        public DataTable GetDataByMaHDN(string maHDN)
        {
            return dal_cthd.GetByMaHDN(maHDN);
        }
        public string GetTenSP(string maSP)
        {
            return dal_cthd.GetTenSanPham(maSP);
        }
        public bool CongSL(string maSP, int soluong)
        {
            return dal_cthd.CongSL(maSP, soluong);
        }
    }
}

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
    public class B_HoaDonNhap
    {
        private D_HoaDonNhap dal_hdn = new D_HoaDonNhap();

        public DataTable GetData()
        {
            return dal_hdn.getData();
        }
        public int KiemTraMaTrung(string ma)
        {
            return dal_hdn.kiemtramatrung(ma);
        }
        public bool ThemHoaDonNhap(HoaDonNhap hdn)
        {
            return dal_hdn.ThemHDN(hdn);
        }

        public bool SuaHoaDonNhap(HoaDonNhap hdn)
        {
            return dal_hdn.SuaHDN(hdn);
        }

        public bool XoaHoaDonNhap(HoaDonNhap hdn)
        {
            return dal_hdn.XoaHDN(hdn);
        }
        public DataTable TimKiem(HoaDonNhap hdn)
        {
            return dal_hdn.TimKiem(hdn);
        }
        public string GetTenNCC(string maNCC)
        {
            return dal_hdn.GetTenNhaCungCap(maNCC);
        }
        public string GetTenNV(string maNV)
        {
            return dal_hdn.GetTenNhanVien(maNV);
        }
    }
}

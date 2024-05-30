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
    public class B_HoaDonBan
    {
        private D_HoaDonBan dal_hdb = new D_HoaDonBan();

        public DataTable GetData()
        {
            return dal_hdb.getData();
        }
        public int KiemTraMaTrung(string ma)
        {
            return dal_hdb.kiemtramatrung(ma);
        }
        public bool ThemHoaDonBan(HoaDonBan hdb)
        {
            return dal_hdb.ThemHDB(hdb);
        }

        public bool SuaHoaDonBan(HoaDonBan hdb)
        {
            return dal_hdb.SuaHDB(hdb);
        }

        public bool XoaHoaDonBan(HoaDonBan hdb)
        {
            return dal_hdb.XoaHDN(hdb);
        }
        public DataTable TimKiem(HoaDonBan hdb)
        {
            return dal_hdb.TimKiem(hdb);
        }
        public string GetTenKH(string maKH)
        {
            return dal_hdb.GetTenKhachHang(maKH);
        }
        public string GetTenNV(string maNV)
        {
            return dal_hdb.GetTenNhanVien(maNV);
        }
        //public DateTime GetNgay(string maHDB)
        //{
        //    return dal_hdb.GetNgayBan(maHDB);
        //}
        //public float GetTongTien(string maHDB)
        //{
        //    return dal_hdb.GetTongTien(maHDB);
        //}
    }
}

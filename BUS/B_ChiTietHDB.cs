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
    public class B_ChiTietHDB
    {
        private D_ChiTietHDB dal_cthd = new D_ChiTietHDB();

        public DataTable GetData()
        {
            return dal_cthd.getdata();
        }
        public int KiemTraMaTrung(string ma)
        {
            return dal_cthd.kiemtramatrung(ma);
        }
        public bool ThemChiTietHDB(ChiTietHDB cthd)
        {
            return dal_cthd.ThemChiTietHDB(cthd);
        }

        public bool SuaChiTietHDB(ChiTietHDB cthd)
        {
            return dal_cthd.SuaChiTietHDB(cthd);
        }

        public bool XoaChiTietHDB(string maHDB, string maSP)
        {
            return dal_cthd.XoaChiTietHDB(maHDB, maSP);
        }
        public DataTable GetDataByMaHDB(string maHDB)
        {
            return dal_cthd.GetByMaHDB(maHDB);
        }
        public string GetTenSP(string maSP)
        {
            return dal_cthd.GetTenSanPham(maSP);
        }
        
        public bool TruSl(string maSP, int soluong)
        {
            return dal_cthd.TruSL(maSP, soluong);
        }

    }
}

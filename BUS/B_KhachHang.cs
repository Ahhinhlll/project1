using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class B_KhachHang
    {
        D_KhachHang khachHang = new D_KhachHang();

        public DataTable GetData()
        {
            return khachHang.getData ();
        }
        public int kiemtramatrung(string ma)
        {
            return khachHang.kiemtramatrung (ma);
        }
        public bool ThemKH(KhachHang kh)
        {
            return khachHang.ThemKH (kh);
        }
        public bool SuaKH(KhachHang kh)
        {
            return khachHang.SuakH (kh);
        }
        public bool XoaKH(KhachHang kh)
        {
            return khachHang.XoaKH (kh);
        }
        public DataTable TimKiem(KhachHang kh)
        {
            return khachHang.TimKiem(kh);
        }
       
    }
}

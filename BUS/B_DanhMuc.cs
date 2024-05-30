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
    public class B_DanhMuc
    {        
        D_DanhMuc danhMuc = new D_DanhMuc();
        public DataTable getData()
        {
            return danhMuc.getData();//dữ liệu từ dal đã đc chuyển vào Bus
        }
        public int kiemtramatrung(string ma)
        {
            return danhMuc.kiemtramatrung(ma);
        }
        public bool ThemDM(DanhMuc dm)
        {
            return danhMuc.ThemDM(dm);
        }
        public bool SuaDM(DanhMuc dm)
        {
            return danhMuc.SuaDM(dm);
        }
        public bool XoaDM(DanhMuc dm)
        {
            return danhMuc.XoaDM(dm);
        }
        public DataTable TimKiem(DanhMuc dm)
        {
            return danhMuc.TimKiem(dm);
        }
    }
}

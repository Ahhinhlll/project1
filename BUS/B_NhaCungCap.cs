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
    public class B_NhaCungCap
    {
       D_NhaCungCap nhaCungCap = new D_NhaCungCap();

        public DataTable GetData()
        {
            return nhaCungCap.getData();
        }
        public int kiemtramatrung(string ma)
        {
            return nhaCungCap.kiemtramatrung(ma);
        }
        public bool ThemNCC(NhaCungCap ncc)
        {
            return nhaCungCap.ThemNCC(ncc);
        }
        public bool SuaNCC(NhaCungCap ncc)
        {
            return nhaCungCap.SuaNCC(ncc);
        }
        public bool XoaNCC(NhaCungCap ncc)
        {
            return nhaCungCap.XoaNCC(ncc);
        }
        public DataTable TimKiem(NhaCungCap ncc)
        {
            return nhaCungCap.TimKiem(ncc);
        }
    }
}

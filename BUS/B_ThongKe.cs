using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class B_ThongKe
    {
        private D_ThongKe dalThongKe;

        public B_ThongKe()
        {
            dalThongKe = new D_ThongKe();
        }

        public DataTable LayTopHoaDonBanChay(DateTime fromDate, DateTime toDate)
        {
            return dalThongKe.TopHoaDonBanChay(fromDate, toDate);
        }
        public DataRow LayThongTinSanPhamTheoTen(string tenSanPham)
        {
            return dalThongKe.ThongTinSP(tenSanPham);
        }
    }
}

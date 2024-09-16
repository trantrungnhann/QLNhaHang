using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOANQLNH.DTO;
namespace DOANQLNH.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance {
            get { if (instance == null) instance = new MenuDAO(); return instance; }
            private set => instance = value;
        }

        private MenuDAO()
        {

        }

        public List<DTO.Menu> GetListMenuByTable(int id)
        {
            List<DTO.Menu> listMenu = new List<DTO.Menu>();
            string query = "SELECT MA.NAME,CTHD.COUNT,MA.GIA,MA.GIA*CTHD.COUNT AS TongTien FROM DBO.CHITIETHOADON AS CTHD,DBO.HOADON AS HD,DBO.MONAN AS MA WHERE CTHD.IDHOADON = HD.ID AND CTHD.IDMONAN = MA.ID AND HD.STATUS = 0 AND HD.IDBAN = " + id;
            DataTable data = DataProvider.Instance.ExecuQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Menu menu = new DTO.Menu(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}

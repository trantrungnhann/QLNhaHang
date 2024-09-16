using DOANQLNH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DAO
{
    public class BilliFDAO
    {
        private static BilliFDAO instance;

        public static BilliFDAO Instance {
            get { if (instance == null) instance = new BilliFDAO(); return instance; }
            private set => instance = value;
        }

        private BilliFDAO() { }

        public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExecuQuery("DELETE DBO.CHITIETHOADON WHERE IDMONAN = " + id);
        }
        public List<BilliFDTO>GetListBillInfo(int id)
        {
            List<BilliFDTO> listBillInfo = new List<BilliFDTO>();
            DataTable data = DataProvider.Instance.ExecuQuery("SELECT * FROM DBO.CHITIETHOADON WHERE IDHOADON = " + id);
            foreach(DataRow item in data.Rows)
            {
                BilliFDTO info = new BilliFDTO(item);
                listBillInfo.Add(info);
            }
            return listBillInfo;
        }
        public void InsertBillInFo(int billID, int foodID, int count)
        {
            string query = "EXEC USP_InsertChiTietHoaDon @IDHOADON , @IDMONAN , @COUNT";
            object[] parameters = new object[] { billID, foodID, count };

            DataProvider.Instance.ExecuNonQuery(query, parameters);
        }

    }
}

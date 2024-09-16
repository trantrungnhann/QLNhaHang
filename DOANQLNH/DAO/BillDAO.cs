using DOANQLNH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            private set => instance = value;
        }
        private BillDAO() { }
        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuQuery("SELECT * FROM DBO.HOADON WHERE IDBAN = " + id + " AND STATUS = 0");
            if (data.Rows.Count > 0)
            {
                BillDTO bill = new BillDTO(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void CheckOut(int id,int discount,float totalPrice)
        {
            string query = "UPDATE DBO.HOADON SET NGAYTRA = GETDATE(), STATUS = 1, " + "DISCOUNT = " + discount + ", TONGTIEN = " + totalPrice + " WHERE id = " + id;
            DataProvider.Instance.ExecuNonQuery(query);

        }
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuNonQuery("EXEC USP_InsertHoaDon @IDBAN", new object[] { id });
        }
        public DataTable GetBillByDate(DateTime checkIn,DateTime checkOut)
        {
           return DataProvider.Instance.ExecuQuery("EXEC USP_GetListBillByDate @NGAYNHAN , @NGAYTRA", new object[] { checkIn, checkOut });
        }
        public DataTable GetBillByDateAndPage(DateTime checkIn, DateTime checkOut, int pageNum)
        {
            return DataProvider.Instance.ExecuQuery("EXEC USP_GetListBillByDateAndPage @NGAYNHAN , @NGAYTRA , @PAGE", new object[] { checkIn, checkOut,pageNum });
        }
        public int GetNumBillByDate(DateTime checkIn, DateTime checkOut)
        {
            return (int)DataProvider.Instance.ExecuScalar("EXEC USP_GetNumBillByDate @NGAYNHAN , @NGAYTRA", new object[] { checkIn, checkOut });
        }
        public int GetMaxIDBill()
        {

            try
            {
                return (int)DataProvider.Instance.ExecuScalar("SELECT MAX(id) FROM DBO.HOADON");
            }
            catch
            {
                return 1;
            }

        }
    }
}

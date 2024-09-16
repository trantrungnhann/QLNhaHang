using DOANQLNH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DAO
{
    class TableDAO
    {
        private static TableDAO instance;

        internal static TableDAO Instance {
            get { if (instance == null) instance = new TableDAO(); return instance; }
            private set => instance = value;
        }

        public static int TableWidth = 100;
        public static int TableHeight = 100;
        private TableDAO() { }

        public void SwitchTable(int id1,int id2)
        {
            DataProvider.Instance.ExecuQuery("USP_SwitchTable @IDBAN1 , @IDBAN2",new object[] { id1,id2});
        }
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuQuery("USP_LayDSBan");

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }
        public bool InsertTable(string name,string status)
        {
            string query = string.Format("INSERT DBO.BAN(NAME,STATUS) VALUES(N'{0}',N'{1}')", name,status);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }
        public bool UpDateTable(string name,string status, int id)
        {
            string query = string.Format("UPDATE DBO.BAN SET NAME = N'{0}', STATUS = N'{1}' WHERE id = {2}", name,status, id);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }

        public bool DeleteTable(int idTable)
        {
            BilliFDAO.Instance.DeleteBillInfoByFoodID(idTable);
            string query = string.Format("DELETE BAN WHERE id = {0}", idTable);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }
    }
}

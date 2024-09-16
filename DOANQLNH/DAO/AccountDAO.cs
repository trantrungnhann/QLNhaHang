using DOANQLNH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set => instance = value;
        }
        private AccountDAO()
        {
        }
        public bool login(string tenTaiKhoan, string matKhau)
        {
            byte[] temp = Encoding.ASCII.GetBytes(matKhau);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }

            //var list = hasData.ToString();
            //list.Reverse();

            string query = "USP_Login @TENTK , @PASSWORD";

            DataTable result = DataProvider.Instance.ExecuQuery(query, new object[] { tenTaiKhoan, hasPass });

            return result.Rows.Count > 0;
        }

        public bool upDateAccount(string userName, string disPlayName,string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuNonQuery("EXEC USP_UpdateAccount @TENTK , @TENHT , @PASSWORD , @NEWPASSWORD",new object[] {userName,disPlayName, pass, newPass });
            return result > 0;
        }
        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuQuery("SELECT TENTK,TENHT,TYPE FROM DBO.TAIKHOAN");
        }
        public AccountDTO GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuQuery("SELECT * FROM TAIKHOAN WHERE TENTK = '" + userName + "'");
            foreach(DataRow item in data.Rows)
            {
                return new AccountDTO(item);
            }
            return null;
        }
        public bool InsertAccount(string name, string disPlayName, int type)
        {
            string query = string.Format("INSERT DBO.TAIKHOAN(TENTK, TENHT, TYPE, PASSWORD) VALUES(N'{0}', (1), {2} , N'{3}')", name, disPlayName, type, "1962026656160185351301320480154111117132155");
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }
        public bool UpDateAccount( string name, string disPlayName, int type)
        {
            string query = string.Format("UPDATE DBO.TAIKHOAN SET TENHT = N'{1}', TYPE = {2} WHERE TENTK = N'{0}'", name, disPlayName, type);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }

        public bool DeleteAccount(string name)
        {
            string query = string.Format("DELETE TAIKHOAN WHERE TENTK = N'{0}'", name);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }
        public bool ResetPassword(string name)
        {

            string query = string.Format("UPDATE TAIKHOAN SET PASSWORD = N'1962026656160185351301320480154111117132155' WHERE TENTK = N'{0}'", name);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }
    }
}

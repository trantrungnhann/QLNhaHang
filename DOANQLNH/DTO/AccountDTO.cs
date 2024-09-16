using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DTO
{
    public class AccountDTO
    {
        public AccountDTO(string userName, string disPlayName, int type,string passWord = null)
        {
            this.UserName = userName;
            this.DisPlayName = disPlayName;
            this.Type = type;
            this.PassWord = passWord;
        }
        public AccountDTO(DataRow row)
        {
            this.UserName = row["TENTK"].ToString();
            this.DisPlayName = row["TENHT"].ToString();
            this.Type = (int)row["TYPE"];
            this.PassWord = row["PASSWORD"].ToString();
        }


        private string userName;
        private string disPlayName;
        private string passWord;
        private int type;

        public string UserName { get => userName; set => userName = value; }
        public string DisPlayName { get => disPlayName; set => disPlayName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public int Type { get => type; set => type = value; }
    }
}

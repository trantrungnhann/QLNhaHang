using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DTO
{
    public class BillDTO
    {
        public BillDTO(int id, DateTime? dataCheckin,DateTime? dataCheckout,int idTable, int status,int discount = 0)
        {
            this.ID = id;
            this.dataCheckIn = dataCheckin;
            this.dataCheckOut = dataCheckout;
            this.IDTable = idTable;
            this.Status = status;
            this.Discount = discount;
        }

        public BillDTO(DataRow row)
        {
            this.ID = (int)row["ID"];
            this.dataCheckIn = (DateTime?)row["NGAYNHAN"];
            var dataCheckOutTemp = row["NGAYTRA"];
            if (dataCheckOutTemp.ToString() != "")
                this.DataCheckOut = (DateTime?)dataCheckOutTemp;
            this.IDTable = (int)row["IDBAN"];
            this.Status = (int)row["STATUS"] ;
            if(row["DISCOUNT"].ToString() != "")
                this.Discount = (int)row["DISCOUNT"];

        }

        private int iD;
        private DateTime? dataCheckIn;
        private DateTime? dataCheckOut;
        private int iDTable;
        private int status;
        private int discount;
        public int ID { get => iD; set => iD = value; }
        public DateTime? DataCheckIn { get => dataCheckIn; set => dataCheckIn = value; }
        public DateTime? DataCheckOut { get => dataCheckOut; set => dataCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int IDTable { get => iDTable; set => iDTable = value; }
        public int Discount { get => discount; set => discount = value; }
    }
}

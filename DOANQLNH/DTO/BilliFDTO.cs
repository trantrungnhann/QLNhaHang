using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DTO
{
    public class BilliFDTO
    {
        public BilliFDTO(int id, int billID,int foofID,int count)
        {
            this.ID = id;
            this.BillID = billID;
            this.FoodID = foofID;
            this.Count = count;
        }
        public BilliFDTO(DataRow row)
        {
            this.ID = (int)row["ID"];
            this.BillID = (int)row["IDHOADON"];
            this.FoodID = (int)row["IDMONAN"];
            this.Count = (int)row["COUNT"];
        }

        private int iD;
        private int billID;
        private int foodID;
        private int count;
        public int ID { get => iD; set => iD = value; }
        public int BillID { get => billID; set => billID = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public int Count { get => count; set => count = value; }
    }
}

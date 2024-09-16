using DOANQLNH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return instance; }
            private set => instance = value;
        }

        private FoodDAO()
        {

        }
        public List<Food> GetListFoodByCategoryID(int id)
        {
            List<Food> listFood = new List<Food>();
            string query = "SELECT *FROM MONAN WHERE IDDM = "+id;
            DataTable data = DataProvider.Instance.ExecuQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }


        public List<Food> GetListFood()
        {
            List<Food> listFood = new List<Food>();
            string query = "SELECT *FROM MONAN";
            DataTable data = DataProvider.Instance.ExecuQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }

        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            string query = string.Format("SELECT *FROM MONAN WHERE NAME like N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExecuQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }

        public bool InsertFood(string name, int id, float price)
        {
            string query = string.Format("INSERT DBO.MONAN(NAME, IDDM, GIA) VALUES(N'{0}', (1), {2})",name,id,price);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }
        public bool UpDateFood(int idFood, string name, int id, float price)
        {
            string query = string.Format("UPDATE DBO.MONAN SET NAME = N'{0}', IDDM = {1} , GIA = {2} WHERE id = {3}", name, id, price,idFood);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }

        public bool DeleteFood(int idFood)
        {
            BilliFDAO.Instance.DeleteBillInfoByFoodID(idFood);
            string query = string.Format("DELETE MONAN WHERE id = {0}",idFood);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }
    }
}

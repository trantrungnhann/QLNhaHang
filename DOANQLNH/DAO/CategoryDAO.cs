using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance {
            get { if (instance == null) instance = new CategoryDAO(); return instance; }
            private set => instance = value;
        }
        private CategoryDAO()
        {

        }
        public List<Category> GetListCategory()
        {
            List<Category> listCategory = new List<Category>();
            string query = "SELECT *FROM DANHMUC";
            DataTable data = DataProvider.Instance.ExecuQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                listCategory.Add(category);
            }
            return listCategory;
        }
        public Category GetCategoryByID(int id)
        {
            Category category = null;

            string query = "SELECT *FROM DANHMUC WHERE id = " + id;
            DataTable data = DataProvider.Instance.ExecuQuery(query);
            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }
            return category;
        }
        public bool InsertCategory(string name)
        {
            string query = string.Format("INSERT DBO.DANHMUC(NAME) VALUES(N'{0}')", name);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }
        public bool UpDateCategory(string name, int id)
        {
            string query = string.Format("UPDATE DBO.DANHMUC SET NAME = N'{0}' WHERE id = {1}", name, id);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }

        public bool DeleteCategory(int idCategory)
        {
            BilliFDAO.Instance.DeleteBillInfoByFoodID(idCategory);
            string query = string.Format("DELETE DANHMUC WHERE id = {0}", idCategory);
            int result = DataProvider.Instance.ExecuNonQuery(query);

            return result > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DOANQLNH.DAO;
using DOANQLNH.DTO;

namespace DOANQLNH
{
    public partial class frm_Admin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public AccountDTO loginAccount;
        public frm_Admin()
        {
            InitializeComponent();
            Load();
            //LoadTaiKhoanList();
            //LoadMonAnList();
        }
        //void LoadMonAnList()
        //{
        //    string query = "SELECT * FROM MONAN";
        //    dgvTK.DataSource = DataProvider.Instance.ExecuQuery(query);
        //}
        //void LoadTaiKhoanList()
        //{

        //    string query = "EXEC DBO.USP_LayTaiKhoanTenTK @TENTK";
        //    dgvTK.DataSource = DataProvider.Instance.ExecuQuery(query,new object[] {"GV"});
        // }
        #region methods
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }
        void Load()
        {
            dgvTA.DataSource = foodList;
            dgvDM.DataSource = categoryList;
            dgvBA.DataSource = tableList;
            dgvTK.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkfromday.Value, dtpktodate.Value);
            LoadListFood();
            LoadCategory();
            LoadTable();
            LoadAccount();
            LoadCategoryIntoCombobox(cbbDMucTA);
            AddFoodBinding();
            AddCategoryBinding();
            AddTableBinding();
            AddAcccountBinding();

        }

        void AddAcccountBinding()
        {
            txtTenTK.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "TENTK",true,DataSourceUpdateMode.Never));
            txtTenHTTK.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "TENHT",true,DataSourceUpdateMode.Never));
            nmLoaiTK.DataBindings.Add(new Binding("Value", dgvTK.DataSource, "TYPE",true,DataSourceUpdateMode.Never));

        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkfromday.Value = new DateTime(today.Year, today.Month, 1);
            dtpktodate.Value = dtpkfromday.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dgvDT.DataSource = BillDAO.Instance.GetBillByDate(checkIn, checkOut);
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        void AddFoodBinding()
        {
            txtTenTA.DataBindings.Add(new Binding("Text", dgvTA.DataSource, "NAME", true, DataSourceUpdateMode.Never));
            txtidTA.DataBindings.Add(new Binding("Text", dgvTA.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmGiaTA.DataBindings.Add(new Binding("Value", dgvTA.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void LoadCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void AddCategoryBinding()
        {
            txtidDM.DataBindings.Add(new Binding("Text", dgvDM.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTenDM.DataBindings.Add(new Binding("Text", dgvDM.DataSource, "NAME", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "NAME";
        }
        void LoadTable()
        {
            tableList.DataSource = TableDAO.Instance.LoadTableList();
        }
        void AddTableBinding()
        {
            txtidBA.DataBindings.Add(new Binding("Text", dgvBA.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtDMBA.DataBindings.Add(new Binding("Text", dgvBA.DataSource, "NAME", true, DataSourceUpdateMode.Never));
            txtTTBA.DataBindings.Add(new Binding("Text", dgvBA.DataSource, "STATUS", true, DataSourceUpdateMode.Never));
        }
        void AddAccount(string userName,string disPlayName,int type)
        {
            if(AccountDAO.Instance.InsertAccount(userName,disPlayName,type))
            {
                MessageBox.Show("Thêm Tài Khoản Thành Công");
            }
            else
            {
                MessageBox.Show("Thêm Tài Khoản Không Thành Công");
            }
            LoadAccount();
        }

        void EditAccount(string userName, string disPlayName, int type)
        {
            if (AccountDAO.Instance.UpDateAccount(userName, disPlayName, type))
            {
                MessageBox.Show("Sửa Tài Khoản Thành Công");
            }
            else
            {
                MessageBox.Show("Sửa Tài Khoản Không Thành Công");
            }
            LoadAccount();
        }
        void DeleteAccount(string userName)
        {
            if(loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Vui Lòng Không Xoá Tài Khoản Của Bạn");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xoá Tài Khoản Thành Công");
            }
            else
            {
                MessageBox.Show("Xoá Tài Khoản Không Thành Công");
            }
            LoadAccount();
        }

        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt Lại Mật Khẩu Thành Công");
            }
            else
            {
                MessageBox.Show("Đặt Lại Mật Khẩu Không Thành Công");
            }
        }

        #endregion

        #region events
        private void btnRSTK_Click(object sender, EventArgs e)
        {
            string userName = txtTenTK.Text;
            ResetPass(userName);
        }
        private void btnXoáTK_Click(object sender, EventArgs e)
        {
            string userName = txtTenTK.Text;
            DeleteAccount(userName);
        }
        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            string userName = txtTenTK.Text;
            string disPlayName = txtTenHTTK.Text;
            int type = (int)nmLoaiTK.Value;

            EditAccount(userName, disPlayName, type);
        }
        private void btnThemTK_Click(object sender, EventArgs e)
        {
            string userName = txtTenTK.Text;
            string disPlayName = txtTenHTTK.Text;
            int type = (int)nmLoaiTK.Value;

            AddAccount(userName, disPlayName, type);
        }
        private void btnXemTK_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }
        private void btnSearchTA_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txtSearchTA.Text);
        }
        private void btnThongKeDT_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkfromday.Value, dtpktodate.Value);
        }
        private void btnXemTA_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txtidTA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvTA.SelectedCells.Count > 0)
                {
                    int id = (int)dgvTA.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbbDMucTA.SelectedItem = category;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbbDMucTA.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbbDMucTA.SelectedIndex = index;

                }
            }
            catch { }

        }

        private void btnThemTA_Click(object sender, EventArgs e)
        {
            string name = txtTenTA.Text;
            int categoryID = (cbbDMucTA.SelectedItem as Category).ID;
            float price = (float)nmGiaTA.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm Thành Công");
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm Không Thành Công");

            }
        }

        private void btnSuaTA_Click(object sender, EventArgs e)
        {
            string name = txtTenTA.Text;
            int categoryID = (cbbDMucTA.SelectedItem as Category).ID;
            float price = (float)nmGiaTA.Value;
            int idfood = Convert.ToInt32(txtidTA.Text);

            if (FoodDAO.Instance.UpDateFood(idfood, name, categoryID, price))
            {
                MessageBox.Show("Sửa Thành Công");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Sửa Không Thành Công");

            }
        }

        private void btnXoaTA_Click(object sender, EventArgs e)
        {
            int idfood = Convert.ToInt32(txtidTA.Text);

            if (FoodDAO.Instance.DeleteFood(idfood))
            {
                MessageBox.Show("Xoá Thành Công");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xoá Không Thành Công");

            }
        }
        private void btnXemDM_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void btnThemDM_Click(object sender, EventArgs e)
        {

            string name = txtTenDM.Text;

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm Thành Công");
                LoadCategory();
                if (insertCategory != null)
                    insertCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm Không Thành Công");

            }
        }

        private void btnXoaDM_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtidDM.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xoá Thành Công");
                LoadCategory();
                if (deleteCategory != null)
                    deleteCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xoá Không Thành Công");

            }
        }

        private void btnSuaDM_Click(object sender, EventArgs e)
        {
            string name = txtTenDM.Text;
            int id = Convert.ToInt32(txtidDM.Text);

            if (CategoryDAO.Instance.UpDateCategory(name, id))
            {
                MessageBox.Show("Sửa Thành Công");
                LoadCategory();
                if (updateCategory != null)
                    updateCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Sửa Không Thành Công");

            }
        }


        private void btnXemBA_Click(object sender, EventArgs e)
        {
            LoadTable();
        }
        private void btnThemBA_Click(object sender, EventArgs e)
        {
            string name = txtDMBA.Text;
            string status = txtTTBA.Text;

            if (TableDAO.Instance.InsertTable(name,status))
            {
                MessageBox.Show("Thêm Thành Công");
                LoadTable();
                if (insertTable != null)
                    insertTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm Không Thành Công");

            }
        }

        private void btnSuaBA_Click(object sender, EventArgs e)
        {
            string name = txtDMBA.Text;
            string status = txtTTBA.Text;
            int id = Convert.ToInt32(txtidBA.Text);

            if (TableDAO.Instance.UpDateTable(name,status, id))
            {
                MessageBox.Show("Sửa Thành Công");
                LoadTable();
                if (updateTable != null)
                    updateTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Sửa Không Thành Công");

            }
        }


        private void btnXoaBA_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtidBA.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xoá Thành Công");
                LoadTable();
                if (deleteTable != null)
                    deleteTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xoá Không Thành Công");

            }
        }

        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }
        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }
        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }

        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }
        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }
        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }



        #endregion

        private void btnDau_Click(object sender, EventArgs e)
        {
            txtPage.Text = "1";
        }

        private void btnCuoi_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillByDate(dtpkfromday.Value, dtpktodate.Value);

            int lastPage = sumRecord / 10;
            if (sumRecord % 10 != 0)
                lastPage++;
            txtPage.Text = lastPage.ToString();
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPage.Text);

            if (page > 1)
                page--;

            txtPage.Text = page.ToString();
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPage.Text);
            int sumRecord = BillDAO.Instance.GetNumBillByDate(dtpkfromday.Value, dtpktodate.Value);
            if (page < sumRecord)
                page++;

            txtPage.Text = page.ToString();
        }

        private void txtPage_TextChanged(object sender, EventArgs e)
        {
            dgvDT.DataSource = BillDAO.Instance.GetBillByDateAndPage(dtpkfromday.Value, dtpktodate.Value, Convert.ToInt32(txtPage.Text));
        }

        private void tpDM_Click(object sender, EventArgs e)
        {

        }
    }
}

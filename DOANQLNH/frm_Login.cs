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
    public partial class frm_Login : Form
    {
        public frm_Login()
        {
            InitializeComponent();
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {

        }

        private void frm_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn đóng chương trình","Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenDN.Text;
            string matKhau = txtMK.Text;
            if (login(tenTaiKhoan,matKhau))
            {
                AccountDTO loginAccount = AccountDAO.Instance.GetAccountByUserName(tenTaiKhoan);
                frm_TableM f = new frm_TableM(loginAccount);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai Tên Tài Khoản hoặc Mật Khẩu");
            }

        }
        bool login(string tenTaiKhoan,string matKhau)
        {
            return AccountDAO.Instance.login(tenTaiKhoan,matKhau);
        }
        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTenDN_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnexit_Click_1(object sender, EventArgs e)
        {

        }
    }
}

using DOANQLNH.DAO;
using DOANQLNH.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOANQLNH
{
    public partial class frm_AccountProfile : Form
    {
        private AccountDTO loginAccount;

        public AccountDTO LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; changeAccount(loginAccount); }
        }
        public frm_AccountProfile(AccountDTO acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }
        void changeAccount(AccountDTO acc)
        {
            txtTenDN.Text = LoginAccount.UserName;
            txtTenHT.Text = LoginAccount.DisPlayName;
        }
        void upDateAccount()
        {
            string disPlayName = txtTenHT.Text;
            string passWord = txtMK.Text;
            string newPass = txtMKN.Text;
            string rePass = txtNLMK.Text;
            string userName = txtTenDN.Text;

            if(!newPass.Equals(rePass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới");
            }
            else
            {
                if(AccountDAO.Instance.upDateAccount(userName,disPlayName,passWord,newPass))
                {
                    MessageBox.Show("Cập Nhật Thành Công");
                    if (UpdateAccount != null)
                        UpdateAccount(this, new AccountEvent( AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu");
                }
            }
        }
        private event EventHandler<AccountEvent> UpdateAccount;
        public event EventHandler<AccountEvent> UpDateAccount
        {
            add { UpdateAccount += value; }
            remove { UpdateAccount -= value; }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            upDateAccount();
        }
    }
    public class AccountEvent:EventArgs
    {
        private AccountDTO acc;

        public AccountDTO Acc { get => acc; set => acc = value; }
        public AccountEvent(AccountDTO acc)
        {
            this.Acc = acc;
        }
    }
}

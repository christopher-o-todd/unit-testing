using Banking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingGUI
{
    public partial class Form1 : Form
    {
        List<Account> accounts = new List<Account>(); // empty list
        public Form1()
        {
            InitializeComponent();
        }

        // create new account and add to the list
        private void btnCreate_Click(object sender, EventArgs e)
        {
            // get initial balance (skipping validation)
            decimal initBal = Convert.ToDecimal(txtInitialBalance.Text);
            // ceate an account and add to the list
            Account newAcct = new Account(initBal);
            accounts.Add(newAcct);
            DisplayAccounts();
        }

        private void DisplayAccounts()
        {
            lstAccounts.Items.Clear();
            foreach(Account acct in accounts)
            {
                lstAccounts.Items.Add(acct);
            }
        }

        private decimal GetAmount()
        {
            decimal amount = 0;
            // skipping validation
            amount = Convert.ToDecimal(txtAmount.Text);
            return amount;
        }

        private Account GetSelectedAccount()
        {
            Account selectedAcct = null;
            int selectedIndex = lstAccounts.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("You need to select account from the list");
            }
            else
            {
                selectedIndex = lstAccounts.SelectedIndex;
                selectedAcct = accounts[selectedIndex];
            }
            return selectedAcct;
        }
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            Account acct = GetSelectedAccount();
            decimal amount = 0;
            if(acct != null)// account selected
            {
                amount = GetAmount();
                acct.Deposit(amount);
                DisplayAccounts();
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            Account acct = GetSelectedAccount();
            decimal amount = 0;
            bool result;
            if(acct != null) // account selected
            {
                amount = GetAmount();
                result = acct.Withdraw(amount);
                if(result == false) // NSF
                {
                    MessageBox.Show("Cannot withdraw more than balance",
                        "NSF Error");
                }
                DisplayAccounts();
            }
        }
    }
}

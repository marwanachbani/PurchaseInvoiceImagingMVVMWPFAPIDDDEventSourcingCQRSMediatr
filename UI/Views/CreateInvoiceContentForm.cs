using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIApi;

namespace UI.Views
{
    public partial class CreateInvoiceContentForm : Form
    {
        private readonly ApiHelper _api = new ApiHelper();
        public event EventHandler InvoiceContentcreated;
        
        private string _token;
        private string _userName;

        public string Token { get => _token; set => _token = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public CreateInvoiceContentForm(string token,string userName)
        {
            InitializeComponent();
            _token = token;
            _userName = userName;
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            var command = await _api.CreateInvoiceContent(_token, dateTimePicker1.Value,_userName);
            if(command.Successed==true)
            {
                InvoiceContentcreated?.Invoke(this, EventArgs.Empty);
                this.Close();
            }

            label3.Text = command.Respnse;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

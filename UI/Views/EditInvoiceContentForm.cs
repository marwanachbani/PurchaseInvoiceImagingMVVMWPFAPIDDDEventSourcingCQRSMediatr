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
    public partial class EditInvoiceContentForm : Form
    {
        private string _token;
        private string _invoiceContent;
        private string _userName;
        private readonly ApiHelper _api = new ApiHelper();
        public event EventHandler InvoiceContentModified;

        public string Token { get => _token; set => _token = value; }
        public string InvoiceContent { get => _invoiceContent; set => _invoiceContent = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public EditInvoiceContentForm(string token , string invoiceContent , string userName)
        {
            _token = token;
            _invoiceContent = invoiceContent;
            _userName = userName;
            InitializeComponent();
            LoadInvoiceContent();
        }
        private void LoadInvoiceContent()
        {
            label4.Text = _invoiceContent;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var command = await _api.EditInvoiceContent(_token, _invoiceContent, dateTimePicker1.Value, _userName);
            if (command.Successed == true)
            {
                InvoiceContentModified?.Invoke(this, EventArgs.Empty);
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

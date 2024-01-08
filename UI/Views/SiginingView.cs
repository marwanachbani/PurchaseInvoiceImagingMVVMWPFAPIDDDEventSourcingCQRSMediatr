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
    public partial class SiginingView : UserControl
    {
        private readonly ApiHelper _apiHelper = new ApiHelper();
        PaperPusherView pusherView;
        
        public SiginingView()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var command = await _apiHelper.Login(textBox1.Text, textBox2.Text);
            Form1 form1 = new Form1();
            if (command.Successed==true)
            {
                pusherView = new PaperPusherView(command.Respnse,textBox1.Text);
                
                
                this.Controls.Remove(this);
                this.Controls.Add(pusherView);
                panel1.Hide();
                panel2.Hide();
                button1.Hide();
                textBox1.Hide();
                textBox2.Hide();
                pusherView.Show();

            }
            if(command.Successed==false) {
                label1.Text = command.Respnse;
                await Task.CompletedTask;
            }
            
        }
    }
}

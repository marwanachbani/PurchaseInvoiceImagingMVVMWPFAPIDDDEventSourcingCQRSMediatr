using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Views;

namespace UI
{
    public partial class Form1 : Form
    {
       
        
        private SiginingView siginingView = new SiginingView();
  
        private bool isDragging = false;
        private Point lastCursorPosition;


        public Form1()
        {
            InitializeComponent();
            this.MinimumSize = new Size(800, 600); // Replace with your desired size
            this.MaximumSize = new Size(800, 600);
            this.Scale(SizeF.Empty);
            panel3.Controls.Add(siginingView);
           
            
           
            siginingView.Show();
            
        }
        

        
        
        

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPosition = e.Location;
            }

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = e.Location.X - lastCursorPosition.X;
                int deltaY = e.Location.Y - lastCursorPosition.Y;

                // Update the form's location based on the mouse movement
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);

                lastCursorPosition = e.Location;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}

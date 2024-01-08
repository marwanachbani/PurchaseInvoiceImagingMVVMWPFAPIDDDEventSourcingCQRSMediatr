using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Twain;
using UIApi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace UI.Views
{
    public partial class PaperPusherView : UserControl
    {
        private string _token;
        private readonly ApiHelper _api=  new ApiHelper();
        private readonly TwainHelper _twainHelper = new TwainHelper();
        
        private CreateInvoiceContentForm _form;
        private EditInvoiceContentForm _editForm;
        private DeleteInvoiceContent _deleteForm;
        private string _userName;

        public string Token { get => _token; set => _token = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public PaperPusherView(string token,string userName)
        {
            
            _token = token;
            _userName = userName;
            _twainHelper.MessageSent += CreateInvoice_whenIsScanned;
            _twainHelper.ScanFinished += CreateInvoice_whenScanFinished;
            InitializeComponent();
            GetDataToThree();
            GetScannerList();
            this.MinimumSize = new Size(800, 690); // Replace with your desired size
            this.MaximumSize = new Size(800, 590);
            treeView1.ImageList = imageList1;
            button1.Enabled = false;
            button2.Enabled = false;
            button7.Enabled = false;
            button6.Enabled = false;
            


        }

        public async void GetDataToThree()
        {
            
            treeView1.Nodes.Clear();
            var list = await _api.GelAllForPeperPusher(_token);
            foreach (var item in list)
            {


                TreeNode parentNode = treeView1.Nodes.Add(item.Content);

                foreach (var child in item.Invoices)
                {
                    parentNode.Nodes.Add(child );
                }

            }
           
        }
        public  async Task GetDatatoTree()
        {
             treeView1.Nodes.Clear();
            var list = await _api.GelAllForPeperPusher(_token);
            foreach (var item in list)
            {

                
                TreeNode parentNode = treeView1.Nodes.Add(item.Content);
                
                parentNode.Expand();

                foreach (var child in item.Invoices)
                {
                    TreeNode _item = parentNode.Nodes.Add(child);
                    
                 _item.ImageIndex = 1;
                _item.SelectedImageIndex = 1;
                 parentNode.ImageIndex = 0;
                    _item.Expand();
                }

            }
            treeView1.ExpandAll();

        }
        private async void CreateInvoiceContent_InvoiceContentCreated(object sender, EventArgs e)
        {
            await GetDatatoTree();
        }
        private async void CreateInvoiceContent_InvoiceContentModified(object sender, EventArgs e)
        {
            await GetDatatoTree();
        }
        private async void CreateInvoiceContent_InvoiceContentDeleted(object sender, EventArgs e)
        {
            await GetDatatoTree();
        }
        private async void CreateInvoice_whenIsScanned(object sender, MessageByte e)
        {
            await _api.AddInvoice(_token, e.Picture,e.Content,_userName);
            
        }
        private async void CreateInvoice_whenScanFinished(object sender, MessageByte e)
        {
            await GetDatatoTree(); 

        }


        private void button5_Click(object sender, EventArgs e)
        {
            _form = new CreateInvoiceContentForm(_token,_userName);
            _form.InvoiceContentcreated += CreateInvoiceContent_InvoiceContentCreated;
            _form.ShowDialog();
        }

        

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if(e.Button == MouseButtons.Right) 
            {
                TreeNode selectedNode = treeView1.GetNodeAt(e.X, e.Y);

                if (selectedNode != null)
                {
                    if (selectedNode.Parent == null)
                    {
                        // Parent node: show the parent context menu strip
                        contextMenuStrip1.Show(treeView1, e.Location);
                    }
                    else
                    {
                        // Child node: show the child context menu strip
                        contextMenuStrip2.Show(treeView1, e.Location);
                    }

                    // Set the selected node as the context menu's Tag (optional but useful for handling menu item actions)
                    contextMenuStrip1.Tag = selectedNode;
                }
                
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            TreeNode selectedNode = treeView1.SelectedNode;
            if(selectedNode!=null)
            {
                _editForm = new EditInvoiceContentForm(_token, selectedNode.Text, _userName);
                _editForm.InvoiceContentModified += CreateInvoiceContent_InvoiceContentModified;
                _editForm.ShowDialog();
            }
            
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if(selectedNode!=null )
            {
                _deleteForm = new DeleteInvoiceContent(_token, selectedNode.Text, _userName);
                _deleteForm.InvoiceContentDeleted += CreateInvoiceContent_InvoiceContentDeleted;
                _deleteForm.ShowDialog();
            }
            
        }
        private async void GetScannerList()
        {
           var list = await _twainHelper.GetScanners();
           comboBox1.DataSource = list;
        }

        private async void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                await _twainHelper.Scan(comboBox1.SelectedItem.ToString(), selectedNode.Text);
            }
            
        }

        private async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            var picture = await _api.GelImage(Token, selectedNode.Text);
            if (picture == null)
            {
                pictureBox3.Image = null;
            }
            if (selectedNode != null && picture != null)
                {

                    using (MemoryStream ms = new MemoryStream(picture.Picture))
                    {
                        Image image = Image.FromStream(ms);

                        // Assign the Image to the PictureBox
                        pictureBox3.Image = image;
                    }
                }
        }
    }
}

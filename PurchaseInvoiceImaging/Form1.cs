using GdPicture14;
namespace PurchaseInvoiceImaging
{
    public partial class Form1 : Form
    {
        GdViewer viewerImage = new GdViewer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            viewerImage.AutoSize = true;
            viewerImage.BackColor = Color.Gold;
        }
    }
}
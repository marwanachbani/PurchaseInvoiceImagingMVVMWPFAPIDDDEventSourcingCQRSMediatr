﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;

namespace WpfApp1.Views.InfoZoneViews
{
    /// <summary>
    /// Interaction logic for CreateNewContent.xaml
    /// </summary>
    public partial class CreateNewContent : UserControl
    {
        public CreateNewContent()
        {
            
            InitializeComponent();
            DataContext = new CreateNewContentVM();
        }
    }
}
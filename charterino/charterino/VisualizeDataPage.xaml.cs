﻿using charterino_bo.model;
using System;
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

namespace charterino
{
    /// <summary>
    /// Interaction logic for VisualizeDataPage.xaml
    /// </summary>
    public partial class VisualizeDataPage : Page
    {
        List<Product> data = new List<Product>();

        public VisualizeDataPage()
        {
            InitializeComponent();
        }
    }
}

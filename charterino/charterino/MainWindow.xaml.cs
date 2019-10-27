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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnImportDataClicked(object sender, MouseButtonEventArgs e)
        {
            _mainFrame.Navigate(new ImportDataPage());
        }

        private void OnManageDataClicked(object sender, MouseButtonEventArgs e)
        {
            _mainFrame.Navigate(new ManageDataPage());
        }

        private void OnVisualizeDataClicked(object sender, MouseButtonEventArgs e)
        {
            _mainFrame.Navigate(new VisualizeDataPage());
        }
    }
}

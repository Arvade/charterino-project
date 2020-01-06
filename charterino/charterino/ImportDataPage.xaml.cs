using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using charterino_bo.import;
using charterino_bo.model;
using charterino_bo.repository;
using Microsoft.Win32;

namespace charterino {
    /// <summary>
    /// Interaction logic for ImportDataPage.xaml
    /// </summary>
    public partial class ImportDataPage : Page {
        private ProductRepository _productRepository;
        private FileImportData<Product> fileImportData;

        public ImportDataPage(ProductRepository productRepository, FileImportData<Product> fileImportData) {
            this._productRepository = productRepository;
            this.fileImportData = fileImportData;
            InitializeComponent();
        }

        public void clearLogs() {
            LogContent.Children.Clear();
        }

        private void selectFile(object sender, RoutedEventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Supported data files |*.json;*.xlxs;*.csv";
            bool? result = dialog.ShowDialog();

            if (result.HasValue && result.Value) {
                SelectedFileField.Text = dialog.FileName;
                CurrentlyLoadedFilename.Text = dialog.SafeFileName;

                using (StreamReader reader = new StreamReader(dialog.FileName)) {
                    List<Product> data2 = fileImportData.Import(reader);
                    data2.ForEach(row => logSingleRow(row));
                    _productRepository.setProducts(data2);
                }
            }
        }

        private void logSingleRow(Product row) {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = row.ToString();
            LogContent.Children.Add(textBlock);
        }
    }
}
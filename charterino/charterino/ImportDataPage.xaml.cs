using System;
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
        private readonly ProductRepository _productRepository;
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
            clearLogs();
            OpenFileDialog dialog = new OpenFileDialog {Filter = "Supported data files |*.json;*.xlxs;*.csv"};
            bool? result = dialog.ShowDialog();

            if (!result.HasValue || !result.Value) return;
            using (StreamReader reader = new StreamReader(dialog.FileName)) {
                try
                {
                    List<Product> data2 = fileImportData.Import(reader);
                    data2.ForEach(row => logSingleRow(row));
                    _productRepository.setProducts(data2);
                    SelectedFileField.Text = dialog.FileName;
                    CurrentlyLoadedFilename.Text = dialog.SafeFileName;
                }catch(Exception ex) 
                {
                    clearLogs();
                    logText("Incorrect file");
                    SelectedFileField.Text = "";
                    CurrentlyLoadedFilename.Text = "";
                }
            }
        }

        private void logSingleRow(Product row) {
            logText(row.ToString());
        }

        private void logText(string text)
        { 
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            LogContent.Children.Add(textBlock);
        }
    }
}
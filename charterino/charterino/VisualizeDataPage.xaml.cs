using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using charterino_bo.chart;
using charterino_bo.model;
using charterino_bo.repository;
using LiveCharts;

namespace charterino {
    /// <summary>
    /// Interaction logic for VisualizeDataPage.xaml
    /// </summary>
    public partial class VisualizeDataPage : Page {
        public SeriesCollection PieChartCollection { get; set; }
        public SeriesCollection BarChartCollection { get; set; }
        public string[] BarLabels;
        public Func<int, string> BarFormatter;
        public SeriesCollection StackedChartCollection { get; set; }
        public Func<double, string> StackedFormatter;

        readonly SolidColorBrush lightGreen = (SolidColorBrush) new BrushConverter().ConvertFrom("#FF389484");
        readonly SolidColorBrush darkGreen = (SolidColorBrush) new BrushConverter().ConvertFrom("#FF246459");

        Charts Charts;
        List<Product> productList;
        ProductRepository _productRepository;

        public VisualizeDataPage(ProductRepository productRepository, Charts charts) {
            Charts = charts;
            _productRepository = productRepository;
            InitializeComponent();

            if (productRepository.getProducts() == null || productRepository.getProducts().Count == 0) {
                dataWarning.Visibility = Visibility.Visible;
                ChartBar.Visibility = Visibility.Hidden;
                ChartPie.Visibility = Visibility.Hidden;
                ChartStacked.Visibility = Visibility.Hidden;
            }
            else {
                dataWarning.Visibility = Visibility.Hidden;

                InstantiateBarChart();
                InstantiatePieChart();
                InstantiateStackedChart();

                ChartBarButton.Click += (s, e) => ShowBarChart();
                ChartStackedButton.Click += (s, e) => ShowStackedChart();
                ChartPieButton.Click += (s, e) => ShowPieChart();
                ShowBarChart();
            }
        }

        void InstantiateBarChart() {
            productList = _productRepository.getProducts();
            string[] distinctCategories = Charts.DistinctCategories(productList);
            BarChartCollection = Charts.GenerateBarChart(productList, distinctCategories, out BarLabels, out BarFormatter);
            DataContext = this;
        }

        void InstantiatePieChart() {
            productList = _productRepository.getProducts();
            string[] distinctCategories = Charts.DistinctCategories(productList);
            PieChartCollection = Charts.GeneratePieChart(productList, distinctCategories);
            DataContext = this;
        }

        void InstantiateStackedChart() {
            productList = _productRepository.getProducts();
            string[] distinctCategories = Charts.DistinctCategories(productList);
            StackedChartCollection = Charts.GenerateStackedChart(productList, distinctCategories, out StackedFormatter);
            DataContext = this;
        }

        void ShowBarChart() {
            InstantiateBarChart();
            ChartPie.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Hidden;
            ChartBar.Visibility = Visibility.Visible;
            ChangeButtonColor(ChartBarButton);
        }

        void ShowPieChart() {
            InstantiatePieChart();
            ChartPie.Visibility = Visibility.Visible;
            ChartStacked.Visibility = Visibility.Hidden;
            ChartBar.Visibility = Visibility.Hidden;
            ChangeButtonColor(ChartPieButton);
        }

        void ShowStackedChart() {
            InstantiateStackedChart();
            ChartPie.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Visible;
            ChartBar.Visibility = Visibility.Hidden;
            ChangeButtonColor(ChartStackedButton);
        }

        public void ChangeButtonColor(Button button) {
            ChartPieButton.Background = lightGreen;
            ChartStackedButton.Background = lightGreen;
            ChartBarButton.Background = lightGreen;
            button.Background = darkGreen;
        }
    }
}
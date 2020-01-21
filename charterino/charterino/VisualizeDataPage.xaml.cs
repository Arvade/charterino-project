using charterino_bo.model;
using charterino_bo.chart;
using LiveCharts.Wpf;
using LiveCharts;
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
using LiveCharts.Defaults;
using charterino_bo.repository;

namespace charterino {
    /// <summary>
    /// Interaction logic for VisualizeDataPage.xaml
    /// </summary>
    public partial class VisualizeDataPage : Page
    {
        public Func<ChartPoint, string> PiePointLabel { get; set; }
        public SeriesCollection PieChartCollection { get; set; }
        public SeriesCollection BarChartCollection { get; set; }
        public string[] BarLabels;
        public Func<int, string> BarFormatter;
        public SeriesCollection StackedChartCollection { get; set; }
        public Func<double, string> StackedFormatter;

        string[] distinctCategories;
        readonly SolidColorBrush lightGreen = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF389484");
        readonly SolidColorBrush darkGreen = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF246459");

        Charts Charts;
        List<Product> productRepository;
        ProductRepository _productRepository;

        public VisualizeDataPage(ProductRepository productRepository)
        {

            InitializeComponent();
            Charts = new Charts();

            if (productRepository.getProducts() == null || productRepository.getProducts().Count == 0)
            {
                dataWarning.Visibility = Visibility.Visible;
                ChartBar.Visibility = Visibility.Hidden;
                ChartPie.Visibility = Visibility.Hidden;
                ChartStacked.Visibility = Visibility.Hidden;
            }
            else
            {
                dataWarning.Visibility = Visibility.Hidden;
                _productRepository = productRepository;

                InstantiateBarChart();
                InstantiatePieChart();
                InstantiateStackedChart();

                ChartBarButton.Click += (s, e) => ShowBarChart();
                ChartStackedButton.Click += (s, e) => ShowStackedChart();
                ChartPieButton.Click += (s, e) => ShowPieChart();
                ShowBarChart();
            }
        }

        void InstantiateBarChart()
        {
            productRepository = _productRepository.getProducts();
            distinctCategories = Charts.DistinctCategories(productRepository);
            BarChartCollection = Charts.GenerateBarChart(productRepository, distinctCategories, out BarLabels, out BarFormatter);
            DataContext = this;
        }

        void InstantiatePieChart()
        {
            productRepository = _productRepository.getProducts();
            distinctCategories = Charts.DistinctCategories(productRepository);
            PieChartCollection = Charts.GeneratePieChart(productRepository, distinctCategories);
            DataContext = this;
        }

        void InstantiateStackedChart()
        {
            productRepository = _productRepository.getProducts();
            distinctCategories = Charts.DistinctCategories(productRepository);
            StackedChartCollection = Charts.GenerateStackedChart(productRepository, distinctCategories, out StackedFormatter);
            DataContext = this;
        }

        void ShowBarChart()
        {
            InstantiateBarChart();
            ChartPie.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Hidden;
            ChartBar.Visibility = Visibility.Visible;
            ChangeButtonColor(ChartBarButton);
        }

        void ShowPieChart()
        {
            InstantiatePieChart();
            ChartBar.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Hidden;
            ChartPie.Visibility = Visibility.Visible;
            ChangeButtonColor(ChartPieButton);
        }

        void ShowStackedChart()
        {
            InstantiateStackedChart(); InstantiateStackedChart();
            ChartBar.Visibility = Visibility.Hidden;
            ChartPie.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Visible;
            ChangeButtonColor(ChartStackedButton);
        }

        public void ChangeButtonColor(Button button)
        {
            ChartBarButton.Background = lightGreen;
            ChartStackedButton.Background = lightGreen;
            ChartPieButton.Background = lightGreen;

            button.Background = darkGreen;
        }
    }
}
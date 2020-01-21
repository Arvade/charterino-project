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

namespace charterino
{
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
        //TODO: remove
        List<Product> dataMock = new List<Product>();

        public VisualizeDataPage()
        {
            //TODO: remove
            dataMock.Add(new Product(1, "Rudy", "humans", 2.52, 10));
            dataMock.Add(new Product(2, "Gothic 5", "games", 3.52, 12));
            dataMock.Add(new Product(3, "Badocha", "humans", 5.52, 15));
            dataMock.Add(new Product(4, "Książka", "games", 1.30, 36));
            dataMock.Add(new Product(5, "Kubek", "accessories", 9.30, 11));
            dataMock.Add(new Product(6, "Coca Cola", "drinks", 1.92, 19));
            dataMock.Add(new Product(4, "długopis", "games", 1.30, 36));
            dataMock.Add(new Product(5, "Zestaw Wildeców", "accessories", 9.30, 11));
            dataMock.Add(new Product(6, "Sprite", "drinks", 1.92, 19));
            dataMock.Add(new Product(4, "Piłka", "games", 1.30, 36));
            dataMock.Add(new Product(5, "Szklanka", "accessories", 9.30, 11));
            dataMock.Add(new Product(6, "Pepsi", "drinks", 1.92, 19));

            InitializeComponent();

            //TODO: move to main window (from B.Ptak to S.M.)
            Charts = new Charts();

            distinctCategories = Charts.DistinctCategories(dataMock);

            ChartBarButton.Click += (s, e) => ShowBarChart();
            ChartStackedButton.Click += (s, e) => ShowStackedChart();
            ChartPieButton.Click += (s, e) => ShowPieChart();

            InstantiateBarChart();
            InstantiatePieChart();
            InstantiateStackedChart();

            ShowBarChart();
        }

        void InstantiateBarChart()
        {
            BarChartCollection = Charts.GenerateBarChart(dataMock, distinctCategories, out BarLabels, out BarFormatter);
            DataContext = this;
        }
        void InstantiatePieChart()
        {
            PieChartCollection = Charts.GeneratePieChart(dataMock, distinctCategories);
            DataContext = this;
        }
        void InstantiateStackedChart()
        {
            StackedChartCollection = Charts.GenerateStackedChart(dataMock, distinctCategories, out StackedFormatter);
            DataContext = this;
        }

        void ShowBarChart()
        {
            ChartPie.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Hidden;
            ChartBar.Visibility = Visibility.Visible;
            ChangeButtonColor(ChartBarButton);
        }

        void ShowPieChart()
        {
            ChartBar.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Hidden;
            ChartPie.Visibility = Visibility.Visible;
            ChangeButtonColor(ChartPieButton);
        }

        void ShowStackedChart()
        {
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
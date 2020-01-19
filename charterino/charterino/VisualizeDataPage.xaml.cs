using charterino_bo.model;
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
        List<Product> dataMock = new List<Product>();

        public Func<ChartPoint, string> PiePointLabel { get; set; }

        //Pie Chart
        public SeriesCollection PieChartCollection { get; set; }

        //Bar Chart
        public SeriesCollection BarChartCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<int, string> BarFormatter { get; set; }

        //Stacked Chart
        public SeriesCollection StackedChartCollection { get; set; }
        public Func<double, string> StackedFormatter { get; set; }

        string[] distinctCategories;

        public VisualizeDataPage()
        {
            InitializeComponent();

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

            distinctCategories = dataMock.Select(product => product.category).Distinct().ToArray();

            CreateBarChart();
            CreatePieChart();
            CreateStackedChart();

            ChartBar.Visibility = Visibility.Hidden;
            ChartPie.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Hidden;

            ShowBarChart();

            ChartBarButton.Click += (s, e) => ShowBarChart();
            ChartStackedButton.Click += (s, e) => ShowStackedChart();
            ChartPieButton.Click += (s, e) => ShowPieChart();
        }

        public void CreateBarChart()
        {
            BarChartCollection = new SeriesCollection();

            for (int i = 0; i < dataMock.Count; i++)
            {
                BarChartCollection.Add(new StackedColumnSeries
                {
                    Title = dataMock[i].name,
                    Values = new ChartValues<int> {},
                });
                int index = Array.FindIndex(distinctCategories, x => x == dataMock[i].category);
                for (int j = 0; j < index+1; j++)
                {
                    if (j == index)
                        BarChartCollection[i].Values.Add(dataMock[i].sold);
                    else
                        BarChartCollection[i].Values.Add(0);
                }
            }

            BarLabels = dataMock.Select(product => product.category).Distinct().ToArray();
            BarFormatter = value => value.ToString();
            DataContext = this;
        }

        public void CreateStackedChart()
        {
            StackedChartCollection = new SeriesCollection();

            for (int i = 0; i < distinctCategories.Length; i++)
            {
                double income = 0;

                foreach (var product in dataMock)
                {
                    if (product.category == distinctCategories[i])
                        income += dataMock[i].sold * dataMock[i].price;
                }

                StackedChartCollection.Add(new RowSeries 
                { 
                    Title = distinctCategories[i],
                    Values = new ChartValues<double>
                    {
                        income
                    },
                });
            }

            StackedFormatter = val => val.ToString("N") + " $";

            DataContext = this;
        }

        public void CreatePieChart()
        {
            PieChartCollection = new SeriesCollection();

            for (int i = 0; i < distinctCategories.Length; i++)
            {
                int productAmount = 0;

                foreach (var product in dataMock)
                {
                    if (product.category == distinctCategories[i])
                        productAmount++;
                }

                PieChartCollection.Add(new PieSeries
                {
                    Title = productAmount + " different " + distinctCategories[i],
                    Values = new ChartValues<int> {
                        productAmount
                    }
                });
            }

            DataContext = this;
        }

        public void ShowBarChart()
        {
            ChartBar.Visibility = Visibility.Visible;
            ChartPie.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Hidden;
        }

        public void ShowPieChart()
        {
            ChartBar.Visibility = Visibility.Hidden;
            ChartPie.Visibility = Visibility.Visible;
            ChartStacked.Visibility = Visibility.Hidden;
        }

        public void ShowStackedChart()
        {
            ChartBar.Visibility = Visibility.Hidden;
            ChartPie.Visibility = Visibility.Hidden;
            ChartStacked.Visibility = Visibility.Visible;
        }
    }
}
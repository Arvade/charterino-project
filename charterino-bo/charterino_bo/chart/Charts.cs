using charterino_bo.model;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace charterino_bo.chart
{
    public class Charts
    {
        public SeriesCollection GenerateBarChart(List<Product> data, string[] distinctCategories, out string[] BarLabels, out Func<int, string> BarFormatter)
        {
            SeriesCollection BarChartCollection = new SeriesCollection();

            for (int i = 0; i < data.Count; i++)
            {
                BarChartCollection.Add(new StackedColumnSeries
                {
                    Title = data[i].name,
                    Values = new ChartValues<int> { },
                });
                int indexCategory = Array.FindIndex(distinctCategories, x => x == data[i].category);
                for (int j = 0; j <= indexCategory; j++)
                {
                    if (j == indexCategory)
                        BarChartCollection[i].Values.Add(data[i].sold);
                    else
                        BarChartCollection[i].Values.Add(0);
                }
            }

            BarLabels = distinctCategories;
            BarFormatter = value => value.ToString();

            return BarChartCollection;
        }

        public SeriesCollection GenerateStackedChart(List<Product> data, string[] distinctCategories, out Func<double, string> StackedFormatter)
        {
            SeriesCollection StackedChartCollection = new SeriesCollection();

            for (int i = 0; i < distinctCategories.Length; i++)
            {
                double income = 0;

                foreach (var product in data)
                {
                    if (product.category == distinctCategories[i])
                        income += data[i].sold * data[i].price;
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

            StackedFormatter = val => (val.ToString("N") + " $");

            return StackedChartCollection;
        }

        public SeriesCollection GeneratePieChart(List<Product> data, string[] distinctCategories)
        {
            SeriesCollection PieChartCollection = new SeriesCollection();

            for (int i = 0; i < distinctCategories.Length; i++)
            {
                int productAmount = 0;

                foreach (var product in data)
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

            return PieChartCollection;
        }

        public string[] DistinctCategories(List<Product> data)
        {
            return data.Select(product => product.category).Distinct().ToArray();
        }
    }
}

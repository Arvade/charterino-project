using System.Collections.Generic;
using System.Windows.Controls;
using charterino_bo.model;
using charterino_bo.repository;

namespace charterino {
    /// <summary>
    /// Interaction logic for VisualizeDataPage.xaml
    /// </summary>
    public partial class VisualizeDataPage : Page {
        List<Product> dataMock = new List<Product>();
        private ProductRepository _productRepository;

        public VisualizeDataPage(ProductRepository productRepository) {
            this._productRepository = productRepository;

            dataMock.Add(new Product(1, "Cool product1", 2.52, 10));
            dataMock.Add(new Product(2, "Cool product2", 3.52, 12));
            dataMock.Add(new Product(3, "Cool product3", 5.52, 15));
            dataMock.Add(new Product(4, "Cool product4", 1.30, 156));
            dataMock.Add(new Product(5, "Cool product5", 9.30, 11));
            dataMock.Add(new Product(6, "Cool product6", 1.92, 19));

            InitializeComponent();
        }
    }
}
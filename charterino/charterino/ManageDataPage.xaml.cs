using System.Windows.Controls;
using charterino_bo.repository;

namespace charterino {
    /// <summary>
    /// Interaction logic for ManageDataPage.xaml
    /// </summary>
    public partial class ManageDataPage : Page {
        private ProductRepository _productRepository;

        public ManageDataPage(ProductRepository productRepository) {
            this._productRepository = productRepository;
            InitializeComponent();
        }
    }
}
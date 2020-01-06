using System.Collections.Generic;
using charterino_bo.model;

namespace charterino_bo.repository {
    public class ProductRepository {
        
        private List<Product> products;

        public void setProducts(List<Product> products) {
            this.products = products;
        }

        public List<Product> getProducts() {
            return this.products;
        }

        public Product findProductById(long id) {
            return products.Find(product => product.id == id);
        }
    }
}
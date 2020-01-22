using System.Collections.Generic;
using charterino_bo.model;

namespace charterino_bo.repository {
    /// <summary>
    /// Repository for Product 
    /// </summary>
    public class ProductRepository {
        private List<Product> products;

        /// <summary>
        /// Sets products 
        /// </summary>
        /// <param name="products">List of products</param>
        public void setProducts(List<Product> products) {
            this.products = products;
        }

        /// <summary>
        /// Get products
        /// </summary>
        /// <returns>List of products</returns>
        public List<Product> getProducts() {
            return this.products;
        }

        /// <summary>
        /// Finds product by id
        /// </summary>
        /// <param name="id">id of Product</param>
        /// <returns>Product or null</returns>
        public Product findProductById(long id) {
            return products.Find(product => product.id == id);
        }
    }
}
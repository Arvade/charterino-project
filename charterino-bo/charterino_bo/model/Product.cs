namespace charterino_bo.model {
    public class Product {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public int sold { get; set; }
        public Product(int id, string name, double price, int sold) {

            this.id = id;
            this.name = name;
            this.category = category;
            this.price = price;
            this.sold = sold;
        }

        public override string ToString() {
            return $"[id: {id}, name: {name}, price: {price}, sold: {sold}]";
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            if (this == obj) return true;
            if (!(obj is Product)) return false;
            return Equals(obj as Product);
        }

        private bool Equals(Product other) {
            return id == other.id && name == other.name && price.Equals(other.price) && sold == other.sold;
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (name != null ? name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ price.GetHashCode();
                hashCode = (hashCode * 397) ^ sold;
                return hashCode;
            }
        }
    }
}
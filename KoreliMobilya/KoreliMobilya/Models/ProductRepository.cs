namespace KoreliMobilyaDeneme.Models
{
    public class ProductRepository
    {
        private readonly List<Product> _products = new List<Product>()
        {
            new() { Id = 1,Name="Dore"},
            new() { Id = 2,Name="Elephant"},
            new() { Id = 3,Name="Mikok"}
        };

        public List<Product> GetAll() => _products;

        public void add(Product newProduct) => _products.Add(newProduct);

        public void Remove(int id)
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == id);

            if (hasProduct == null)
            {
                throw new Exception($"Bu id({id})'ye sahip ürün bulunmamaktadır.");
            }
            _products.Remove(hasProduct);
        }

        public void Update(Product updateProduct)
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == updateProduct.Id);

            if (hasProduct == null)
            {
                throw new Exception($"Bu id({updateProduct.Id})'ye sahip ürün bulunmamaktadır.");
            }

            hasProduct.Name = updateProduct.Name;

            var index = _products.FindIndex(x => x.Id == updateProduct.Id);

            _products[index] = hasProduct;
        }

    }
}

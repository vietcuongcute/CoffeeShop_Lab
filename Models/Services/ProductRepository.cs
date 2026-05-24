using coffeeshop.Models.Interfaces;

namespace coffeeshop.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> productsList = new()
        {
            new Product
            {
                Id = 1,
                Name = "America",
                Price = 25,
                Detail = "America coffee product",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp",
                IsTrendingProduct = true
            },
            new Product
            {
                Id = 2,
                Name = "Vietnam",
                Price = 20,
                Detail = "Vietnamese coffee product",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp",
                IsTrendingProduct = true
            },
            new Product
            {
                Id = 3,
                Name = "United Kingdom",
                Price = 15,
                Detail = "UK coffee product",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp",
                IsTrendingProduct = false
            }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return productsList;
        }

        public IEnumerable<Product> GetTrendingProducts()
        {
            return productsList.Where(p => p.IsTrendingProduct);
        }

        public Product? GetProductDetail(int id)
        {
            return productsList.FirstOrDefault(p => p.Id == id);
        }
    }
}
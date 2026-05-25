using coffeeshop.Models;

namespace coffeeshop.Models.Interfaces
{
    public interface IShoppingCartRepository
    {
        void AddToCart(Product product);

        int RemoveFromCart(Product product);

        List<ShoppingCartItem> GetAllShoppingCartItems();

        void ClearCart();

        decimal GetShoppingCartTotal();

        List<ShoppingCartItem>? ShoppingCartItems { get; set; }
    }
}
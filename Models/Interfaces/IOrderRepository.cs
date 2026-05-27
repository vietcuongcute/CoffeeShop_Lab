using coffeeshop.Models;

namespace coffeeshop.Models.Interfaces
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order);
    }
}
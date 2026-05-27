using coffeeshop.Data;
using coffeeshop.Models.Interfaces;

namespace coffeeshop.Models.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CoffeeshopDbContext dbContext;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public OrderRepository(
            CoffeeshopDbContext dbContext,
            IShoppingCartRepository shoppingCartRepository)
        {
            this.dbContext = dbContext;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public void PlaceOrder(Order order)
        {
            var shoppingCartItems = shoppingCartRepository.GetAllShoppingCartItems();

            order.OrderDetails = new List<OrderDetail>();

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = item.Qty,
                    ProductId = item.Product.Id,
                    Price = item.Product.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = shoppingCartRepository.GetShoppingCartTotal();

            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }
    }
}
using EcomAPI.Data;
using EcomAPI.Events;
using EcomAPI.Models;
using MediatR;

namespace EcomAPI.Projections
{
    public class OrderCreatedProjectionHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ReadDbContext _context;
        public OrderCreatedProjectionHandler(ReadDbContext context)
        {
            _context = context;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = notification.OrderId,
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                Status = "CreatedAt",
                CreatedAt = DateTime.Now,
                TotalCost = notification.TotalCost
            };

            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

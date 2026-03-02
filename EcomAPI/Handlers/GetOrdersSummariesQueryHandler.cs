using EcomAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EcomAPI.Handlers
{
    public class GetOrdersSummariesQueryHandler : IQueryHandler<GetOrdersSummariesQuery, List<OrderSummaryDto>>
    {
        public readonly ReadDbContext _context;
        public GetOrdersSummariesQueryHandler(ReadDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderSummaryDto>?> HandleAsync(GetOrdersSummariesQuery query)
        {
            return await _context.Orders
            .Select(o => new OrderSummaryDto(
                o.Id,
                o.FirstName + " " + o.LastName,
                o.Status,
                o.TotalCost
            )).ToListAsync();
        }
    }
}

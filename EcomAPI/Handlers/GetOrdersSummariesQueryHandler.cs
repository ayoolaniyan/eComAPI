using EcomAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EcomAPI.Handlers
{
    public class GetOrdersSummariesQueryHandler : IQueryHandler<GetOrdersSummariesQuery, List<OrderSummaryDto>>
    {
        public readonly AppDbContext _context;
        public GetOrdersSummariesQueryHandler(AppDbContext context)
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

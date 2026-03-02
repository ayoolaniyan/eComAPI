using EcomAPI.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcomAPI.Handlers
{
    public class GetOrdersSummariesQueryHandler : IRequestHandler<GetOrdersSummariesQuery, List<OrderSummaryDto>>
    {
        public readonly ReadDbContext _context;
        public GetOrdersSummariesQueryHandler(ReadDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderSummaryDto>> Handle(GetOrdersSummariesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .AsNoTracking()
                .Select(o => new OrderSummaryDto(
                    o.Id,
                    o.FirstName + " " + o.LastName,
                    o.Status,
                    o.TotalCost
                )).ToListAsync(cancellationToken);
        }
    }
}

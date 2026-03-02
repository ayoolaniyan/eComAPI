using MediatR;

public record GetOrdersSummariesQuery()  : IRequest<List<OrderSummaryDto>>;

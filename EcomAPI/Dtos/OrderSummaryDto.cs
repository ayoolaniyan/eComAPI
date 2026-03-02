public record OrderSummaryDto
(
    int orderId,
    string CustomerName,
    string Status,
    Decimal TotalCost
);

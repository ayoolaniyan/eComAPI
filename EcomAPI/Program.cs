using EcomAPI.Data;
using EcomAPI.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto>, GetOrderByIdQueryHandler>();

var app = builder.Build();

app.MapPost("/api/orders", async (ICommandHandler<CreateOrderCommand, OrderDto> handler, CreateOrderCommand command) =>
{
    var createdOrder = await handler.HandleAsync(command);

    if (createdOrder == null)
        return Results.BadRequest("Failed to create an order");

    return Results.Created($"/api/orders{createdOrder.Id}", createdOrder);
});

app.MapGet("/api/orders/{id}", async (IQueryHandler<GetOrderByIdQuery, OrderDto> handler, int id) =>
{
    var order = await handler.HandleAsync(new GetOrderByIdQuery(id));
    if (order == null)
        return Results.NotFound();

    return Results.Ok(order);
});

app.Run();

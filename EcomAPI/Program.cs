using EcomAPI.Commands;
using EcomAPI.Data;
using EcomAPI.Events;
using EcomAPI.Handlers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));
builder.Services.AddDbContext<WriteDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("WriteDbConnection")));
builder.Services.AddDbContext<ReadDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("ReadDbConnection")));

builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto>, GetOrderByIdQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrdersSummariesQuery, List<OrderSummaryDto>>, GetOrdersSummariesQueryHandler>();
builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();

builder.Services.AddSingleton<IEventPublisher, ConsoleEventPublisher>();

var app = builder.Build();

app.MapPost("/api/orders", async (ICommandHandler<CreateOrderCommand, OrderDto> handler, CreateOrderCommand command) =>
{
    try
    {
        var createdOrder = await handler.HandleAsync(command);

        if (createdOrder == null)
            return Results.BadRequest("Failed to create an order");

        return Results.Created($"/api/orders{createdOrder.Id}", createdOrder);
        
    }
    catch (ValidationException ex)
    {
        var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
        return Results.BadRequest(errors);
    }
    
});

app.MapGet("/api/orders/{id}", async (IQueryHandler<GetOrderByIdQuery, OrderDto> handler, int id) =>
{
    var order = await handler.HandleAsync(new GetOrderByIdQuery(id));
    if (order == null)
        return Results.NotFound();

    return Results.Ok(order);
});

// TODOS: Pagination
app.MapGet("/api/orders", async (IQueryHandler<GetOrdersSummariesQuery, List<OrderSummaryDto>> handler) =>
{
    var summaries = await handler.HandleAsync(new GetOrdersSummariesQuery());
    return Results.Ok(summaries);
});

app.Run();

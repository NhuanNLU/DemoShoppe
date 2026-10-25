using DemoShoppe.Application.DependencyInjection.Extensions;
using DemoShoppe.Contract.DependencyInjection.Extensions;
using DemoShoppe.Persistence.DependencyInjection.Extensions;
using DemoShoppe.Presentation.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(typeof(AuthController).Assembly);
builder.Services.AddMediatR();
builder.Services.AddFluentValidation();
builder.Services.AddOpenApi();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddUnitOfWork();
builder.Services.AddRepositoryBase();
builder.Services.AddUserRepository();
builder.Services.AddProfileRepository();
builder.Services.AddCategoryRepository();
builder.Services.AddProductRepository();
builder.Services.AddProductCategoryRepository();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

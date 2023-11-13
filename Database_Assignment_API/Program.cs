using Database_Assignment_API.Contexts;
using Database_Assignment_API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<CustomerInformationRepository>();
builder.Services.AddScoped<CustomerInformationTypeRepository>();
builder.Services.AddScoped<InStockRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderRowRepository>();
builder.Services.AddScoped<PrimaryCategoryRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<SubCategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

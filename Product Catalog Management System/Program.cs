using Product_Catalog_Management_System.Middlware;
using Product_Catalog_Management_System.Models;
using Product_Catalog_Management_System.Repositories;
using Product_Catalog_Management_System.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IRepository<Product>, InMemoryRepository<Product>>();
builder.Services.AddSingleton<IRepository<Category>, InMemoryRepository<Category>>();
builder.Services.AddSingleton<IRepository<Product>, InMemoryRepository<Product>>();
builder.Services.AddSingleton<IRepository<Category>, InMemoryRepository<Category>>(); 
builder.Services.AddSingleton<ProductSearchEngine>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseCors("AllowAngular");

app.UseMiddleware<RequestLoggingMiddleware>(); 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

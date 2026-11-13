using EfCore2.Data;
using EfCore2.Files;
using EfCore2.Middlewares;
using EfCore2.Repositories;
using EfCore2.Seeding;
using EfCore2.Services;
using EfCore2.Seeding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails(
    option =>
    {
        option.CustomizeProblemDetails = context =>
        {
            context.ProblemDetails.Instance ??= context.HttpContext.Request.Path.Value;

            context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
        };
    }
);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Repository
// builder.Services.AddScoped<IProductRepository, SqlProductRepository>();

// EFCORE REPOSITORY
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<IProductQueryRepository, EfProductQueryRepository>();

// Service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageStorage, LocalProductImageStorage>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();

var connectionString = builder.Configuration.GetConnectionString("ecommerce")
        ?? throw new InvalidOperationException("ConnectionString : Belum dikonfigurasi");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString)
       .UseSeeding((context, _) => ProductSeedData.Seed(context))
       .UseAsyncSeeding(async (context, _, ct) =>
       {
           await ProductSeedData.SeedAsync(context, ct);
           await OrderSeedData.SeedAsync(context, ct);
       });
});


var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
     {
         options.SwaggerEndpoint("/openapi/v1.json", "Koda Store API v1");
         options.RoutePrefix = "swagger";

         options.DocumentTitle = "Koda Store | API Documentation";

         options.EnableFilter();
     });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Handlers;
using ShoppingCart.Data;
using ShoppingCart.Services;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// For Swagger/OpenAPI documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(
        Path.Combine(AppContext.BaseDirectory, xmlFilename)
    );
});

// Register application services
builder.Services.AddServices();

// Register global exception handler
builder.Services.AddScoped<GlobalExceptionHandler>();


// Configure Entity Framework with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseCors("DevCors");

// Middleware to handle exceptions globally
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var handler = context.RequestServices
            .GetRequiredService<GlobalExceptionHandler>();

        await handler.HandleAsync(context);
    });
});

// Set a global path base for the API
app.UsePathBase("/api");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

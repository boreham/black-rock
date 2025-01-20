using BlackRock.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Разрешить React-приложению
              .AllowAnyMethod() // Разрешить любые HTTP-методы (GET, POST, PUT, DELETE)
              .AllowAnyHeader(); // Разрешить любые заголовки
    });
});

// Register ProductService
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

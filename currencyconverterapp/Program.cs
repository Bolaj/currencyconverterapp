using currencyconverterapp.Data;
using currencyconverterapp.Repository;
using currencyconverterapp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Currency Converter API", Version = "v1" });
});


builder.Services.AddDbContext<CurrencyRateDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("CurrencyConnectionString")
    ));

builder.Services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();
builder.Services.AddScoped<ICurrency, CurrencyService>();
builder.Services.AddHttpClient();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency Converter API v1"));


app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();

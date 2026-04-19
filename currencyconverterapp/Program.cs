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
builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency Converter API v1"));


app.MapGet("/", () => "Hello World!");


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CurrencyRateDbContext>();

    var retries = 5;
    while (retries > 0)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch
        {
            retries--;
            Thread.Sleep(5000); // wait 5 seconds
        }
    }
}

app.MapControllers();
app.Run();

using System;
using currencyconverterapp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace currencyconverterapp.Data;

public class CurrencyRateDbContext : DbContext
{
    public CurrencyRateDbContext(DbContextOptions<CurrencyRateDbContext> options) : base(options)
    {
    }

    public DbSet<CurrencyRate> CurrencyRates { get; set; }


}

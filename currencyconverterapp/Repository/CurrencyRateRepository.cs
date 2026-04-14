using System;
using currencyconverterapp.Data;
using currencyconverterapp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace currencyconverterapp.Repository;

public class CurrencyRateRepository : ICurrencyRateRepository
{
    private readonly CurrencyRateDbContext dbContext;

    public CurrencyRateRepository(CurrencyRateDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CurrencyRate?> GetRateAsync(string baseCurrency, string targetCurrency)
    {
        return await dbContext.CurrencyRates
            .FirstOrDefaultAsync(x => x.BaseCurrency == baseCurrency && x.TargetCurrency == targetCurrency);
    }

    public async Task AddRateAsync(CurrencyRate rate)
    {
        await dbContext.CurrencyRates.AddAsync(rate);
    }

    public Task UpdateRateAsync(CurrencyRate rate)
    {
        dbContext.CurrencyRates.Update(rate);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

}

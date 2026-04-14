using System;
using currencyconverterapp.Models.Domain;

namespace currencyconverterapp.Repository;

public interface ICurrencyRateRepository
{
    Task<CurrencyRate?> GetRateAsync(string baseCurrency, string targetCurrency);
    Task AddRateAsync(CurrencyRate rate);
    Task UpdateRateAsync(CurrencyRate rate);
    Task SaveChangesAsync();

}

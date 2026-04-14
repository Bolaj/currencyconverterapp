using System;
using System.Linq;
using currencyconverterapp.Data;
using currencyconverterapp.Models.Domain;
using currencyconverterapp.Repository;

namespace currencyconverterapp.Services;

public class CurrencyService : ICurrency
{
    private readonly ICurrencyRateRepository repository;
    private readonly HttpClient httpClient;
    public CurrencyService(ICurrencyRateRepository repository, HttpClient httpClient)
    {
        this.repository = repository;
        this.httpClient = httpClient;
        
    }
    public async Task<decimal> Convert(string fromCurrency, string toCurrency, decimal amount)
    {
        var rate = await repository.GetRateAsync(fromCurrency, toCurrency);

        if (rate == null || rate.LastUpdated < DateTime.UtcNow.AddHours(-1))
        {
            rate = await FetchAndSaveRate(fromCurrency, toCurrency);
        }

        return amount * rate.Rate;
    }
     private async Task<CurrencyRate> FetchAndSaveRate(string from, string to)
    {
        var response = await httpClient.GetFromJsonAsync<ExchangeResponse>(
            $"https://api.exchangerate-api.com/v4/latest/{from}");

        if (response == null || !response.rates.ContainsKey(to))
        {
            throw new Exception("Failed to fetch exchange rate");
        }

        var existing = await repository.GetRateAsync(from, to);

        if (existing != null)
        {
            existing.Rate = response.rates[to];
            existing.LastUpdated = DateTime.UtcNow;

            await repository.UpdateRateAsync(existing);
            await repository.SaveChangesAsync();

            return existing;
        }

        var newRate = new CurrencyRate
        {
            BaseCurrency = from,
            TargetCurrency = to,
            Rate = response.rates[to],
            LastUpdated = DateTime.UtcNow
        };

        await repository.AddRateAsync(newRate);
        await repository.SaveChangesAsync();

        return newRate;
    }
    

}

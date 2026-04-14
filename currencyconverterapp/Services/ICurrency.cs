using System;

namespace currencyconverterapp.Services;

public interface ICurrency
{
    Task<decimal> Convert(string fromCurrency, string toCurrency, decimal amount);

}

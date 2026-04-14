using System;

namespace currencyconverterapp.Models.Domain;

public class CurrencyRate
{
    public int Id { get; set; }
    public string BaseCurrency { get; set; }
    public string TargetCurrency { get; set; }
    public decimal Rate { get; set; }
    public DateTime LastUpdated { get; set; }

}

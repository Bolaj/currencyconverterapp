using System;

namespace currencyconverterapp.Models.Dtos;

public class CurrencyConvertRequestDto
{
     public string From { get; set; }
    public string To { get; set; }
    public decimal Amount { get; set; }

}

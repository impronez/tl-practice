using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class Currency : ValueObject
{
    public static readonly Currency Rub = new(nameof(Rub));
    public static readonly Currency Usd = new(nameof(Usd));
    public static readonly Currency Eur = new(nameof(Eur));

    private static readonly Currency[] All = [Rub, Usd, Eur];

    protected Currency()
    {
    }

    private Currency(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Currency> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Result.Failure<Currency>("Value cannot be null or whitespace.");
        }

        string currency = input.Trim().ToLower();

        if (All.Any(c => c.Value.ToLower() == currency) == false)
        {
            return Result.Failure<Currency>($"Value [{input}] is not a currency.");
        }

        return new Currency(currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
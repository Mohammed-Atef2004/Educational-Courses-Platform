using EducationalPlatform.EducationalPlatform.Domain.Courses;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.EducationalPlatform.Domain.Courses.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Amount   { get; }
    public string  Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount   = amount;
        Currency = currency;
    }

    public static Result<Money> Create(decimal amount, string currency = "EGP")
    {
        if (amount < 0)
            return Result<Money>.Failure(CourseErrors.Price.Negative);

        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            return Result<Money>.Failure(CourseErrors.Price.InvalidCurrency);

        return Result<Money>.Success(new Money(amount, currency.ToUpperInvariant()));
    }


    public static Money Free() => new(0, "EGP");

    public bool IsFree() => Amount == 0;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => IsFree() ? "Free" : $"{Amount:F2} {Currency}";
}

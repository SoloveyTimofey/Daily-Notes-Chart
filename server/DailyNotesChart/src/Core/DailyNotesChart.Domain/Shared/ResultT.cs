namespace DailyNotesChart.Domain.Shared;

public class Result<TValue> : Result
{
    private readonly TValue? _value;
    protected internal Result(TValue? value, bool isSucceeded, Error error) : base(isSucceeded, error)
    {
        _value = value;
    }

    public TValue? Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("The value of a falure result cannot be accessed.");
}
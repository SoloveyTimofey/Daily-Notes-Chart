namespace DailyNotesChart.Domain.Shared.ResultPattern;

//public class Result<TValue> : Result
//{
//    private readonly TValue? _value;
//    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
//    {
//        _value = value;
//    }

//    public TValue? Value => IsSuccess
//        ? _value
//        : throw new InvalidOperationException("The value of a falure result cannot be accessed.");
//}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, List<Error> errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }

    public TValue? Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("The value of a failure result cannot be accessed.");
}
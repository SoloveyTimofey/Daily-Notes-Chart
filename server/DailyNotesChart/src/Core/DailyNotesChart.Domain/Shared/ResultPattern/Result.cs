namespace DailyNotesChart.Domain.Shared.ResultPattern;

//public class Result
//{
//    protected internal Result(bool isSuccess, Error error)
//    {
//        if (isSuccess && error != Error.None) throw new InvalidOperationException();

//        if (!isSuccess && error == Error.None) throw new InvalidOperationException();

//        IsSuccess = isSuccess;
//        Error = error;
//    }

//    public bool IsSuccess { get; init; }
//    public bool IsFailure => !IsSuccess;
//    public Error Error { get; init; }

//    public static Result Success() => new(true, Error.None);
//    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
//    public static Result Failure(Error error) => new(false, error);
//    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
//}

public class Result
{
    protected internal Result(bool isSuccess, List<Error> errors)
    {
        if (isSuccess && errors.Count > 0) throw new InvalidOperationException("Success result cannot contain errors.");

        if (!isSuccess && errors.Count == 0) throw new InvalidOperationException("Failure result must contain at least one error.");

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<Error> Errors { get; init; }

    public static Result Success() => new(true, new List<Error>());
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, new List<Error>());
    public static Result Failure(params Error[] errors) => new(false, errors.ToList());
    public static Result<TValue> Failure<TValue>(params Error[] errors) => new(default, false, errors.ToList());
}
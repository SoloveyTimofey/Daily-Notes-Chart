using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Application.Extensions;

public static class ResultExtensions
{
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func) 
        => result.IsSuccess ? func(result.Value!) : Result.Failure<TOut>(result.Error);

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func)
        => result.IsSuccess ? Result.Success(func(result.Value!)) : Result.Failure<TOut>(result.Error);
}
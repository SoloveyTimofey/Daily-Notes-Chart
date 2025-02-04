using DailyNotesChart.Application.Exceptions;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Application.Operations;

public abstract class CommandHandlerBase<TResponse>
{
    private const string EXCEPTION_MESSAGE = "Before calling this method you have checked if result is failure.";

    protected static Task<Result<TResponse>> FailureAsync(Result result)
    {
        return result.IsFailure ? Task.FromResult(Result.Failure<TResponse>(result.Error)) : throw new ThisShouldNotHaveHappenedException(EXCEPTION_MESSAGE);
    }

    protected static Result<TResponse> Failure(Result result)
    {
        return result.IsFailure ? Result.Failure<TResponse>(result.Error) : throw new ThisShouldNotHaveHappenedException(EXCEPTION_MESSAGE);
    }

    protected static Result<TAnotherResult> Failure<TAnotherResult>(Result result)
    {
        return result.IsFailure ? Result.Failure<TAnotherResult>(result.Error) : throw new ThisShouldNotHaveHappenedException(EXCEPTION_MESSAGE);
    }

    protected static Task<Result<TAnotherResponse>> FailureAsync<TAnotherResponse>(Result result)
    {
        return result.IsFailure ? Task.FromResult(Result.Failure<TAnotherResponse>(result.Error)) : throw new ThisShouldNotHaveHappenedException(EXCEPTION_MESSAGE);
    }
}
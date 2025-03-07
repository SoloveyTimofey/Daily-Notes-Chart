using DailyNotesChart.Domain.Shared.ResultPattern;
using MediatR;

namespace DailyNotesChart.Application.Abstractions.MediatrSpecific;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
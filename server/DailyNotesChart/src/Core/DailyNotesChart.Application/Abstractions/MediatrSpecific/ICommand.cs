using DailyNotesChart.Domain.Shared;
using MediatR;

namespace DailyNotesChart.Application.Abstractions.MediatrSpecific;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
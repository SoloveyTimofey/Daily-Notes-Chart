using DailyNotesChart.Domain.Shared.ResultPattern;
using MediatR;

namespace DailyNotesChart.Application.Abstractions.MediatrSpecific;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
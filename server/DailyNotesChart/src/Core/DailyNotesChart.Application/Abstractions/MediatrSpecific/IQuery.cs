using DailyNotesChart.Domain.Shared;
using MediatR;

namespace DailyNotesChart.Application.Abstractions.MediatrSpecific;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
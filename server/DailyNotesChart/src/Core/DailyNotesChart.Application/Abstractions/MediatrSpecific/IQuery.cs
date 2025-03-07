using DailyNotesChart.Domain.Shared.ResultPattern;
using MediatR;

namespace DailyNotesChart.Application.Abstractions.MediatrSpecific;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
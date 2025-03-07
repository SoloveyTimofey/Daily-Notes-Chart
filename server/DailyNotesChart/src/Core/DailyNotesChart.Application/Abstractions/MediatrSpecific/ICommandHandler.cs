﻿using DailyNotesChart.Domain.Shared.ResultPattern;
using MediatR;

namespace DailyNotesChart.Application.Abstractions.MediatrSpecific;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
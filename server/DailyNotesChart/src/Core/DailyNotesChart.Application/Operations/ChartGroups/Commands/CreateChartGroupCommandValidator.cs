using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using FluentValidation;

namespace DailyNotesChart.Application.Operations.ChartGroups.Commands;

internal class CreateChartGroupCommandValidator : AbstractValidator<CreateChartGroupCommand>
{
    public CreateChartGroupCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Name).NotEmpty().MaximumLength(ChartGroupName.NAME_MAX_LENGHT).MinimumLength(ChartGroupName.NAME_MIN_LENGHT);
    }
}
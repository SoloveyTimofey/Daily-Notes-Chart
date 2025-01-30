using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

public class YAxeName : ValueObject
{
    public const int NAME_MIN_LENGHT = 3;
    public const int NAME_MAX_LENGHT = 20;

    private YAxeName(string name) => Value = name;

    public string Value { get; }

    public static Result<YAxeName> Create(string name)
    {
        if (name.Length < NAME_MIN_LENGHT || name.Length > NAME_MAX_LENGHT)
        {
            return Result.Failure<YAxeName>(DomainErrors.Chart.InvalidYAxeName);
        }

        return Result.Success(new YAxeName(name));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
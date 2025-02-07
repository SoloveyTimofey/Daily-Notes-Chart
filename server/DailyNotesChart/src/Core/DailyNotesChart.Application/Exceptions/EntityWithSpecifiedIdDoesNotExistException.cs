namespace DailyNotesChart.Application.Exceptions;

public class EntityWithSpecifiedIdDoesNotExistException : ApplicationException
{
    public EntityWithSpecifiedIdDoesNotExistException(string entityName, string providedId) : base($"Entity {entityName} with ID {providedId} does not exist") { }
}
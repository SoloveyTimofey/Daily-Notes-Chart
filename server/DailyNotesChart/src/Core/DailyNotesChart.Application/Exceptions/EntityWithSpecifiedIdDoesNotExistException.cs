namespace DailyNotesChart.Application.Exceptions;

public class EntityWithSpecifiedIdDoesNotExistException : ApplicationException
{
    public EntityWithSpecifiedIdDoesNotExistException(string entityName, string id) : base($"Entity {entityName} with ID {id} does not exist") { }
}
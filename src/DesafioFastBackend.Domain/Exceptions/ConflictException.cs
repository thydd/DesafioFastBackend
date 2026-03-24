namespace DesafioFastBackend.Domain.Exceptions;

public class ConflictException(string message) : BusinessRuleException(message)
{
}

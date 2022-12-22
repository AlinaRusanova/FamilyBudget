
namespace FamilyFinance.Exceptions.Exceptions
{
    public class IncorrectUserException: Exception
    {
        public IncorrectUserException(object key) : base($"Username {key} not registered") { }
    }
}

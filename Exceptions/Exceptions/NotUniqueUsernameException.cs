

namespace FamilyFinance.Exceptions.Exceptions
{
    public class NotUniqueUsernameException: Exception
    {
        public NotUniqueUsernameException(object key) : base($"Username {key} shoul be unique. Please try another one") { }
    }
}

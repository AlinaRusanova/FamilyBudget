
namespace FamilyFinance.Exceptions.Exceptions
{
    public class NotRegisteredException:Exception
    {
        public NotRegisteredException(string name, object key) : base($"Entity {name} ({key}) didn't register.") { }
    }
}

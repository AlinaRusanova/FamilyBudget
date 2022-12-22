namespace FamilyFinance.Exceptions.Exceptions

{
    public class IncorrectPasswordException:Exception
    {
        public IncorrectPasswordException() : base($"Wrong password! Please try again") { }
    }
}

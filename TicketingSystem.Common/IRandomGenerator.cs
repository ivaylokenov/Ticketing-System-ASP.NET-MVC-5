namespace TicketingSystem.Common
{
    public interface IRandomGenerator
    {
        string RandomString(int minLength = 5, int maxLength = 50);

        int RandomNumber(int min, int max);
    }
}

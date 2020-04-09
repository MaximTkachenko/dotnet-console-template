namespace ConsoleApp.Services
{
    public class ServiceA : IServiceA
    {
        public int Get() => 5;
    }

    public interface IServiceA
    {
        int Get();
    }
}

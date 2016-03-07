namespace PatternsInAutomatedTests.Conference.Base
{
    public interface IFactory<T>
    {
        T Create();
    }
}
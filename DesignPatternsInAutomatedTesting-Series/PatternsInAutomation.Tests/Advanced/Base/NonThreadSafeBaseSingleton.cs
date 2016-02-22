
namespace PatternsInAutomation.Tests.Advanced.Base
{
    public abstract class NonThreadSafeBaseSingleton<T>
        where T: new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }
    }
}

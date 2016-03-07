namespace PatternsInAutomatedTests.Advanced.Base
{
    public abstract class ThreadSafeBaseSingleton<T>
        where T : new()
    {
        private static T instance;
        private static readonly object lockObject = new object();

        private ThreadSafeBaseSingleton()
        {
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
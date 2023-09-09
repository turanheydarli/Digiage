namespace Core.Utilities.Singletons
{
    /// <summary>
    /// Singleton that persists across multiple scenes
    /// </summary>
    public class PersistentSingleton<T> : SingletonBase<T> where T : SingletonBase<T>
    {
        protected override void Awake()
        {
            base.Awake();

            if (instance || instance == this)
            {
                // Skip if destroying
                if (transform.parent != null)
                    transform.parent = null; // Make sure this is on root so DontDestroyOnLoad works

                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
using UnityEngine;

namespace Core.Utilities.Singletons
{
    public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static volatile T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                }

                return instance;
            }
        }

        /// <summary>
        /// Awake method to associate singleton with instance
        /// </summary>
        protected virtual void Awake()
        {
            if (instance != null)
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// OnDestroy method to clear singleton association
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (instance == this) instance = null;
        }
    }
}
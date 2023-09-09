using UnityEngine;

namespace Core.Utilities.ScriptableObjects
{
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>(typeof(T).Name);

                    if (_instance == null)
                    {
                        T[] foundInstances = Resources.FindObjectsOfTypeAll<T>();
                        if (foundInstances.Length > 0)
                        {
                            _instance = foundInstances[0];
                            if (foundInstances.Length > 1)
                            {
                                Debug.LogWarningFormat("Multiple instances of {0} found in Resources folder. Using the first one.", typeof(T));
                            }
                        }
                        else
                        {
                            Debug.LogErrorFormat("{0} - Setting file not found in Resources folder. Creating a new one with default settings.", typeof(T));
                            _instance = CreateInstance<T>();
                        }
                    }
                }

                return _instance;
            }
            
        }
    }
}
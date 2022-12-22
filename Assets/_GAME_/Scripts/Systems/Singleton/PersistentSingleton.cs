using UnityEngine;

namespace OmnioCore.Singleton
{
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {

        public static T Instance => instance;

        private static T instance;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = GetComponent<T>();
            }
            else if (instance != GetComponent<T>())
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

    }
}

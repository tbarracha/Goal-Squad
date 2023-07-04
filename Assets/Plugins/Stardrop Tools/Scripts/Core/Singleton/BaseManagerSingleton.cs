
using UnityEngine;

namespace StardropTools
{
    public class BaseManagerSingleton<T> : BaseManager where T : MonoBehaviour
	{
		#region Singleton
		/// <summary>
		/// The instance.
		/// </summary>
		private static T instance;


		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();
					if (instance == null)
					{
						GameObject obj = new GameObject();
						obj.name = typeof(T).Name;
						instance = obj.AddComponent<T>();
					}
				}
				return instance;
			}
		}

		void SingletonInitialization()
		{
			if (instance == null)
			{
				instance = this as T;
				DontDestroyOnLoad(gameObject);
			}

			else
				Destroy(gameObject);
		}


		protected override void Awake()
		{
			SingletonInitialization();
			base.Awake();
		}
		#endregion // singleton


		protected override void EventFlow() { }
	}
}
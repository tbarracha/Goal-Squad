
using UnityEngine;

namespace StardropTools
{
    public class BaseObjectManagerSingleton<T> : BaseObject, IManager where T : MonoBehaviour
	{
		public bool canUpdate { get; protected set; }

		public void InitializeManager() => Initialize();

		public void LateInitializeManager() => LateInitialize();

		public override void Initialize()
		{
			base.Initialize();
			EventFlow();
		}

		/// <summary>
		/// Method reserved for Game Event/Action subscriptions, that change the current objects behaviour
		/// </summary>
		protected virtual void EventFlow() { }

        public override void StartUpdate()
        {
            base.StartUpdate();

			if (canUpdate == false)
				canUpdate = true;
        }

        public bool GetCanUpdate() => canUpdate;

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
	}
}
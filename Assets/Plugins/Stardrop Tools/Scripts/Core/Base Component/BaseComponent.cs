using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Base class for all Initializable MonoBehaviours
    /// </summary>
    public class BaseComponent : MonoBehaviour, IInitialize, ILateInitialize, IUpdateable
    {
        [Header("Initialization")]
        [SerializeField] protected InitializeAt initializeAt;

        [NaughtyAttributes.Foldout("Object Info")]
        [NaughtyAttributes.ReadOnly]
        [SerializeField] protected GameObject selfObject;


        public bool IsInitialized { get; protected set; }
        public bool IsLateInitialized { get; protected set; }
        public GameObject GameObject => selfObject;


        protected virtual void Awake()
        {
            if (initializeAt == InitializeAt.Awake)
                Initialize();
        }

        protected virtual void Start()
        {
            if (initializeAt == InitializeAt.Start)
                Initialize();
        }


        /// <summary>
        /// Instead of class initializing at Start() by itself,
        /// we call this function when we want it to "Start"
        /// </summary>
        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
        }

        public virtual void LateInitialize()
        {
            if (IsLateInitialized)
                return;

            IsLateInitialized = true;
        }


        /// <summary>
        /// Set objects active state
        /// </summary>
        public void SetActive(bool value) => selfObject.SetActive(value);



        public virtual void StartUpdate() => LoopManager.AddToUpdate(this);

        public virtual void StopUpdate() => LoopManager.RemoveFromUpdate(this);

        public virtual void HandleUpdate() { }

        public virtual void ResetComponentPublic() { }

        protected virtual void ResetComponent() { }


        protected virtual void OnValidate()
        {
            if (selfObject == null)
                selfObject = gameObject;
        }
    }
}
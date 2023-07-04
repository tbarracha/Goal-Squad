
using UnityEngine;

namespace StardropTools
{
    public abstract class BaseGameManager : ManagerInitializer
    {
        #region Singleton
        /// <summary>
        /// The instance.
        /// </summary>
        private static BaseGameManager instance;


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static BaseGameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BaseGameManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(BaseGameManager).Name;
                        instance = obj.AddComponent<BaseGameManager>();
                    }
                }
                return instance;
            }
        }

        public void SingletonInitialization()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            else
                Destroy(gameObject);
        }
        #endregion // singleton


        [SerializeField] BaseGameState gameState;
        public BaseGameState GameState => gameState;


        #region Events

        public static readonly EventHandler OnInitialized       = new EventHandler();

        public static readonly EventHandler OnMainMenu          = new EventHandler();
        public static readonly EventHandler OnPlayStart         = new EventHandler();
        public static readonly EventHandler OnPlayEnd           = new EventHandler();

        public static readonly EventHandler OnPaused            = new EventHandler();
        public static readonly EventHandler OnResumed           = new EventHandler();

        public static readonly EventHandler OnNextLevel         = new EventHandler();
        public static readonly EventHandler OnGenerating        = new EventHandler();
        public static readonly EventHandler OnGenerationEnd     = new EventHandler();

        public static readonly EventHandler OnWin               = new EventHandler();
        public static readonly EventHandler OnLose              = new EventHandler();
        public static readonly EventHandler OnRestart           = new EventHandler();

        public static readonly EventHandler OnMainMenuRequest   = new EventHandler();
        public static readonly EventHandler OnPlayRequest       = new EventHandler();
        public static readonly EventHandler OnPlayEndRequest    = new EventHandler();
        public static readonly EventHandler OnPauseRequest      = new EventHandler();
        public static readonly EventHandler OnGenerateRequest   = new EventHandler();
        public static readonly EventHandler OnRestartRequest    = new EventHandler();
        public static readonly EventHandler OnNextLevelRequest  = new EventHandler();

        public static readonly EventHandler OnPoolsPopulated    = new EventHandler();

        #endregion // events


        protected override void Awake()
        {
            base.Awake();
            SingletonInitialization();
            InitializeManagers();
            EventFlow();
        }

        protected virtual void EventFlow()
        {
            OnMainMenuRequest.AddListener   (() => ChangeBaseGameState(BaseGameState.MainMenu));
            OnPlayRequest.AddListener       (() => ChangeBaseGameState(BaseGameState.Playing));
            OnPlayEndRequest.AddListener    (() => ChangeBaseGameState(BaseGameState.PlayResults));
            OnPauseRequest.AddListener      (() => ChangeBaseGameState(BaseGameState.Paused));
            OnGenerateRequest.AddListener   (() => ChangeBaseGameState(BaseGameState.Generating));
            OnGenerationEnd.AddListener     (() => ChangeBaseGameState(BaseGameState.MainMenu));

            OnRestartRequest.AddListener    (() => OnRestart?.Invoke());
            OnNextLevelRequest.AddListener  (() => OnNextLevel?.Invoke());
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (Application.isPlaying == false && gameState != BaseGameState.Initializing)
                gameState = BaseGameState.Initializing;
        }

        protected void ChangeBaseGameState(BaseGameState targetState)
        {
            if (gameState == targetState)
                return;

            BaseGameState prevState = gameState;
            gameState = targetState;

            switch (prevState)
            {
                case BaseGameState.Initializing:
                    OnInitialized?.Invoke();
                    break;

                case BaseGameState.MainMenu:
                    break;

                case BaseGameState.Playing:
                    break;

                case BaseGameState.PlayResults:
                    break;

                case BaseGameState.Generating:
                    break;

                case BaseGameState.Paused:
                    if (targetState == BaseGameState.Playing)
                        OnResumed?.Invoke();
                    break;
            }

            switch (targetState)
            {
                case BaseGameState.MainMenu:
                    OnMainMenu?.Invoke();
                    break;

                case BaseGameState.Playing:
                    if (prevState != BaseGameState.Paused)
                        OnPlayStart?.Invoke();
                    break;

                case BaseGameState.PlayResults:
                    if (prevState == BaseGameState.Playing)
                        OnPlayEnd?.Invoke();
                    break;

                case BaseGameState.Generating:
                    OnGenerating?.Invoke();
                    break;

                case BaseGameState.Paused:
                    OnPaused?.Invoke();
                    break;
            }
        }

        protected abstract void Win();

        protected abstract void Lose();
    }
}
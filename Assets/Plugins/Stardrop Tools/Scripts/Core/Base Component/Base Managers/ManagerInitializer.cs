
namespace StardropTools
{
    /// <summary>
    /// Class that finds all child objects with the IManager interface and calls its methods in the following order : InitializeManager(), LateInitializeManager()
    /// <para> Note : NOT a singleton </para>
    /// </summary>
    public class ManagerInitializer : BaseComponent
    {
        protected IManager[] managers;
        [UnityEngine.SerializeField] protected BaseManagerUpdatable[] updatableManagers;

        public override void Initialize()
        {
            base.Initialize();
            InitializeManagers();
        }

        protected virtual void InitializeManagers()
        {
            managers = Utilities.GetListComponentsInChildren<IManager>(transform).ToArray();
            updatableManagers = Utilities.GetListComponentsInChildren<BaseManagerUpdatable>(transform).ToArray();

            Utilities.InitializeManagers(managers);
            Utilities.LateInitializeManagers(managers);
        }

        protected virtual void UpdateManagers() => Utilities.UpdateManagers(updatableManagers);

        public override void HandleUpdate()
        {
            UpdateManagers();
        }
    }
}
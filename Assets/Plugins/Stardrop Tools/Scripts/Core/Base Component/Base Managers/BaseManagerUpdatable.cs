
namespace StardropTools
{
    /// <summary>
    /// BaseComponent class with the IManager interface. Still use Initialize() and LateInitialze() for class setup
    /// <para>Note : NOT a singleton!</para>
    /// </summary>
    public abstract class BaseManagerUpdatable : BaseManager
    {
        public override void Initialize()
        {
            canUpdate = true;

            base.Initialize();
        }

        /// <summary>
        /// Method reserved for calling in Update
        /// </summary>
        public abstract void UpdateManager();

        /// <summary>
        /// Method reserved for calling in LateUpdate
        /// </summary>
        public virtual void LateUpdateManager() { }

        /// <summary>
        /// Method reserved for calling in FixedUpdate
        /// </summary>
        public virtual void FixedUpdateManager() { }
    }
}
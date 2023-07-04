using System.Collections;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// BaseComponent class with the IManager interface. Still use Initialize() and LateInitialze() for class setup
    /// <para>Note : NOT a singleton!</para>
    /// </summary>
    public abstract class BaseManager : BaseComponent, IManager
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
        protected abstract void EventFlow();


        public bool GetCanUpdate() => canUpdate;
    }
}
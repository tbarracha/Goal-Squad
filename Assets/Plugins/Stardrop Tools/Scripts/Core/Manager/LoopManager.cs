
using System.Collections.Generic;

namespace StardropTools
{
    /// <summary>
    /// Class responsible for the majority of the game loop (Normal, Fixed and Late Update functions), invoking the IUpdateable, IFixedUpdateable and ILateUpdateable
    /// <para> Update()         as HandleUpdate() </para>
    /// <para> FixedUpdate()    as HandleFixedUpdate() </para>
    /// <para> LateUpdate()     as HandleLateUpdate() </para>
    /// <para> Useful for calling update function, only when we need to update </para>
    /// </summary>
    public class LoopManager : Singleton<LoopManager>
    {
        public bool IsInitialized { get; private set; }

        [NaughtyAttributes.ShowNonSerializedField] private int updateCount;
        [NaughtyAttributes.ShowNonSerializedField] private int fixedUpdateCount;
        [NaughtyAttributes.ShowNonSerializedField] private int lateUpdateCount;

        #region Events

        public static readonly EventHandler OnFrameworkInitialized = new EventHandler();



        #endregion

        static List<IUpdateable>        updateList;
        static List<IFixedUpdateable>   fixedUpdateList;
        static List<ILateUpdateable>    lateUpdateList;


        public void Initialize()
        {
            if (IsInitialized)
                return;

            updateList      = new List<IUpdateable>();
            fixedUpdateList = new List<IFixedUpdateable>();
            lateUpdateList  = new List<ILateUpdateable>();

            OnFrameworkInitialized?.Invoke();
            IsInitialized = true;
        }


        private void Update()
        {
            if (IsInitialized == false)
                return;

            if (updateList.Exists())
            {
                if (updateCount != updateList.Count)
                    updateCount = updateList.Count;

                for (int i = 0; i < updateList.Count; i++)
                    updateList[i].HandleUpdate();
            }
        }


        private void FixedUpdate()
        {
            if (IsInitialized == false)
                return;

            if (updateList.Count > 0)
            {
                if (fixedUpdateCount != fixedUpdateList.Count)
                    fixedUpdateCount = fixedUpdateList.Count;

                for (int i = 0; i < fixedUpdateList.Count; i++)
                    fixedUpdateList[i].HandleFixedUpdate();
            }
        }


        private void LateUpdate()
        {
            if (IsInitialized == false)
                return;

            if (updateList.Count > 0)
            {
                if (lateUpdateCount != lateUpdateList.Count)
                    lateUpdateCount = lateUpdateList.Count;

                for (int i = 0; i < lateUpdateList.Count; i++)
                    lateUpdateList[i].HandleLateUpdate();
            }
        }


        /// <summary>
        /// Adds item to the Update List 
        /// </summary>
        public static void AddToUpdate(IUpdateable updateable) => updateList.Add(updateable);

        /// <summary>
        /// If list doesn't contain the item, we add it
        /// </summary>
        public static void AddToUpdateSafe(IUpdateable updateable)
        {
            if (updateList.Contains(updateable) == false)
                updateList.Add(updateable);
        }


        /// <summary>
        /// Remove item from the Update list
        /// </summary>
        public static void RemoveFromUpdate(IUpdateable updateable) => updateList.Remove(updateable);

        /// <summary>
        /// If list contains the item, we REMOVE it
        /// </summary>
        public static void RemoveFromUpdateSafe(IUpdateable updateable)
        {
            if (updateList.Contains(updateable))
                updateList.Remove(updateable);
        }



        /// <summary>
        /// Adds item to the Update List 
        /// </summary>
        public static void AddToFixedUpdate(IFixedUpdateable updateable) => fixedUpdateList.Add(updateable);

        /// <summary>
        /// If list doesn't contain the item, we add it
        /// </summary>
        public static void AddToFixedUpdateSafe(IFixedUpdateable updateable)
        {
            if (fixedUpdateList.Contains(updateable) == false)
                fixedUpdateList.Add(updateable);
        }


        /// <summary>
        /// Remove item from the Update list
        /// </summary>
        public static void RemoveFromFixedUpdate(IFixedUpdateable updateable) => fixedUpdateList.Remove(updateable);

        /// <summary>
        /// If list contains the item, we REMOVE it
        /// </summary>
        public static void RemoveFromFixedUpdateSafe(IFixedUpdateable updateable)
        {
            if (fixedUpdateList.Contains(updateable))
                fixedUpdateList.Remove(updateable);
        }



        /// <summary>
        /// Adds item to the Update List 
        /// </summary>
        public static void AddToLateUpdate(ILateUpdateable updateable) => lateUpdateList.Add(updateable);

        /// <summary>
        /// If list doesn't contain the item, we add it
        /// </summary>
        public static void AddToLateUpdateSafe(ILateUpdateable updateable)
        {
            if (lateUpdateList.Contains(updateable) == false)
                lateUpdateList.Add(updateable);
        }


        /// <summary>
        /// Remove item from the Update list
        /// </summary>
        public static void RemoveFromLateUpdate(ILateUpdateable updateable) => lateUpdateList.Remove(updateable);

        /// <summary>
        /// If list contains the item, we REMOVE it
        /// </summary>
        public static void RemoveFromLateUpdateSafe(ILateUpdateable updateable)
        {
            if (lateUpdateList.Contains(updateable))
                lateUpdateList.Remove(updateable);
        }
    }
}
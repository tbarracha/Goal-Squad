using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenManager : Singleton<TweenManager>, IUpdateable
    {
        [SerializeField] int tweenCount;
        List<Tween> tweens = new List<Tween>();
        bool isUpdating;

        protected override void Awake()
        {
            base.Awake();

            if (tweens == null)
                tweens = new List<Tween>();
        }

        public bool ProcessTween(Tween tween)
        {
            if (Application.isPlaying == false)
            {
                Debug.Log("Can't tween on Edit Mode!");
                return false;
            }

            if (tween == null)
            {
                Debug.Log("Tween is null!");
                return false;
            }

            if (tween.Duration <= 0)
            {
                Debug.Log("Tween has zero duration!");
                return false;
            }

            FilterTween(tween.TweenID, tween.TweenType);
            AddTween(tween);

            return true;
        }

        /// <summary>
        /// Check if there is already a tween of this type
        /// </summary>
        void FilterTween(int tweenID, TweenType tweenType)
        {
            if (tweens.Exists() == false)
                return;

            for (int i = 0; i < tweens.Count; i++)
            {
                if (tweens[i].TweenID == tweenID && tweens[i].TweenType == tweenType)
                    tweens[i].Stop();
            }
        }

        void AddTween(Tween tween)
        {
            if (tween == null)
                return;

            if (tween.isInManagerList == false)
            {
                tweens.Add(tween);
                tween.isInManagerList = true;

                if (isUpdating == false)
                {
                    StartUpdate();
                    isUpdating = true;
                }
            }
        }


        /// <summary>
        /// Remove input tween from update list
        /// </summary>
        public void RemoveTween(Tween tween)
        {
            if (tween == null)
                return;

            if (tween.isInManagerList == true)
            {
                tween.isInManagerList = false;
                tweens.Remove(tween);
            }
        }

        void UpdateTweens()
        {
            if (tweens.Exists())
                for (int i = 0; i < tweens.Count; i++)
                {
                    if (tweens.Exists() == false)
                        break;

                    tweens[i].UpdateTweenState();
                }

            tweenCount = tweens.Count;

            if (tweenCount == 0)
            {
                StopUpdate();
                isUpdating = false;
            }
        }


        /// <summary>
        /// Check cached tween list for the one that meets input criteria and Stop()'s it
        /// </summary>
        public void StopTween(int tweenID, TweenType tweenType)
        {
            foreach (Tween tween in tweens)
            {
                if (tween.TweenID == tweenID && tween.TweenType == tweenType)
                    tween.Stop();
            }
        }


        /// <summary>
        /// Check if tween is active. If true, Stop() tween
        /// </summary>
        public static void StopTween(Tween tween)
        {
            if (tween != null)
                tween.Stop();
        }

        public static void StartTweenComponents(TweenComponent[] tweenComponents)
        {
            if (tweenComponents.Exists() == false)
                return;

            for (int i = 0; i < tweenComponents.Length; i++)
                tweenComponents[i].StartTween();
        }

        public static void StopTweenComponents(TweenComponent[] tweenComponents)
        {
            if (tweenComponents.Exists() == false)
                return;

            for (int i = 0; i < tweenComponents.Length; i++)
                tweenComponents[i].StopTween();
        }


        public void StartUpdate() => LoopManager.AddToUpdate(this);

        public void StopUpdate() => LoopManager.RemoveFromUpdate(this);

        public void HandleUpdate() => UpdateTweens();
    }
}
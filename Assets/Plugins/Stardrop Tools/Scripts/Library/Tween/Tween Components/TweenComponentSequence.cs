
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenComponentSequence : MonoBehaviour
    {
        [SerializeField] TweenComponent mainTween;
        [SerializeField] TweenComponent[] tweensOnDelayComplete;
        [SerializeField] TweenComponent[] tweensOnTweenComplete;
        
        public void StartSequence()
        {
            if (mainTween == null)
                return;

            mainTween.OnDelayCompleted.AddListener(OnDelayComplete);
            mainTween.OnTweenCompleted.AddListener(OnTweenComplete);
        }


        void OnDelayComplete()
        {
            if (tweensOnDelayComplete.Length > 0)
                for (int i = 0; i < tweensOnDelayComplete.Length; i++)
                    tweensOnDelayComplete[i].StartTween();
        }

        void OnTweenComplete()
        {
            if (tweensOnTweenComplete.Length > 0)
                for (int i = 0; i < tweensOnTweenComplete.Length; i++)
                    tweensOnTweenComplete[i].StartTween();
        }
    }
}
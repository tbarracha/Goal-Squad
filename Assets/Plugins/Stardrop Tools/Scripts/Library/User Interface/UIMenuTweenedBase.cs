
using StardropTools.Tween;
using UnityEngine;

namespace StardropTools.UI
{
    public class UIMenuTweenedBase : UIMenuBase
    {
        [Header("Open & Close Tweens")]
        [SerializeField] TweenComponentManager tweenManager_Open;
        [SerializeField] TweenComponentManager tweenManager_Close;

        public override void Open()
        {
            base.Open();

            if (tweenManager_Close != null)
                tweenManager_Close.StopTweens();
            if (tweenManager_Open != null)
                tweenManager_Open.StartTweens();
        }

        public override void Close()
        {
            if (tweenManager_Open != null)
                tweenManager_Open.StopTweens();
            if (tweenManager_Close != null)
                tweenManager_Close.StartTweens();
        }
    }
}
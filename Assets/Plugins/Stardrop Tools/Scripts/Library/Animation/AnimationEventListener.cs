
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Used in conjunction with an Animator Component. Detects animation events based on event ID
    /// </summary>
    public class AnimationEventListener : MonoBehaviour
    {
        [NaughtyAttributes.ShowNonSerializedField] string intEvent = "AnimEventINT";
        [NaughtyAttributes.ShowNonSerializedField] string floatEvent = "AnimEventFLOAT";
        [NaughtyAttributes.ShowNonSerializedField] string stringEvent = "AnimEventSTRING";
        [SerializeField] bool debug;

        public readonly EventHandler<int> OnAnimEventINT = new EventHandler<int>();
        public readonly EventHandler<float> OnAnimEventFLOAT = new EventHandler<float>();
        public readonly EventHandler<string> OnAnimEventSTRING = new EventHandler<string>();

        public void AnimEventINT(int eventID)
        {
            if (debug)
                Debug.Log($"Anim event <color=lime>INT:</color> <color=white>{eventID}</color>");

            OnAnimEventINT?.Invoke(eventID);
        }

        public void AnimEventFLOAT(float eventID)
        {
            if (debug)
                Debug.Log($"Anim event <color=cyan>FLOAT:</color> <color=white>{eventID}</color>");

            OnAnimEventFLOAT?.Invoke(eventID);
        }

        public void AnimEventSTRING(string eventID)
        {
            if (debug)
                Debug.Log($"Anim event <color=yellow>STRING:</color> <color=white>{eventID}</color>");

            OnAnimEventSTRING?.Invoke(eventID);
        }
    }
}


namespace StardropTools.UI
{
    public class UIScrollSnapElement : BaseUIObject
    {
        public int index;
        [UnityEngine.Space]
        public bool isFirst;
        public bool isLast;
        [UnityEngine.Space]
        public bool canSwipeNext = true;
        public bool canSwipePrevious = true;
    }
}
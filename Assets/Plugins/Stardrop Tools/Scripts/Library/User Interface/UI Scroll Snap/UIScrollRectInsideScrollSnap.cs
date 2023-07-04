
using UnityEngine;
using UnityEngine.EventSystems;

namespace StardropTools.UI
{
    [RequireComponent(typeof(UnityEngine.UI.ScrollRect))]
    public class UIScrollRectInsideScrollSnap : MonoBehaviour //, IPointerDownHandler
    {
        [SerializeField] UIScrollSnap targetScrollSnap;
        [SerializeField] UnityEngine.UI.ScrollRect scrollRect;
        [SerializeField] GameObject selfObject;

        public UnityEngine.UI.ScrollRect ScrollRect => scrollRect;

        //public void OnPointerDown(PointerEventData eventData)
        //{
        //    targetScrollSnap.SelectedScrollRect(this);
        //}

        private void Start()
        {
            SingleInputManager.OnInputStart.AddListener(IsMouseOverScrollRect);
        }

        void IsMouseOverScrollRect()
        {
            if (SingleInputManager.Instance.ContainsObject(selfObject))
                targetScrollSnap.SelectedScrollRect(this);
        }

        public bool CheckIfPassable(SwipeManager.SwipeDirection swipeDirection, float min, float max)
        {
            float horizontal = scrollRect.horizontalNormalizedPosition;
            float vertical = scrollRect.verticalNormalizedPosition;

            bool canPass = false;

            if (swipeDirection == SwipeManager.SwipeDirection.left || swipeDirection == SwipeManager.SwipeDirection.right)
            {
                if (scrollRect.horizontal == true && horizontal <= min || horizontal >= max)
                    canPass = true;
            }

            if (swipeDirection == SwipeManager.SwipeDirection.up || swipeDirection == SwipeManager.SwipeDirection.down)
            {
                if (scrollRect.vertical == true && vertical <= min || vertical >= max)
                    canPass = true;
            }
            

            //print(name + ", is passible = " + canPass);
            return canPass;
        }

        public void SetTargetScrollSnap(UIScrollSnap scrollSnap)
        {
            targetScrollSnap = scrollSnap;
            selfObject = gameObject;
            GetScrollRect();
        }

        public void GetScrollRect()
        {
            if (scrollRect == null)
                scrollRect = GetComponent<UnityEngine.UI.ScrollRect>();
        }
    }
}
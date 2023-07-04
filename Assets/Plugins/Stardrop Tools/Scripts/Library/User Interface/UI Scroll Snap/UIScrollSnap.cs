
using UnityEngine;
using UnityEngine.EventSystems;

namespace StardropTools.UI
{
    public class UIScrollSnap : BaseUIObject, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [Header("Properties")]
        [SerializeField] bool isButtonOnly;
        [Space]
        public int index    = 0;
        public int priority = 1;
        [Space]
        public int currentIndex = 0;
        [SerializeField] int nextIndex  = 0;
        [SerializeField] int startIndex = 0;
        [Space]
        public bool canSwipeNextScrollSnap;
        [SerializeField] bool disableHidden = true;
        [SerializeField] bool debug;

        [Header("Animation")]
        [SerializeField] UIOrientation  orientation;
        [SerializeField] AnimationCurve animCurve;
        [SerializeField] float duration = .2f;

        [Header("Elements")]
        [SerializeField] System.Collections.Generic.List<RectTransform> rectsToIgnore;
        [SerializeField] System.Collections.Generic.List<UIScrollSnapElement> elements;
#if UNITY_EDITOR
        [SerializeField] bool getElements;
        [SerializeField] bool reorderElements;
#endif

        [Header("Buttons")]
        [SerializeField] Transform parentButtons;
        [SerializeField] System.Collections.Generic.List<UIToggleButton> buttons;
#if UNITY_EDITOR
        [SerializeField] bool getButtons;
#endif

        [Header("Scroll Rect")]
        [SerializeField] UIScrollRectInsideScrollSnap selectedScrollRect;
        [SerializeField] UIScrollRectInsideScrollSnap[] scrollRects;
#if UNITY_EDITOR
        [SerializeField] bool getScrollRects;
        [SerializeField] bool removeScrollRects;
#endif

        [Header("Enable or Disable at Index")]
        [SerializeField] System.Collections.Generic.List<UIScrollSnapEnableAtIndex> enableAtIndex;
        [SerializeField] System.Collections.Generic.List<UIScrollSnapEnableAtIndex> disableAtIndex;

        Coroutine animCR;
        int direction;
        System.Guid uniqueID;

        public System.Guid UniqueID { get => uniqueID; }

        public readonly EventHandler SwipeToNextPriority = new EventHandler();

        public readonly EventHandler OnMoveStart    = new EventHandler();
        public readonly EventHandler OnMoving       = new EventHandler();
        public readonly EventHandler OnMoveComplete = new EventHandler();

        public readonly EventHandler<int> OnIndexChanged = new EventHandler<int>();


        protected override void Start()
        {
            base.Start();

            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            uniqueID = System.Guid.NewGuid();

            if (buttons != null && buttons.Count > 0)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].ButtonID = i;
                    buttons[i].Initialize();
                    buttons[i].OnClickID.AddListener(MoveToIndex);
                    buttons[i].Toggle(false);
                }

                buttons[startIndex].Toggle(true);
            }

            MoveToIndex(startIndex, true);

            SwipeToNextPriority.AddListener(() => UIScrollSnapManager.Instance.MoveNext(this, direction));
        }

        // Add to manager & let manager decide
        // which ScrollSnap to subscribe to swipe,
        // based on priority
        public void OnPointerDown(PointerEventData eventData)
            => PointerEnter();

        // Remove from manager
        public void OnPointerExit(PointerEventData eventData)
            => PointerExit();
        public void OnPointerUp(PointerEventData eventData)
            => PointerExit();

        void PointerEnter()
        {
            if (IsInitialized == false)
                Initialize();

            if (isButtonOnly == false)
                SwipeManager.OnSwipeDirection.AddListener(SwipeToIndex);
            //ScrollSnapManager.Instance.AddToList(this);
        }

        void PointerExit()
        {
            // Delay listener removal so that we can register the swipe event
            //ScrollSnapManager.Instance.RemoveFromList(this, true);

            if (isButtonOnly == false)
                DelayedSwipeUnsubscribe();
        }

        // Delay listener removal so that we can register the swipe event
        public void DelayedSwipeUnsubscribe()
        {
            CancelInvoke();
            Invoke(nameof(UnsubscribeSwipe), .2f);
        }

        public void UnsubscribeSwipe()
        {
            SwipeManager.OnSwipeDirection.RemoveListener(SwipeToIndex);
            selectedScrollRect = null;
        }

        public void SwipeToIndex(SwipeManager.SwipeDirection swipeDirection)
        {
            //if (debug)
            //    Debug.Log("Swiped to: " + direction);

            if (orientation == UIOrientation.Horizontal)
            {
                switch (swipeDirection)
                {
                    case SwipeManager.SwipeDirection.left:
                        if (orientation == UIOrientation.Horizontal)
                            direction = 1;
                        break;
                    case SwipeManager.SwipeDirection.right:
                        if (orientation == UIOrientation.Horizontal)
                            direction = -1;
                        break;
                }
            }

            if (orientation == UIOrientation.Vertical)
            {
                switch (swipeDirection)
                {
                    case SwipeManager.SwipeDirection.up:
                        if (orientation == UIOrientation.Vertical)
                            direction = -1;
                        break;
                    case SwipeManager.SwipeDirection.down:
                        if (orientation == UIOrientation.Vertical)
                            direction = 1;
                        break;
                }
            }

            if (selectedScrollRect != null && selectedScrollRect.CheckIfPassable(swipeDirection, .01f, .99f) == false)
                return;

            NextDirection(direction);
        }


        void NextDirection(int direction)
        {
            nextIndex = Mathf.Clamp(currentIndex + direction, -1, elements.Count);

            if (nextIndex == -1 || nextIndex == elements.Count)
            {
                SwipeToNextPriority?.Invoke();
                return;
            }

            MoveToIndex(nextIndex, direction);
        }

        void SetCurrentIndex(int index)
        {
            currentIndex = Mathf.Clamp(index, 0, elements.Count - 1);
            OnIndexChanged?.Invoke(currentIndex);

            if (enableAtIndex.Exists())
                for (int i = 0; i < enableAtIndex.Count; i++)
                    if (enableAtIndex[i].Index == index)
                        enableAtIndex[i].gameObject.SetActive(true);

            if (disableAtIndex.Exists())
                for (int i = 0; i < disableAtIndex.Count; i++)
                    if (disableAtIndex[i].Index == index)
                        disableAtIndex[i].gameObject.SetActive(false);
        }

        public void MoveToIndex(int index)
            => MoveToIndex(index, false);

        public void MoveToIndex(int index, bool ignoreSameIndex = false)
        {
            if (ignoreSameIndex == false)
            {
                index = Mathf.Clamp(index, 0, elements.Count - 1);
                if (index == currentIndex)
                    return;
            }

            if (index > currentIndex)
                MoveToIndex(index, 1, true);
            else if (index < currentIndex)
                MoveToIndex(index, -1, true);
        }

        /// <summary>
        /// Hide Current Element & Show Next in direction (width or height * direction (1 or -1))
        /// </summary>
        /// <param name="index"> Element Index to show </param>
        /// <param name="direction"> width or height * direction (1 or -1) </param>
        public void MoveToIndex(int index, int direction, bool ignoreSameIndex = false)
        {
            if (ignoreSameIndex == false)
            {
                index = Mathf.Clamp(index, 0, elements.Count - 1);
                if (index == currentIndex)
                    return;
            }

            if (debug)
                Debug.Log("Index: " + index);

            // start animation
            if (animCR != null)
                StopCoroutine(animCR);
            animCR = StartCoroutine(MoveCR(index, direction));

            RefreshButtons();
        }

        System.Collections.IEnumerator MoveCR(int index, int direction)
        {
            // 1-send target index to center
            // 2-move current index in direction

            OnMoveStart?.Invoke();

            float t = 0;
            float percent = 0;
            float eval = 0;

            UIScrollSnapElement elementToShow = elements[index];
            UIScrollSnapElement elementToHide = elements[currentIndex];
            SetCurrentIndex(index);

            // Reposition element to Show, instead of showing all elements until we reach target
            // this makes it look as if it was just next to each other
            if (orientation == UIOrientation.Horizontal)
            {
                Vector2 showPos = new Vector2(elementToHide.AnchoredPosition.x + elementToShow.WidthRect * direction, elementToHide.AnchoredPosition.y);
                elementToShow.SetAnchoredPosition(showPos);
            }

            else if (orientation == UIOrientation.Vertical)
            {
                Vector2 showPos = new Vector2(elementToHide.AnchoredPosition.x, elementToHide.AnchoredPosition.y + elementToShow.HeightRect * direction);
                elementToShow.SetAnchoredPosition(showPos);
            }

            if (elementToShow.GameObject.activeInHierarchy == false)
                elementToShow.SetActive(true);

            if (debug)
                Debug.Log("<color=yellow>From: </color>" + elementToHide.name + ", <color=yellow>To: </color>" + elementToShow.name);

            // Get start anchored positions for lerping purposes
            Vector2 showStartPos = elementToShow.AnchoredPosition;
            Vector2 hideStartPos = elementToHide.AnchoredPosition;
            Vector2 showLerpPos, hideLerpPos;

            // get old Target Pos
            Vector2 oldTargetPos = Vector2.zero;
            if (orientation == UIOrientation.Horizontal) // width
                oldTargetPos = Vector2.right * elementToHide.WidthRect * direction * -1; // inverse direction because of position

            else if (orientation == UIOrientation.Vertical) // height
                oldTargetPos = Vector2.up * elementToHide.HeightRect * direction * -1; // inverse direction because of position

            while (t < duration)
            {
                percent = t / duration;
                eval = animCurve.Evaluate(percent);

                // set target pos
                showLerpPos = Vector2.Lerp(showStartPos, Vector2.zero, eval);
                elementToShow.SetAnchoredPosition(showLerpPos);

                // set old pos
                hideLerpPos = Vector2.Lerp(hideStartPos, oldTargetPos, eval);
                elementToHide.SetAnchoredPosition(hideLerpPos);

                // have to do this because of a bug
                if (currentIndex < buttons.Count)
                    buttons[currentIndex].Toggle(true);

                t += Time.deltaTime;
                OnMoving?.Invoke();
                yield return null;
            }

            elementToShow.SetAnchoredPosition(Vector2.zero);
            elementToHide.SetAnchoredPosition(oldTargetPos);

            if (disableHidden)
                elementToHide.SetActive(false);

            OnMoveComplete?.Invoke();
        }

        void SetElementPosition(UIScrollSnapElement element, int direction)
        {
            // inverse direction because of coordenates
            direction *= -1;

            if (orientation == UIOrientation.Horizontal) // width
                element.SetAnchoredPosition(Vector2.right * element.WidthRect * direction);

            else if (orientation == UIOrientation.Vertical) // height
                element.SetAnchoredPosition(Vector2.up * element.HeightRect * direction);
        }

        public void ReorderElements()
        {
            elements[0].SetAnchoredPosition(Vector2.zero);
            elements[0].index = 0;

            for (int i = 1; i < elements.Count; i++)
            {
                if (orientation == UIOrientation.Horizontal)
                    elements[i].SetAnchoredPosition(elements[i].WidthRect, 0);
                else if (orientation == UIOrientation.Vertical)
                    elements[i].SetAnchoredPosition(0, elements[i].HeightRect);

                elements[i].index = i;
            }
        }

        void RefreshButtons()
        {
            if (buttons.Count == 0)
                return;

            for (int i = 0; i < buttons.Count; i++)
                if (i != currentIndex)
                    buttons[i].Toggle(false);

            if (currentIndex < buttons.Count)
                buttons[currentIndex].Toggle(true);
        }

        void GetScrollRects()
        {
            var scrollRectArray = GetComponentsInChildren<UnityEngine.UI.ScrollRect>();
            scrollRects = new UIScrollRectInsideScrollSnap[scrollRectArray.Length];

            for (int i = 0; i < scrollRects.Length; i++)
            {
                UIScrollRectInsideScrollSnap uiScrollRect = scrollRectArray[i].GetComponent<UIScrollRectInsideScrollSnap>();

                if (uiScrollRect == null)
                    uiScrollRect = scrollRectArray[i].gameObject.AddComponent<UIScrollRectInsideScrollSnap>();

                uiScrollRect.SetTargetScrollSnap(this);
                scrollRects[i] = uiScrollRect;
            }
        }

        void RemoveScrollRects()
        {
            for (int i = 0; i < scrollRects.Length; i++)
                DestroyImmediate(scrollRects[i]);

            scrollRects = new UIScrollRectInsideScrollSnap[0];
        }

        public void SelectedScrollRect(UIScrollRectInsideScrollSnap selected)
        {
            if (selected == selectedScrollRect)
                return;

            selectedScrollRect = selected;
        }


        public void AddToEnableAtIndex(int index, GameObject gameObject)
        {
            for (int i = 0; i < enableAtIndex.Count; i++)
                if (enableAtIndex[i].gameObject == gameObject)
                    return;

            UIScrollSnapEnableAtIndex enable = new UIScrollSnapEnableAtIndex(index, gameObject);
            enableAtIndex.Add(enable);
        }

        public void RemoveEnableAtIndex(GameObject gameObject)
        {
            for (int i = 0; i < enableAtIndex.Count; i++)
                if (enableAtIndex[i].gameObject == gameObject)
                    enableAtIndex.Remove(enableAtIndex[i]);
        }



        public void AddToDisableAtIndex(int index, GameObject gameObject)
        {
            for (int i = 0; i < disableAtIndex.Count; i++)
                if (disableAtIndex[i].gameObject == gameObject)
                    return;

            UIScrollSnapEnableAtIndex enable = new UIScrollSnapEnableAtIndex(index, gameObject);
            disableAtIndex.Add(enable);
        }

        public void RemoveDisableAtIndex(GameObject gameObject)
        {
            for (int i = 0; i < disableAtIndex.Count; i++)
                if (disableAtIndex[i].gameObject == gameObject)
                    disableAtIndex.Remove(disableAtIndex[i]);
        }


#if UNITY_EDITOR
        void GetElements()
        {
            var children = Utilities.GetListComponentsInChildren<RectTransform>(transform);
            var copyElements = new System.Collections.Generic.List<RectTransform>();
            elements = new System.Collections.Generic.List<UIScrollSnapElement>();

            for (int i = 0; i < children.Count; i++)
            {
                if (rectsToIgnore.Contains(children[i]))
                    continue;

                // get or set Scroll Snap Element component
                UIScrollSnapElement element = children[i].GetComponent<UIScrollSnapElement>();
                
                if (element == null)
                    element = (children[i].gameObject.AddComponent<UIScrollSnapElement>());

                element.isFirst = false;
                element.isLast = false;

                copyElements.Add(element.RectTransform);
                elements.Add(element);
            }

            elements.GetFirst().isFirst = true;
            elements.GetLast().isLast   = true;

            UtilitiesUI.CopySizeRects(RectTransform, copyElements);
        }

        void GetButtons()
        {
            if (parentButtons == null)
                return;

            buttons = parentButtons.GetComponentsInChildren<UIToggleButton>().ToList();

            for (int i = 0; i < buttons.Count; i++)
                buttons[i].ButtonID = i;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (getElements)
            {
                GetElements();
                getElements = false;
            }

            if (reorderElements)
            {
                ReorderElements();
                reorderElements = false;
            }

            if (getButtons)
            {
                GetButtons();
                getButtons = false;
            }

            if (getScrollRects)
            {
                GetScrollRects();
                getScrollRects = false;
            }

            if (removeScrollRects)
            {
                RemoveScrollRects();
                removeScrollRects = false;
            }
        }
#endif
    }
}
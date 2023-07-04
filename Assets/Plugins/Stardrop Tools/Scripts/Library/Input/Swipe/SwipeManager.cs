
using UnityEngine;
using StardropTools;

/// <summary>
/// Input Class focused on user swipe input (pc or mobile)
/// </summary>
public class SwipeManager : Singleton<SwipeManager>, IUpdateable
{
    public enum SwipeDirection
    {
        none,

        left,
        right,

        up,
        upLeft,
        upRight,

        down,
        downLeft,
        downRight
    }

    [Header("Swipe Params")]
    [SerializeField] Vector2 screenSize;
    [SerializeField] float screenAvg = 0;
    [SerializeField] float screenPercentTarget = .05f;
    [Space]
    [SerializeField] float minSwipeDistance = 80;
    [SerializeField] float swipeTime = .2f;

    [Header("Internal Variables")]
    [SerializeField] bool hasInput;
    [Space]
    [SerializeField] float deltaDragTime;
    [SerializeField] float dragDistance;

    float startPosX;
    float startPosY;

    float endPosX;
    float endPosY;

    float distanceX;
    float distanceY;

    SwipeData data;

    [Header("Result")]
    public SwipeDirection swipeDirection;
    [SerializeField] bool debug;

    #region Events

    public static readonly EventHandler<SwipeData> OnSwipe = new EventHandler<SwipeData>();
    public static readonly EventHandler<SwipeDirection> OnSwipeDirection = new EventHandler<SwipeDirection>();

    public static readonly EventHandler<int> OnSwipeHorizontal = new EventHandler<int>();
    public static readonly EventHandler<int> OnSwipeVertical = new EventHandler<int>();

    public static readonly EventHandler OnSwipeUp = new EventHandler();
    public static readonly EventHandler OnSwipeDown = new EventHandler();

    public static readonly EventHandler OnSwipeLeft = new EventHandler();
    public static readonly EventHandler OnSwipeRight = new EventHandler();

    public static readonly EventHandler OnSwipeUpLeft = new EventHandler();
    public static readonly EventHandler OnSwipeUpRight = new EventHandler();

    public static readonly EventHandler OnSwipeDownLeft = new EventHandler();
    public static readonly EventHandler OnSwipeDownRight = new EventHandler();

    #endregion // events

    protected override void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        CalculateScreenPercent();

        StartUpdate();
    }

    public void CheckSwipe()
    {
        #region Standalone
        if (Input.GetMouseButton(0))
        {
            if (hasInput == false)
            {
                ResetValues();
                deltaDragTime = 0;

                startPosX = Input.mousePosition.x;
                startPosY = Input.mousePosition.y;

                hasInput = true;
            }

            else if (hasInput && deltaDragTime < swipeTime)
            {
                deltaDragTime += Time.deltaTime;

                endPosX = Input.mousePosition.x;
                endPosY = Input.mousePosition.y;

                distanceX = endPosX - startPosX;
                distanceY = endPosY - startPosY;

                dragDistance = Vector2.Distance(new Vector2(startPosX, startPosY), new Vector2(endPosX, endPosY));
            }
        }

        else
        {
            if (hasInput == true)
            {
                Swipe();
                hasInput = false;
            }
        }
        #endregion // standalone
    }

    public void Swipe()
    {
        var touchVector = new Vector2(distanceX, distanceY);

        if (touchVector.magnitude > minSwipeDistance && deltaDragTime <= swipeTime)
        {
            float angle = Mathf.Atan2(distanceY, distanceX) / Mathf.PI;

            if (debug) Debug.Log("Swipe angle = " + angle);

            //-------- 4 directions
            // left
            if (angle < -0.875f || angle > 0.875f)
            {
                swipeDirection = SwipeDirection.left;
                OnSwipeHorizontal?.Invoke(1);
                OnSwipeLeft?.Invoke();
            }

            // right
            else if (angle > -0.125f && angle < 0.125f)
            {
                swipeDirection = SwipeDirection.right;
                OnSwipeHorizontal?.Invoke(-1);
                OnSwipeRight?.Invoke();
            }

            // up
            if (angle > 0.375f && angle < 0.625f)
            {
                swipeDirection = SwipeDirection.up;
                OnSwipeVertical?.Invoke(-1);
                OnSwipeUp?.Invoke();
            }

            // down
            else if (angle < -0.375f && angle > -0.625f)
            {
                swipeDirection = SwipeDirection.down;
                OnSwipeVertical?.Invoke(1);
                OnSwipeDown?.Invoke();
            }



            //-------- 4 diagonal directions

            // up left
            if (angle > 0.625f && angle < 0.875f)
            {
                swipeDirection = SwipeDirection.upLeft;
                OnSwipeUpLeft?.Invoke();
            }

            // up right
            else if (angle > 0.125f && angle < 0.375f)
            {
                swipeDirection = SwipeDirection.upRight;
                OnSwipeUpRight?.Invoke();
            }

            // down left
            else if (angle < -0.625f && angle > -0.875f)
            {
                swipeDirection = SwipeDirection.downLeft;
                OnSwipeDownLeft?.Invoke();
            }

            // down right
            else if (angle < -0.125f && angle > -0.375f)
            {
                swipeDirection = SwipeDirection.downRight;
                OnSwipeDownRight?.Invoke();
            }

            // Create swipe data
            GenerateData();

            // Invoke Swipe Event
            OnSwipe?.Invoke(data);
            OnSwipeDirection?.Invoke(swipeDirection);

            if (debug)
                Debug.Log("Swiped: " + swipeDirection);
        }
    }

    SwipeData GenerateData()
    {
        Vector2 startPos = new Vector2(startPosX, startPosY);
        Vector2 endPos = new Vector2(endPosX, endPosY);
        Vector2 direction = data.endPoint - data.startPoint;
        data = new SwipeData(swipeDirection, startPos, endPos, direction);

        return data;
    }

    private void ResetValues()
    {
        swipeDirection = SwipeDirection.none;
        hasInput = false;
        deltaDragTime = 0;

        startPosX = 0;
        startPosY = 0;

        distanceX = 0;
        distanceY = 0;

        data = new SwipeData();
    }

    void CalculateScreenPercent()
    {
        screenAvg = (screenSize.x + screenSize.y) * .5f;
        minSwipeDistance = screenAvg * screenPercentTarget;
    }

    public static SwipeDirection GetRandomSwipeDirection()
        => (SwipeDirection)Random.Range(0, 9);

    public static SwipeDirection GetRandomCardinalSwipeDirection()
    {
        int rand = Random.Range(0, 4);

        if (rand == 0)
            return SwipeDirection.down;
        else if (rand == 1)
            return SwipeDirection.up;
        else if (rand == 2)
            return SwipeDirection.left;
        else
            return SwipeDirection.right;
    }

    public static SwipeDirection GetRandomDiagonalSwipeDirection()
    {
        int rand = Random.Range(0, 4);

        if (rand == 0)
            return SwipeDirection.downLeft;
        else if (rand == 1)
            return SwipeDirection.downRight;
        else if (rand == 2)
            return SwipeDirection.upLeft;
        else
            return SwipeDirection.upRight;
    }


    public void StartUpdate() => LoopManager.AddToUpdate(this);

    public void StopUpdate() => LoopManager.RemoveFromUpdate(this);

    public void HandleUpdate() => CheckSwipe();
}

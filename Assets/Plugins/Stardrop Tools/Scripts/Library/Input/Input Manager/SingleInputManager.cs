
using UnityEngine;
using UnityEngine.EventSystems;
using StardropTools;

/// <summary>
/// Input Class focused on single finger/mouse input. Designed for mobile usage
/// </summary>
public class SingleInputManager : Singleton<SingleInputManager>, IUpdateable
{
    /// <summary>
    /// 0-joystick center is finger input point, 1-screen center only applies to Horizontal, 2-screen center only applies to Vertical, 3-screen center is dead center
    /// </summary>
    public enum InputCenter { dynamicJoystick, screenCenterHorizontal, screenCenterVertical, screenCenterComplete }

    [Header("Legacy Input")]
    [SerializeField] InputCenter inputCenter;
    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    [Space]
    [SerializeField] bool hasInput;
    [SerializeField] bool isOverUI;

    [Header("Debug")]
    [SerializeField] Vector2 screenSize;
    [SerializeField] float screenAvg = 0;
    [SerializeField] float maxScreenDistance = 100f;
    [SerializeField] float screenPercentTarget = .1f;
    [SerializeField] bool calculateScreenPercent;
    [Space]
    [SerializeField] Vector2 inputStart;
    [SerializeField] Vector2 inputCurrent;
    [Space]
    [SerializeField] float distanceX;
    [SerializeField] float distanceY;
    [SerializeField] float distance;

    [Header("Raycast")]
    [SerializeField] UnityEngine.EventSystems.EventSystem eventSystem;
    [SerializeField] new Camera camera;
    [SerializeField] LayerMask maskPick;
    [SerializeField] LayerMask maskGround;

    public Vector3 DirectionXZ { get => new Vector3(horizontal, 0, vertical); }

    
    private Ray ray;
    RaycastHit hit;
    public System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> raycastList;


    #region Parameters
    public bool HasInput { get => hasInput; }
    public bool IsOverUI { get => isOverUI; }

    public float Horizontal { get => horizontal; }
    public float Vertical { get => vertical; }

    public Vector2 InputStart { get => inputStart; }
    public Vector2 InputCurrent { get => inputCurrent; }

    public Ray CameraRay { get => ray; }
    public RaycastHit RaycastHit { get => hit; }
    public System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> uiRaycastList { get => raycastList; }
    #endregion // params

    #region Events

    // ---------- !! ADD EVENTS FOR VALUES: HORIZONTAL, VERTICAL, INPUTSTART, ETC
    public static readonly EventHandler OnInputStart = new EventHandler();
    public static readonly EventHandler OnInputEnd = new EventHandler();
    public static readonly EventHandler OnInput = new EventHandler();

    public static readonly EventHandler OnInputStartUI = new EventHandler();
    public static readonly EventHandler OnInputEndUI = new EventHandler();

    public static readonly EventHandler<float> OnInputHorizontal = new EventHandler<float>();
    public static readonly EventHandler<float> OnInputVertical = new EventHandler<float>();

    public static readonly EventHandler<float> OnInputDistance = new EventHandler<float>();
    #endregion // events

    protected override void Awake()
    {
        base.Awake();

        if (camera == null)
            camera = Camera.main;

        screenSize = new Vector2(Screen.width, Screen.height);
        CalculateScreenPercent();

        StartUpdate();
    }

    public void UserInput()
    {
        if (camera == null)
            camera = Camera.main;

        // Input start
        if (Input.GetMouseButtonDown(0))
        {
            if (hasInput == false)
                hasInput = true;

            inputStart = Input.mousePosition;

            if (isOverUI)
                OnInputStartUI?.Invoke();

            OnInputStart?.Invoke();
        }

        // continuous input
        if (Input.GetMouseButton(0))
        {
            inputCurrent = Input.mousePosition;

            distanceX = inputCurrent.x - inputStart.x;
            distanceY = inputCurrent.y - inputStart.y;

            distance = (inputCurrent - inputStart).magnitude;

            OnInputDistance?.Invoke(distance);
            OnInput?.Invoke();
        }

        // input end
        if (Input.GetMouseButtonUp(0))
        {
            if (hasInput == true)
                hasInput = false;

            if (isOverUI)
                OnInputEndUI?.Invoke();

            OnInputEnd?.Invoke();
        }

        if (hasInput && calculateScreenPercent)
            CalculateScreenPercent();

        InputFilter();

        //Raycast();
        //RaycastUI();
    }

    void InputFilter()
    {
        if (inputCenter == InputCenter.dynamicJoystick)
            InputJoystick();
        else
            InputScreenCenter();
    }

    void InputJoystick()
    {
        if (inputCenter != InputCenter.dynamicJoystick)
            return;

        if (hasInput)
        {
            horizontal = Mathf.Clamp(distanceX / maxScreenDistance, -1, 1);
            vertical = Mathf.Clamp(distanceY / maxScreenDistance, -1, 1);

            InvokeFloatEvents();
        }

        else
        {
            horizontal = 0;
            vertical = 0;
        }
    }
     
    void InputScreenCenter()
    {
        if (hasInput)
        {
            // Center only for Horizontal
            if (inputCenter == InputCenter.screenCenterHorizontal)
            {
                horizontal = (inputCurrent.x / Screen.width) * 2 - 1;
                vertical = Mathf.Clamp(distanceY / maxScreenDistance, -1, 1);
            }

            // Center only for Vertical
            else if (inputCenter == InputCenter.screenCenterVertical)
            {
                horizontal = Mathf.Clamp(distanceX / maxScreenDistance, -1, 1);
                vertical = (inputCurrent.y / Screen.height) * 2 - 1;
            }

            // Dead center
            else if (inputCenter == InputCenter.screenCenterComplete)
            {
                horizontal = (inputCurrent.x / Screen.width) * 2 - 1;
                vertical = (inputCurrent.y / Screen.height) * 2 - 1;
            }

            InvokeFloatEvents();
        }

        else
        {
            horizontal = 0;
            vertical = 0;
        }
    }

    void InvokeFloatEvents()
    {
        OnInputHorizontal?.Invoke(horizontal);
        OnInputVertical?.Invoke(vertical);
    }

    void CalculateScreenPercent()
    {
        screenAvg = (screenSize.x + screenSize.y) * .5f;
        maxScreenDistance = screenAvg * screenPercentTarget;
    }


    void Raycast() => ray = camera.ScreenPointToRay(Input.mousePosition);

    public bool CheckIfOverUI()
    {
        isOverUI = eventSystem.IsPointerOverGameObject();
        return isOverUI;
    }

    public void RaycastUI()
    {
        if (eventSystem == null)
            eventSystem = EventSystem.current;

        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        raycastList = new System.Collections.Generic.List<RaycastResult>();
        eventSystem.RaycastAll(pointerEventData, raycastList);

        if (raycastList.Count == 0 && isOverUI == true)
            isOverUI = false;

        if (raycastList.Count > 0 && isOverUI == false)
            isOverUI = true;
    }

    public bool ContainsObject(GameObject objectToCheck)
    {
        RaycastUI();

        if (raycastList.Count > 0)
            for (int i = 0; i < raycastList.Count; i++)
            {
                if (raycastList[i].gameObject == objectToCheck)
                    return true;
            }

        return false;
    }


    public void StartUpdate() => LoopManager.AddToUpdate(this);

    public void StopUpdate() => LoopManager.RemoveFromUpdate(this);

    public void HandleUpdate() => UserInput();
}

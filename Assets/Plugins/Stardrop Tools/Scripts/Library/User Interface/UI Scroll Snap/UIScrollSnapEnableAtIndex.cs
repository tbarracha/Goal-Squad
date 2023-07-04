
using UnityEngine;

public class UIScrollSnapEnableAtIndex : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] GameObject targetObject;

    public int Index => index;
    public GameObject TargetObject => targetObject;

    public UIScrollSnapEnableAtIndex(int index, GameObject targetObject)
    {
        this.index = index;
        this.targetObject = targetObject;
    }
}
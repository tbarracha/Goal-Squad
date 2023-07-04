
using UnityEngine;

public class BaseInput : MonoBehaviour
{
    protected float horizontal;
    protected float vertical;
    protected bool hasInput;

    public float Horizontal => horizontal;
    public float Vertical => vertical;
    public bool HasInput => hasInput;
}


using UnityEngine;
using StardropTools;

public class RoomEnd : Room
{
    [SerializeField] Transform goalPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Transform confettiPoint;

    public Transform GoalPoint => goalPoint;
    public Transform PlayerEndPoint => endPoint;
    public Transform ConfettiPoint => confettiPoint;
}

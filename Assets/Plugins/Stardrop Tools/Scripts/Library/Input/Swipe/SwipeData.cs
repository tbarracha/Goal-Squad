

using UnityEngine;

[System.Serializable]
public struct SwipeData
{
    public SwipeManager.SwipeDirection swipeDirection;
    public Vector2 startPoint;
    public Vector2 endPoint;
    public Vector2 direction;
    public float distance;

    public SwipeData(SwipeManager.SwipeDirection swipeDirection, Vector2 startPoint, Vector2 endPoint, Vector2 direction)
    {
        this.swipeDirection = swipeDirection;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.direction = direction;
        distance = direction.magnitude;
    }
}
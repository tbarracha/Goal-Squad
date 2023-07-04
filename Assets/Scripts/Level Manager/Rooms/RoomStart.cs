
using UnityEngine;
using StardropTools;

public class RoomStart : Room
{
    [SerializeField] Transform playerStartPoint;

    public Transform PlayerStartPoint => playerStartPoint;
}

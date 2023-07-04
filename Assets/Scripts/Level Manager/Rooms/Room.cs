
using UnityEngine;
using StardropTools;
using System.Collections.Generic;

public abstract class Room : BaseObject
{
    [SerializeField] protected Transform nextRoomSpawnPoint;
    [SerializeField] protected Transform parentGridEdges;
    [SerializeField] protected Transform parentGridFour;
    [SerializeField] protected Transform parentGridFive;
    [Space]
    [SerializeField] protected Transform[] gridEdgePoints;
    [SerializeField] protected Transform[] gridFourPoints;
    [SerializeField] protected Transform[] gridFivePoints;

    public Transform NextRoomSpawnPoint => nextRoomSpawnPoint;

    public List<Transform> GetRandomGridEdgePoints(int amount) => gridEdgePoints.GetRandomNonRepeat(amount);
    public List<Transform> GetRandomGridFourPoints(int amount) => gridFourPoints.GetRandomNonRepeat(amount);
    public List<Transform> GetRandomGridFivePoints(int amount) => gridFivePoints.GetRandomNonRepeat(amount);


    protected override void OnValidate()
    {
        base.OnValidate();

        gridEdgePoints = Utilities.GetArrayComponentsInChildren<Transform>(parentGridEdges);
        gridFourPoints = Utilities.GetArrayComponentsInChildren<Transform>(parentGridFour);
        gridFivePoints = Utilities.GetArrayComponentsInChildren<Transform>(parentGridFive);
    }
}

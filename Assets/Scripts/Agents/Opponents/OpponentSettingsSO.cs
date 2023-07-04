
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Goal Squad / Settings / Opponent Settings")]
public class OpponentSettingsSO : ScriptableObject
{
    [Header("Default Settings")]
    [SerializeField] float moveSpeed            = 5;
    [SerializeField] float lookSpeed            = 10;
    [SerializeField] float maxSideEdge          = 7;
    [SerializeField] float ballDetectDistance   = 10;
    [Space]
    [SerializeField] float actionTime   = 1;
    [SerializeField] float accelTime    = .1f;
    [SerializeField] float deaccelTime  = .2f;

    [Header("Line Runner")]
    [SerializeField] float midPrediction            = 3;
    [SerializeField] float sidePrediction           = 3;
    [SerializeField] float farsidePrediction        = 5;
    [SerializeField] float sidePredictionDistance   = 5;

    [Header("Ping Pong")]
    [SerializeField] float minWPDistance = 2;
    [SerializeField] float[] pingPongWaypointsX = new float[] { -5, -2.5f, 0, 2.5f, 5 };
    [SerializeField] bool invertPingPongWaypoints;

    public float MoveSpeed          => moveSpeed;
    public float LookSpeed          => lookSpeed;
    public float MaxSideEdge        => maxSideEdge;
    public float BallDetectDistance => ballDetectDistance;

    public float ActionTime         => actionTime;
    public float AccelTime          => accelTime;
    public float DeaccelTime        => deaccelTime;


    // Line Runner
    public float MidPrediction      => midPrediction;       //moveSpeed * 5 / 12;
    public float SidePrediction     => sidePrediction;      //moveSpeed * 3 / 12;
    public float FarsidePrediction  => farsidePrediction;   //moveSpeed * 8 / 12;
    public float SidePredictionDistance => sidePredictionDistance;


    // Ping pong
    public float MinWaypointDistance => minWPDistance;
    public float[] GetRandomPingPongWayPointsX => pingPongWaypointsX.GetRandomNonRepeat(2).ToArray();
    public float GetRandomPingPoingWaypointX => pingPongWaypointsX.GetRandom();



    private void OnValidate()
    {
        if (invertPingPongWaypoints)
        {
            List<float> ppList = new List<float>(pingPongWaypointsX);
            List<float> inverted = new List<float>();

            for (int i = 0; i < ppList.Count; i++)
                if (ppList[i] != 0)
                    inverted.Add(pingPongWaypointsX[i] * -1);

            inverted.Add(0);

            for (int i = 0; ppList.Count > i; i++)
                if (ppList[i] != 0)
                    inverted.Add(pingPongWaypointsX[i]);

            pingPongWaypointsX = inverted.ToArray();

            invertPingPongWaypoints = false;
        }
    }
}
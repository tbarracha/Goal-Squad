using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Goal Squad / Settings / Ball Settings")]
public class BallSettingsSO : ScriptableObject
{
    [SerializeField] float passDuration     = .2f;
    [SerializeField] float kickSpeed        = 30;
    [SerializeField] float kickForce        = 1;
    [SerializeField] float rotationSpeed    = 30;

    public float PassDuration => passDuration;
    public float KickSpeed => kickSpeed;
    public float KickForce => kickForce;
    public float RotationSpeed => rotationSpeed;
}
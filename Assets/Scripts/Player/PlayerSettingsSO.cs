
using UnityEngine;

[CreateAssetMenu(menuName = "Goal Squad / Settings / Player Settings")]
public class PlayerSettingsSO : ScriptableObject
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 7;
    [SerializeField] float lookSpeed = 12;
    [Space]
    [SerializeField] float accelTime   = .3f;
    [SerializeField] float deaccelTime = .3f;
    [Space]
    [SerializeField] float kickMaxEdge = 8;
    [SerializeField] float kickSmoothness = 16;
    

    public float MoveSpeed => moveSpeed;
    public float LookSpeed => lookSpeed;
    
    public float AccelTime => accelTime;
    public float DeaccelTime => deaccelTime;

    public float KickMaxEdge => kickMaxEdge;
    public float KickSmoothness => kickSmoothness;

    
}

using UnityEngine;

[CreateAssetMenu(menuName = "Goal Squad / Settings / Agent Settings")]
public class AgentSettingsSO : ScriptableObject
{
    [SerializeField] float moveSpeed = 7;
    [SerializeField] float lookSpeed = 12;
    [Space]
    [SerializeField] float accelTime   = .3f;
    [SerializeField] float deaccelTime = .3f;

    public float MoveSpeed => moveSpeed;
    public float LookSpeed => lookSpeed;
    public float AccelTime => accelTime;
    public float DeaccelTime => deaccelTime;
}
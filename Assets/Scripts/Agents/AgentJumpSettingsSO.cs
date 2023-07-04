
using UnityEngine;

[CreateAssetMenu(menuName = "Goal Squad / Settings / Agent Jump Settings")]
public class AgentJumpSettingsSO : ScriptableObject
{
    [SerializeField] float jumpDuration     = .3f;
    [SerializeField] float minJumpHeight    = .25f;
    [SerializeField] float maxJumpHeight    = .5f;
    [SerializeField] AnimationCurve jumpCurve;

    public float JumpDuration => jumpDuration;
    public float RandomJumpHeight => Random.Range(minJumpHeight, maxJumpHeight);
    public AnimationCurve JumpCurve => jumpCurve;
}
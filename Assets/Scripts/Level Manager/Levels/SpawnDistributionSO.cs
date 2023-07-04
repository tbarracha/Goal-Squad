using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Goal Squad / Levels / Level Spawn Distribuition")]
public class SpawnDistributionSO : ScriptableObject
{
    [SerializeField] AnimationCurve[] spawnCurves;
}

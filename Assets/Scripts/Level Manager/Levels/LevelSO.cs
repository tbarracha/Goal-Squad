
using UnityEngine;
using StardropTools;

[CreateAssetMenu(menuName = "Goal Squad / Levels / Level ")]
public class LevelSO : ScriptableObject
{
    [SerializeField] int roomCount = 2;
    [SerializeField] int minOpponentsPerRoom = 2;
    [SerializeField] int maxOpponentsPerRoom = 4;
    [SerializeField] WeightedList<int> opponentTypes;
    [SerializeField] AnimationCurve spawnDistribuition;
    [SerializeField] SpawnDistributionSO spawnDistributionSO;

    public int RoomCount => roomCount;
    public int MinOpponentsPerRoom => minOpponentsPerRoom;
    public int MaxOpponentsPerRoom => maxOpponentsPerRoom;
    public int RandomOpponentCount => Random.Range(minOpponentsPerRoom, maxOpponentsPerRoom + 1);

    public int GetOpponentType() => opponentTypes.GetRandom();
    public int OpponentCountPercent(float percet) => Mathf.CeilToInt(Mathf.Lerp(minOpponentsPerRoom, maxOpponentsPerRoom, spawnDistribuition.Evaluate(percet)));
}

using UnityEngine;

[CreateAssetMenu(menuName = "Goal Squad / Levels / Level Database ")]
public class LevelDBSO : ScriptableObject
{
    //[NaughtyAttributes.Expandable]
    [SerializeField] LevelSO[] levels;

    public LevelSO GetLevelByIndex(int levelIndex) => levels[UtilsArray.ClampIntToArrayLength(levelIndex, levels)];

    public int GetNextLevelIndex(int currentIndex) => UtilsArray.ClampIntToArrayLength(currentIndex + 1, levels);
}
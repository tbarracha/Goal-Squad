using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Goal Squad / Settings / Level Manager Settings")]
public class LevelManagerSettingsSO : ScriptableObject
{
    [SerializeField] int minRooms = 1;
    [SerializeField] int maxRooms = 5;
    [SerializeField] float minConfettiSpawnTime = .2f;
    [SerializeField] float maxConfettiSpawnTime = .2f;

    public int MinRooms => minRooms;
    public int MaxRooms => maxRooms;
    public int RandomRoomCount => Random.Range(minRooms, maxRooms);

    public float MinConfettiSpawnTime => minConfettiSpawnTime;
    public float MaxConfettiSpawnTime => maxConfettiSpawnTime;
    public float RandomConfettiSpawnTime => Random.Range(minConfettiSpawnTime, maxConfettiSpawnTime);
}
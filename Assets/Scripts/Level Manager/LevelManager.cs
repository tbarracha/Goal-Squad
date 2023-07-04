
using StardropTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseManager
{
    [NaughtyAttributes.Expandable][SerializeField] LevelManagerSettingsSO settings;
    [SerializeField] LevelState levelState;
    [Space]
    [NaughtyAttributes.Expandable][SerializeField] LevelDBSO levelDatabase;
    [SerializeField] int levelIndex = 0;
    [Space]
    [SerializeField] Transform parentRooms;
    [SerializeField] Transform parentOpponents;
    [SerializeField] Vector2 opponentRotation;
    [Space]
    [SerializeField] List<Room> listRooms;
    [SerializeField] List<Opponent> listOpponents;

    PoolManager poolManager;
    Coroutine confettiCR;
    bool canGenerateNext;


    public RoomStart RoomStart  => listRooms.GetFirst() as RoomStart;
    public RoomEnd RoomEnd      => listRooms.GetLast() as RoomEnd;

    public Vector3 PlayerStartPosition  => RoomStart.PlayerStartPoint.position;
    public Vector3 PlayerEndPosition    => RoomEnd.PlayerEndPoint.position;
    public Vector3 GoalPosition         => RoomEnd.GoalPoint.position;
    public Vector3 ConfettiPosition     => RoomEnd.ConfettiPoint.position;


    public static readonly EventHandler<Vector3, Vector3, Vector3> OnPlayerPositionsGenerated = new EventHandler<Vector3, Vector3, Vector3>();
    public static readonly EventHandler<LevelState> OnChangedLevelState = new EventHandler<LevelState>();
    public static readonly EventHandler OnLevelReset = new EventHandler();


    public override void LateInitialize()
    {
        base.LateInitialize();
        poolManager = PoolManager.Instance;
    }

    protected override void EventFlow()
    {
        GameManager.OnPoolsPopulated.AddListener(() => GenerateLevel(true));
        GameManager.OnGenerating.AddListener(()     => GenerateLevel(true));

        GameManager.OnPlayStart.AddListener(()      => ChangeLevelState(LevelState.Move));
        Player.OnPlayerReachGoal.AddListener(()     => ChangeLevelState(LevelState.Kick));
        GameManager.OnPlayEnd.AddListener(()        => ChangeLevelState(LevelState.Finish));

        GameManager.OnNextLevel.AddListener(NextLevel);
        GameManager.OnWin.AddListener(SpawnConfetti);

        GameManager.OnPlayEnd.AddListener(() => canGenerateNext = true);

        GameManager.OnRestart.AddListener(ResetComponent);
    }

    protected override void ResetComponent()
    {
        ResetOpponents();
    }

    void ChangeLevelState(LevelState targetState)
    {
        if (levelState == targetState) return;
        levelState = targetState;

        if (levelState == LevelState.Move)
            StartOpponents();

        if (levelState == LevelState.Kick)
            IdleOpponents();

        if (levelState == LevelState.Finish)
            StopOpponents();

        OnChangedLevelState?.Invoke(levelState);
    }


    void ClearSpawned()
    {
        poolManager.ClearRooms();
        poolManager.ClearOpponents();

        listRooms = new List<Room>();
        listOpponents = new List<Opponent>();
    }

    
    void GenerateLevel(bool spawnOpponents)
    {
        StopAllCoroutines();
        ChangeLevelState(LevelState.Generating);
        ClearSpawned();

        LevelSO level = levelDatabase.GetLevelByIndex(levelIndex);
        int roomCount = level.RoomCount;

        Vector3 spawnLocation = parentRooms.position;
        RoomStart roomStart = poolManager.SpawnRoom(0, spawnLocation, parentRooms) as RoomStart;
        spawnLocation = roomStart.NextRoomSpawnPoint.position;
        listRooms.Add(roomStart);

        for (int i = 0; i < roomCount; i++)
        {
            RoomSection room = poolManager.SpawnRoom(2, spawnLocation, parentRooms) as RoomSection;
            spawnLocation = room.NextRoomSpawnPoint.position;
            listRooms.Add(room);

            if (spawnOpponents)
            {
                int spawnCount = level.RandomOpponentCount;

                List<Transform> edgePoints = room.GetRandomGridEdgePoints(spawnCount);
                List<Transform> fourPoints = room.GetRandomGridFourPoints(spawnCount);
                List<Transform> fivePoints = room.GetRandomGridFivePoints(spawnCount);
                 
                for (int j = 0; j < spawnCount; j++)
                {
                    int opponentType = level.GetOpponentType();
                    Vector3 spawnPosition = Vector3.zero;

                    // spawn On Edge
                    if (opponentType == 0 || opponentType == 1)
                    {
                        print("Spawn Count: " + spawnCount + ", J: " + j);
                        spawnPosition = edgePoints[0].position;
                        edgePoints.Remove(edgePoints[0]);
                    }

                    // spawn in the middle of the map
                    else if (opponentType == 2 || opponentType == 3)
                    {
                        spawnPosition = fivePoints[0].position;
                        fivePoints.Remove(fivePoints[0]);
                    } 

                    SpawnOpponent(opponentType, spawnPosition);
                }
            }
        }

        RoomEnd roomEnd = poolManager.SpawnRoom(1, spawnLocation, parentRooms) as RoomEnd;
        listRooms.Add(roomEnd);

        OnPlayerPositionsGenerated?.Invoke(PlayerStartPosition, PlayerEndPosition, GoalPosition);
        GameManager.OnMainMenuRequest?.Invoke();
    }

    void NextLevel()
    {
        if (canGenerateNext == false)
            return;

        canGenerateNext = false;
        int next = levelDatabase.GetNextLevelIndex(levelIndex);
        levelIndex = next;
        print("Next level: " + levelIndex);
        GenerateLevel(true);
    }

    void SpawnConfetti()
    {
        if (confettiCR != null)
            StopCoroutine(confettiCR);

        confettiCR = StartCoroutine(SpawnConfettiCR());
    }

    IEnumerator SpawnConfettiCR()
    {
        Vector3 pos = UtilsVector.RandomInsideUnitCircleXZ(ConfettiPosition, 3);
        PoolManager.Instance.SpawnEffect(0, pos, null);

        float t = 0;
        float spawnTime = settings.RandomConfettiSpawnTime;

        while (levelState == LevelState.Finish)
        {
            t += Time.deltaTime;
            if (t > spawnTime)
            {
                t = 0;

                pos = UtilsVector.RandomInsideUnitCircleXZ(ConfettiPosition, 3);
                poolManager.SpawnEffect(0, pos, null);
            }

            yield return null;
        }

        poolManager.ClearEffectByID(0);
    }

    Opponent SpawnOpponent(int opponentID, Vector3 position)
    {
        int multiplier = 1;
        if (position.x > 0)
            multiplier = -1;

        Quaternion rotation = Quaternion.Euler(0, Random.Range(opponentRotation.x, opponentRotation.y) * multiplier, 0);
        Opponent opponent = poolManager.SpawnOpponent(opponentID, position, Quaternion.identity, parentOpponents);
        opponent.SetGraphicRotation(rotation);
        opponent.Initialize();

        listOpponents.Add(opponent);

        return opponent;
    }

    void StartOpponents()
    {
        for (int i = 0; i < listOpponents.Count; i++)
            listOpponents[i].StartUpdate();
    }

    void StopOpponents()
    {
        for (int i = 0; i < listOpponents.Count; i++)
            listOpponents[i].StopUpdate();
    }

    void ResetOpponents()
    {
        for (int i = 0; i < listOpponents.Count; i++)
            listOpponents[i].ResetComponentPublic();
    }

    void IdleOpponents()
    {
        for (int i = 0; i < listOpponents.Count; i++)
            listOpponents[i].SetIdle();
    }


    [NaughtyAttributes.Button("Generate Level")]
    void GenerateNoOpponents() => GenerateLevel(false);

    [NaughtyAttributes.Button("Generate Level With Opponents")]
    void GenerateWithOpponents() => GenerateLevel(true);
}
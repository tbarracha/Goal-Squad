
using UnityEngine;
using StardropTools.Pool;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] PoolCluster poolRooms;
    [SerializeField] PoolCluster poolEffects;
    [SerializeField] PoolCluster poolOpponents;



    protected override void Awake()
    {
        base.Awake();

        poolRooms.Populate();
        poolEffects.Populate();
        poolOpponents.Populate();

        GameManager.OnPoolsPopulated?.Invoke();
    }


    /// <summary>
    /// 0-Room Start, 1-Room End, 2-Room Section
    /// </summary>
    public Room SpawnRoom(int roomID, Vector3 position, Transform parent)
        => poolRooms.SpawnFromPool<Room>(roomID, position, Quaternion.identity, parent);

    public void ClearRooms() => poolRooms.ClearPools();




    /// <summary>
    /// 0-LineDasher, 1-Follower, 2-PingPong
    /// </summary>
    public Opponent SpawnOpponent(int opponentID, Vector3 position, Quaternion rotation, Transform parent)
        => poolOpponents.SpawnFromPool<Opponent>(opponentID, position, rotation, parent);

    public void ClearOpponents() => poolOpponents.ClearPools();
    public void ClearOpponentsCR() => poolOpponents.ClearPools(true);




    /// <summary>
    /// 0-ps confetti
    /// </summary>
    public PooledEffect SpawnEffect(int effectID, Vector3 position, Quaternion rotation, Transform parent)
        => poolEffects.SpawnFromPool<PooledEffect>(effectID, position, rotation, parent);

    public PooledEffect SpawnEffect(int effectID, Vector3 position, Transform parent)
        => poolEffects.SpawnFromPool<PooledEffect>(effectID, position, Quaternion.identity, parent);

    public void ClearEffectByID(int effectID) => poolEffects.ClearPool(effectID);
    public void ClearEffects() => poolEffects.ClearPools();
}


public class WaitForSecondsCache
{
    public System.Collections.Generic.Dictionary<float, UnityEngine.WaitForSeconds> dictionaryCache = new System.Collections.Generic.Dictionary<float, UnityEngine.WaitForSeconds>();
    
    public UnityEngine.WaitForSeconds GetWait(float time)
    {
        if (dictionaryCache.TryGetValue(time, out var wait))
            return wait;

        dictionaryCache[time] = new UnityEngine.WaitForSeconds(time);
        return dictionaryCache[time];
    }
}
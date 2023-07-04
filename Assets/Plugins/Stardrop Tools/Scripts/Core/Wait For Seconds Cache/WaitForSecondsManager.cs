
/// <summary>
/// Static class that stores all Coroutine WaitForSeconds for performances
/// </summary>
public static class WaitForSecondsManager
{
    public static System.Collections.Generic.Dictionary<string, WaitForSecondsCache> waitForSecondsDictionary = new System.Collections.Generic.Dictionary<string, WaitForSecondsCache>();

    /// <summary>
    /// key = identifier, time = wait time
    /// </summary>
    /// <param name="key"> identifier </param>
    /// <param name="time"> wait time </param>
    public static UnityEngine.WaitForSeconds GetWait(string key, float time)
    {
        // check if requested WaitForSeconds exists in dictionary
        if (waitForSecondsDictionary.TryGetValue(key, out var waitCache))
            return waitCache.GetWait(time);

        // else create one if requested WaitForSeconds does not exist
        waitForSecondsDictionary[key] = new WaitForSecondsCache();
        return waitForSecondsDictionary[key].GetWait(time);
    }
}
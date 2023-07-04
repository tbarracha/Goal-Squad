
public interface ILateUpdateable
{
    /// <summary>
    /// Adds object to the LateUpdate list in the FrameworkManager
    /// </summary>
    public void StartLateUpdate();


    /// <summary>
    /// Removes object to the LateUpdate list in the FrameworkManager
    /// </summary>
    public void StopLateUpdate();

    public void HandleLateUpdate();
}
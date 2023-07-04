
public interface IFixedUpdateable
{
    /// <summary>
    /// Adds object to the FixedUpdate list in the FrameworkManager
    /// </summary>
    public void StartFixedUpdate();


    /// <summary>
    /// Removes object to the FixedUpdate list in the FrameworkManager
    /// </summary>
    public void StopFixedUpdate();

    public void HandleFixedUpdate();
}
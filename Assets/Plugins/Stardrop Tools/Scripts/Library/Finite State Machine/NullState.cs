
namespace StardropTools.FiniteStateMachine
{
    /// <summary>
    /// Null state for FSM initialization
    /// </summary>
    [System.Serializable]
    public class NullState : BaseState
    {
        public NullState()
        {
            stateID = -1;
            stateName = "Null State";
        }
    }
}
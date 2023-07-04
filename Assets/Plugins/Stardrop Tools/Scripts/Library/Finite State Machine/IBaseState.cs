
namespace StardropTools.FiniteStateMachine
{
    public interface IBaseState
    {
        public void Initialize(FiniteStateMachine stateMachine, int stateID);
        public void EnterState();
        public void HandleInput();
        public void UpdateState();
        public void ExitState();

        public int GetStateID();
        public void SetID(int stateID);
    }
}
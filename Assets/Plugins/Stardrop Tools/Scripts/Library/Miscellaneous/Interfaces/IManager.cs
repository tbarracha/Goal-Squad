
namespace StardropTools
{
    public interface IManager
    {
        public void InitializeManager();
        public void LateInitializeManager();

        public bool GetCanUpdate();
    }
}
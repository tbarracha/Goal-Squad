
namespace StardropTools
{
    /// <summary>
    /// Interface that implements methods related to the HealthContainer class
    /// </summary>
    public interface IAlive
    {
        public int ApplyDamage(int damage);
        public int ApplyHeal(int heal);

        void Kill();
        public void Revive();
    }
}
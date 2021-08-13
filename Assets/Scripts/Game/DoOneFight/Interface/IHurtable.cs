namespace Game.DoOneFight.Interface
{
    public interface IHurtable
    {
        bool IsHurt();
        void TakeDamage(float damage);
    }
}
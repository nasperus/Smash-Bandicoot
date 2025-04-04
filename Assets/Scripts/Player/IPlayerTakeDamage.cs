namespace Player
{
    public interface IPlayerTakeDamage
    {
        void PlayerTakeHit();
    }

    public delegate void DamageEventHandler();
}
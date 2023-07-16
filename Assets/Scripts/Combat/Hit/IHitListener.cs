namespace Combat.Hit
{
    public interface IHitListener
    {
        void OnHit(ref HitInfo hitInfo);
    }
}
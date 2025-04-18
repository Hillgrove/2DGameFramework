using _2DGameFramework.Models.Creatures;

namespace _2DGameFramework.Interfaces
{
    public interface ICombatHandler
    {
        void Attack(Creature target);
        void ReceiveHit(int hitPoints);
    }
}
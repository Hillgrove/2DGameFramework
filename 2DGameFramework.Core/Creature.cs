namespace _2DGameFramework.Core
{
    public class Creature
    {
        private string Name { get; set; }
        private int Hitpoints { get; set; }

        public int Hit()
        {
            throw new NotImplementedException();
        }

        public void ReceiveHit(int hitdamage)
        {
            throw new NotImplementedException();
        }

        public void Loot(WorldObject obj)
        {
            throw new NotImplementedException();
        }
    }
}

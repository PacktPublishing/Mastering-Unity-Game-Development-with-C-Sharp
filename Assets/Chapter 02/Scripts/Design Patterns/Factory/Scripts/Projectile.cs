
namespace FactoryPattern
{
    public abstract class Projectile 
    {
        public abstract string Name { get; }
        public abstract void Create();
    }


    public enum ProjectileType
    {
        Bullet,
        Rocket
    }
}
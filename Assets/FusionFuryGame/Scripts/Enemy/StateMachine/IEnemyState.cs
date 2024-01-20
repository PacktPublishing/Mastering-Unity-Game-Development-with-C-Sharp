namespace FusionFuryGame
{
    public interface IEnemyState 
    {
        void EnterState(BaseEnemy enemy);
        void UpdateState(BaseEnemy enemy);
        void ExitState(BaseEnemy enemy);
    }
}
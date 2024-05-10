using UnityEngine;


namespace Chapter4
{
    public class IdleState : IEnemyState
    {
        private float idleTime = 3f; // Set the duration for which the enemy stays idle 
        private float timer; // Timer to track the idle time 
        public void EnterState(BaseEnemy enemy)
        {
            timer = 0f;
        }

        public void ExitState(BaseEnemy enemy)
        {
            //Logic for Exit 
        }

        public void UpdateState(BaseEnemy enemy)
        {
            // Logic to be executed while in the idle state 
            timer += Time.deltaTime;

            if (timer >= idleTime)
            {
                enemy.TransitionToState(enemy.wanderState);
            }
            else if (enemy.PlayerInSight())
            {
               enemy.TransitionToState(enemy.chaseState);
            }
            else if (enemy.PlayerInRange())
            {
                enemy.TransitionToState(enemy.attackState);
            }
       }
    }

}
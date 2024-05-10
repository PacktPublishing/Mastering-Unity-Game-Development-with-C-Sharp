using UnityEngine;


namespace Chapter4
{
    public class WanderState : IEnemyState
    {
        private float wanderTime = 5f; // Set the duration for which the enemy wanders
        private float timer; // Timer to track the wandering time

        public void EnterState(BaseEnemy enemy)
        {
            timer = 0f;

            StartWanderBehavior(enemy);
        }

        public void ExitState(BaseEnemy enemy)
        {
            StopWanderBehavior(enemy);
        }

        public void UpdateState(BaseEnemy enemy)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTime)
            {
                enemy.TransitionToState(enemy.idleState);
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

        private void StartWanderBehavior(BaseEnemy enemy)
        {
            Vector3 randomPosition = GetRandomPosition(enemy);
            enemy.navMeshAgent.SetDestination(randomPosition);
        }

        private void StopWanderBehavior(BaseEnemy enemy)
        {
            enemy.navMeshAgent.ResetPath();
        }

        private Vector3 GetRandomPosition(BaseEnemy enemy)
        {
            float wanderRadius = 10f;
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
            randomDirection += enemy.transform.position;
            randomDirection.y = enemy.transform.position.y; // Keep the same height
            return randomDirection;
        }
    }

}
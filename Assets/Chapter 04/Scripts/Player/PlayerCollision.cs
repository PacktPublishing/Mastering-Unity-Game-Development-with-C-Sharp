using UnityEngine;

namespace Chapter4
{
    public class PlayerCollision : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        private IDamage enemyDamage;

        private void Start()
        {
            playerHealth = GetComponent<PlayerHealth>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyProjectile"))
            {
                if (collision.gameObject.TryGetComponent(out enemyDamage))
                {
                    playerHealth.TakeDamage(enemyDamage.GetDamageValue());

                }

            }

        }
    }
}
using UnityEngine;

namespace FusionFuryGame
{
    public class EnemyCollision : MonoBehaviour
    {
        private IDamage playerDamage;
        private EnemyHealth healthComponent;

        private void Start()
        {
            healthComponent = GetComponent<EnemyHealth>();
        }
        //we can also make layers for them and reduce calculations of collision in layer matrix in project settings 
        private void OnCollisionEnter(Collision collision)
        {
        

            if (collision.gameObject.CompareTag("PlayerProjectile"))
            {
                Debug.Log("Player Projectile ");
                if (collision.gameObject.TryGetComponent(out playerDamage))
                {
                    Debug.Log("Player Projectile 2" + playerDamage.GetDamageValue());

                    healthComponent.TakeDamage(playerDamage.GetDamageValue());
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
           
        }
    }
}

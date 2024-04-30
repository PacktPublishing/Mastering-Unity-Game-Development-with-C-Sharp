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
                if (collision.gameObject.TryGetComponent(out playerDamage))
                {
                    healthComponent.TakeDamage(playerDamage.GetDamageValue());
                }
            }
        }
    }
}

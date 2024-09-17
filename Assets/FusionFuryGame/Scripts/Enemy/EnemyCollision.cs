using System.Collections;
using UnityEngine;

namespace FusionFuryGame
{
    public class EnemyCollision : MonoBehaviour
    {
        private IDamage playerDamage;
        private EnemyHealth healthComponent;
        [SerializeField] EnemyData enemyData;

        [SerializeField] private Renderer enemyRenderer;
        [SerializeField] private Material flashMaterial;
        private Material originalMaterial;
        private void Start()
        {
            healthComponent = GetComponent<EnemyHealth>();

            originalMaterial = enemyRenderer.material; // Cache original material

        }
        //we can also make layers for them and reduce calculations of collision in layer matrix in project settings 
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PlayerProjectile"))
            {
                if (collision.gameObject.TryGetComponent(out playerDamage))
                {
                    healthComponent.TakeDamage(playerDamage.GetDamageValue());

                    StartCoroutine(FlashEffect());

                    var floatingText = PoolManager.Instance.GetPooledObject("Floating Text").GetComponent<FloatingText>();
                    // Initialize the floating text with the damage value and the color from EnemyData
                    floatingText.Initialize(playerDamage.GetDamageValue().ToString(), enemyData.floatingTextColor, transform);
                }
            }
        }

        private IEnumerator FlashEffect()
        {
            enemyRenderer.material = flashMaterial; // Swap to flash material
            yield return new WaitForSeconds(0.1f);  // Flash duration
            enemyRenderer.material = originalMaterial; // Revert to original material
        }

        private void OnTriggerEnter(Collider other)
        {
           
        }
    }
}

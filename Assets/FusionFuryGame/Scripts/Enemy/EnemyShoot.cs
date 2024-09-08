using UnityEngine;

namespace FusionFuryGame
{
    public class EnemyShoot : MonoBehaviour, IDamage
    {
        [SerializeField] float damage; //when the enemy collide with the player

        public BaseWeapon attachedWeapon;  // Reference to the attacted Weapon
        [SerializeField] float fireDamage; //when the enemy shoot the player
        private BaseEnemy baseEnemy;
        private void Start()
        {
            if (attachedWeapon != null)
                PoolManager.Instance.AddNewPoolItem(attachedWeapon.weaponData.projectileData.attachedProjectile.gameObject, 10);
            baseEnemy = GetComponent<BaseEnemy>();
        }
        public void FireShot()
        {
            Vector3 directionToPlayer = (baseEnemy.player.position - transform.position).normalized;
            directionToPlayer.y = 0f;  // Ignore vertical aiming
            Debug.Log("SHot Start");
            attachedWeapon.Shoot(fireDamage, directionToPlayer);
        }

        public float GetDamageValue()
        {
            // You can implement more sophisticated logic here based on enemy stats
            return damage;
        }
    }
}

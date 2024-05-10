using UnityEngine;


namespace Chapter4
{
    public class EnemyShoot : MonoBehaviour, IDamage
    {
        [SerializeField] float damage; //when the enemy collide with the player 
        [SerializeField] float fireDamage; //when the enemy shoot the player 
        public BaseWeapon attachedWeapon;  // Reference to the attacted Weapon 

        public void FireProjectile()
        {
            attachedWeapon.Shoot(fireDamage);
        }

        public float GetDamageValue()
        {
           // You can implement more sophisticated logic here based on enemy stats 
            return damage;
        }
    }

}
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;


namespace Chapter4
{
    public interface IHealth
    {
        float MaxHealth { get; set; }   // Property for maximum health 
        float CurrentHealth { get; set; }  // Property for current health 

        void TakeDamage(float damage);  // Method to apply damage 
        void SetMaxHealth();  // Method to set current health to max health 
        void Heal();            // Method to apply healing 

    }

}
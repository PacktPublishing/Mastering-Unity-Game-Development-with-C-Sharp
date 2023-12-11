using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FactoryPattern;

public class Weapon : MonoBehaviour
{
    void Start()
    {
        var Bullet = ProjectileFactory.GetProjectile(ProjectileType.Bullet);

        Bullet.Create();        
    }

}

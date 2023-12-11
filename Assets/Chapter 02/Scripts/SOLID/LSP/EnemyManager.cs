using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2.LSP
{
    public class EnemyManager : MonoBehaviour
    {
        void Start()
        {
            // Creating instances of GroundEnemy and FlyingEnemy
            Enemy groundEnemy = new GroundEnemy();
            Enemy flyingEnemy = new FlyingEnemy();

            // Using LSP, treating both enemies as base class
            groundEnemy.Move();
            groundEnemy.Attack();

            flyingEnemy.Move();
            flyingEnemy.Attack();
        }

    }
}
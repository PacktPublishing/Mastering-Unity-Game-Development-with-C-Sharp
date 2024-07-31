using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]

    public class EnemyData : ScriptableObject
    {
        public float chaseSpeed;
        public float rotationSpeed;
        public float attackRange;
        public Color floatingTextColor;
    }
}
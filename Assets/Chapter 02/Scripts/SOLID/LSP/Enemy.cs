using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2.LSP
{
    public class Enemy : MonoBehaviour
    {
        public virtual void Move()
        {// Basic movement logic for all enemies
        }
        public virtual void Attack()
        {// Basic attack logic for all enemies
        }

    }
}
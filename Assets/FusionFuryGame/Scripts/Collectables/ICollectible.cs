using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public abstract class ICollectible : MonoBehaviour
    {
        public string collectibleID;
        public AudioClip collectibleSFX;
        public GameObject collectibleEffect;
        protected GameObject player;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                ApplyEffect();
            }
        }


        protected abstract void ApplyEffect();
    }
}
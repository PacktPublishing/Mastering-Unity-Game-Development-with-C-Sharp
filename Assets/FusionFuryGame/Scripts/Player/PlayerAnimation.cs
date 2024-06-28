
using UnityEngine;

namespace FusionFuryGame
{
    public class PlayerAnimation : MonoBehaviour
    {
        private readonly int hit = Animator.StringToHash("Hit");
        private readonly int powerUp = Animator.StringToHash("Hit");
        private readonly int shoot = Animator.StringToHash("Hit");
        private readonly int die = Animator.StringToHash("Hit");
        private readonly int movementVector = Animator.StringToHash("Hit");

        private Animator m_Animator;
        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
        }

        private void Onhit()
        {
            m_Animator.SetTrigger(hit);
        }

        private void OnShooting()
        {
            m_Animator.SetTrigger(shoot);
        }

        private void OnDie(bool state)
        {
            m_Animator.SetBool(die, state);
        }

        private void OnPowerUp()
        {
            m_Animator.SetTrigger(powerUp);
        }

        private void OnMove(float movement)
        {
            m_Animator.SetFloat(movementVector , movement);
        }

    }
}
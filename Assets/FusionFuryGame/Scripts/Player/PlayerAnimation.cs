
using UnityEngine;

namespace FusionFuryGame
{
    public class PlayerAnimation : MonoBehaviour
    {
        private readonly int hit = Animator.StringToHash("Hit");
        private readonly int powerUp = Animator.StringToHash("PowerUp");
        private readonly int shoot = Animator.StringToHash("Shooting");
        private readonly int die = Animator.StringToHash("IsDied");
        private readonly int movementVector = Animator.StringToHash("MovementVector");

        private Animator m_Animator;
        private void OnEnable()
        {
            PlayerShoot.onFire += OnShooting;
            PlayerHealth.onPlayerDied += OnDie;
            PlayerMovement.onMove += OnMove;
            PlayerAbility.OnAbilityActivated += OnPowerUp;
            PlayerCollision.onPlayerGetHit += Onhit;
        }

        private void OnDisable()
        {
            PlayerShoot.onFire -= OnShooting;
            PlayerHealth.onPlayerDied -= OnDie;
            PlayerMovement.onMove -= OnMove;
            PlayerAbility.OnAbilityActivated -= OnPowerUp;
            PlayerCollision.onPlayerGetHit -= Onhit;

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

        private void OnDie()
        {
            m_Animator.SetBool(die, true);
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
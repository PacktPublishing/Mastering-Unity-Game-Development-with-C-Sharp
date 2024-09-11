using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Chapter6;
using Coffee.UIEffects;

namespace FusionFuryGame
{
    public class HUDView : BaseView
    {
        [SerializeField] Image fasthealthBar;
        [SerializeField] Image slowHealthBar;
        [SerializeField] UIShiny fastHealthShiny;

        private float dummyHealth= 100;
        private float previousHealth; 

        protected override void Start()
        {
            base.Start();
            HUDManager.Instance.RegisterView(this);
            previousHealth = 100f;
        }
        private void OnEnable()
        {
            PlayerHealth.onPlayerHealthChanged += OnPlayerHealthChanged;
        }

        private void OnDisable()
        {
            PlayerHealth.onPlayerHealthChanged -= OnPlayerHealthChanged;
        }

        void OnPlayerHealthChanged(float newHealth)
        {
            float healthPercentage = newHealth / 100;
            float previousHealthPercentage = previousHealth / 100;
            float HealthChange = newHealth - previousHealth;



            if (HealthChange > 0)
            {
                // Increase energy: move the slow bar first in green, then the fast bar catches up
                slowHealthBar.color = Color.cyan;
                slowHealthBar.fillAmount = previousHealthPercentage;
                fasthealthBar.fillAmount = previousHealthPercentage;

                slowHealthBar.DOFillAmount(healthPercentage, 0.25f).OnComplete(() =>
                {
                    fasthealthBar.DOFillAmount(healthPercentage, 1f).SetEase(Ease.OutQuad);
                });
            }
            else
            {
                // Decrease energy: move the fast bar first, then the slow bar catches up in red
                slowHealthBar.color = Color.red;
                fasthealthBar.fillAmount = previousHealthPercentage;
                slowHealthBar.fillAmount = previousHealthPercentage;

                fastHealthShiny.effectPlayer.Play(true);
                fasthealthBar.DOFillAmount(healthPercentage, 0.25f).OnComplete(() =>
                {
                    slowHealthBar.DOFillAmount(healthPercentage, 1f).SetEase(Ease.OutQuad);
                    
                });
            }

            previousHealth = newHealth;
        }


        [ContextMenu("Test Damage")]
        public void TestTakeDamage()
        {
            dummyHealth -= 10f;
            OnPlayerHealthChanged(dummyHealth);
        }
        //here player health 


    }
}
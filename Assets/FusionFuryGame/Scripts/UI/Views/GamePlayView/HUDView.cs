using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Chapter6;
using Coffee.UIEffects;
using TMPro;
using UnityEngine.Events;

namespace FusionFuryGame
{
    public class HUDView : BaseView
    {
        [SerializeField] Image fasthealthBar;
        [SerializeField] Image slowHealthBar;
        [SerializeField] UIShiny fastHealthShiny;
        [SerializeField] TextMeshProUGUI ammoText;
        
        //Ability Button
        [SerializeField] Button abilityButton;
        [SerializeField] Image fillingAbilityImage;
        [SerializeField] private float abilityCooldownTime = 10f;  // Set cooldown time for the ability
        [SerializeField] UIShiny fillingImageShiny;
        private float dummyHealth= 100;
        private float previousHealth; 

        public static UnityAction onAbilityPressed = delegate { };
        protected override void Start()
        {
            base.Start();
            HUDManager.Instance.RegisterView(this);
            previousHealth = 100f;
            ammoText.text = 10.ToString();
        }
        private void OnEnable()
        {
            PlayerHealth.onPlayerHealthChanged += OnPlayerHealthChanged;
            BaseWeapon.onChangeAmmo += OnPlayerAmmoChange;
            abilityButton.onClick.AddListener(OnAbilityPressed);
        }

        private void OnDisable()
        {
            PlayerHealth.onPlayerHealthChanged -= OnPlayerHealthChanged;
            BaseWeapon.onChangeAmmo -= OnPlayerAmmoChange;
            abilityButton.onClick?.RemoveListener(OnAbilityPressed);
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

        void OnPlayerAmmoChange(int newAmmo)
        {
            if (newAmmo < 0) { return; }

            ammoText.text = newAmmo.ToString();

        }
        [ContextMenu("Test Damage")]
        public void TestTakeDamage()
        {
            dummyHealth -= 10f;
            OnPlayerHealthChanged(dummyHealth);
        }
        //here player health 

        private void OnAbilityPressed()
        {
            fillingImageShiny.effectPlayer.play = false;
            // Disable the button
            abilityButton.interactable = false;

            // Set fill amount to 0
            fillingAbilityImage.fillAmount = 0;

            // Invoke ability usage
            onAbilityPressed.Invoke();

            // Start cooldown coroutine to refill image and enable button
            StartCoroutine(HandleAbilityCooldown());
        }

        private IEnumerator HandleAbilityCooldown()
        {
            float elapsedTime = 0f;

            while (elapsedTime < abilityCooldownTime)
            {
                elapsedTime += Time.deltaTime;

                // Calculate and set the fill amount
                fillingAbilityImage.fillAmount = elapsedTime / abilityCooldownTime;

                yield return null;
            }
            fillingImageShiny.effectPlayer.play = true;


            // Cooldown complete, re-enable the button
            abilityButton.interactable = true;
        }

    }
}
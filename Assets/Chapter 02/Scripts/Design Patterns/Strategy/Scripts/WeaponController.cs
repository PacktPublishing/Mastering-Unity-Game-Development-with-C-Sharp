using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StrategyPattern;


public class WeaponController : MonoBehaviour
{
    
    [SerializeField] GameObject blaster;
    [SerializeField] GameObject launcher;

    private GameObject currentPrefab;

    private WeaponBase currentWeapon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(blaster);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(launcher);
        }

        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Shoot();
        }
    }


    private void EquipWeapon(GameObject currentEquipped)
    {
        if (currentPrefab != null)
            Destroy(currentPrefab);

        currentPrefab = Instantiate(currentEquipped, this.transform);

        currentWeapon = currentPrefab.GetComponent<WeaponBase>();

    }
}

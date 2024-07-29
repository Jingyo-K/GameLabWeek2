using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    public Weapons weapon;
    public GameObject defalutWeapon1;
    public GameObject defalutWeapon2;
    void Start()
    {
        GameEvents.onWeaponSwap += OnWeaponSwap;
        GameEvents.onWeaponChange += OnWeaponChange;
        weapons = new GameObject[2];
        weapons[0] = defalutWeapon1;
        weapons[1] = defalutWeapon2;
        weapon = weapons[0].GetComponent<Weapons>();
        SetWeapon();
    }
    
    void OnWeaponSwap(GameEvents gameEvents)
    {
        GameObject temp = weapons[0];
        weapons[0] = weapons[1];
        weapons[1] = temp;
        weapon = weapons[0].GetComponent<Weapons>();
        SetWeapon();
    }
    void OnWeaponChange(GameEvents gameEvents)
    {
        weapon = weapons[0].GetComponent<Weapons>();
        SetWeapon();
    }
    void OnDestroy()
    {
        GameEvents.onWeaponSwap -= OnWeaponSwap;
        GameEvents.onWeaponChange -= OnWeaponChange;
    }
    public void SetWeapon()
    {
        weapon = weapons[0].GetComponent<Weapons>();
        GetComponent<Weapons>().weaponType = weapon.weaponType;
        GetComponent<Weapons>().BulletPrefab = weapon.BulletPrefab;
        GetComponent<Weapons>().DamageCoefficient = weapon.DamageCoefficient;
        GetComponent<Weapons>().FireRateCoefficient = weapon.FireRateCoefficient;
        GetComponent<Weapons>().bulletCount = weapon.bulletCount;
    }
}

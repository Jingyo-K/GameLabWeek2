using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentChangeUIManager : MonoBehaviour
{
    public GameObject ChangePanel; // 스테이터스 패널
    public _PlayerManager playerEquipment;
    private GameObject newEquipment;
    GameEvents gameEvents;
    GameManager gameManager;
    public void Start()
    {
        gameManager = GameManager.instance;
    }
    private void Update()
    {
    }

    public void ToggleChangePanel()
    {
        ChangePanel.SetActive(!ChangePanel.activeSelf);
        if(ChangePanel.activeSelf)
        {
            TimeManager.isChangePanelActive = true;
        }
        else
        {
            TimeManager.isChangePanelActive = false;
        }
    }

    public void UpdateChangeUI(GameObject equipment)
    {
        newEquipment = equipment;
        Equipment.EquipmentType type = equipment.GetComponent<Equipment>().Type;
        GameObject CurrentEquipment = playerEquipment.GetEquipment(type);
        TextMeshProUGUI NewEquipment = ChangePanel.transform.Find("NewEquipment").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI NewName = ChangePanel.transform.Find("NewName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI NewRank = ChangePanel.transform.Find("NewRank").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI NewStat1 = ChangePanel.transform.Find("NewStat1").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI NewStat2 = ChangePanel.transform.Find("NewStat2").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI NewStat3 = ChangePanel.transform.Find("NewStat3").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI NewStat4 = ChangePanel.transform.Find("NewStat4").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI NewStat5 = ChangePanel.transform.Find("NewStat5").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI CurEquipment = ChangePanel.transform.Find("CurEquipment").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI CurName = ChangePanel.transform.Find("CurName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI CurRank = ChangePanel.transform.Find("CurRank").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI CurStat1 = ChangePanel.transform.Find("CurStat1").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI CurStat2 = ChangePanel.transform.Find("CurStat2").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI CurStat3 = ChangePanel.transform.Find("CurStat3").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI CurStat4 = ChangePanel.transform.Find("CurStat4").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI CurStat5 = ChangePanel.transform.Find("CurStat5").GetComponent<TextMeshProUGUI>();
        switch (type)
        {
            case Equipment.EquipmentType.Weapon:
                NewEquipment.text = "New Weapon";
                NewName.text = equipment.name;
                NewRank.text = equipment.GetComponent<Weapons>().weaponRarity.ToString();
                switch(equipment.GetComponent<Weapons>().weaponRarity)
                {
                    case Weapons.WeaponRarity.Common:
                        NewRank.color = Color.white;
                        break;
                    case Weapons.WeaponRarity.Rare:
                        NewRank.color = Color.blue;
                        break;
                    case Weapons.WeaponRarity.Epic:
                        NewRank.color = Color.magenta;
                        break;
                    case Weapons.WeaponRarity.Legendary:
                        NewRank.color = Color.yellow;
                        break;
                }
                NewStat1.text = "Damage: x" + equipment.GetComponent<Weapons>().DamageCoefficient;
                NewStat2.text = string.Format("Fire Rate: {0:0.00} Fire/sec", 1/equipment.GetComponent<Weapons>().FireRateCoefficient);
                NewStat3.text = "Bullet: " + equipment.GetComponent<Weapons>().bulletCount;
                NewStat4.text = "Type: " + equipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().moveType;
                if(equipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType == Bullet.AttackType.Common)
                    NewStat5.text = "";
                else if(equipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType == Bullet.AttackType.Penetrate)
                    NewStat5.text = "Special: " + equipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType + "  " + equipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().penetrateCount;
                else if(equipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType == Bullet.AttackType.Blast)
                    NewStat5.text = "Special: " + equipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType + "  " + equipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().blastRadius;
                CurEquipment.text = "Current Weapon";
                CurName.text = CurrentEquipment.name;
                CurRank.text = CurrentEquipment.GetComponent<Weapons>().weaponRarity.ToString();
                switch(CurrentEquipment.GetComponent<Weapons>().weaponRarity)
                {
                    case Weapons.WeaponRarity.Common:
                        CurRank.color = Color.white;
                        break;
                    case Weapons.WeaponRarity.Rare:
                        CurRank.color = Color.blue;
                        break;
                    case Weapons.WeaponRarity.Epic:
                        CurRank.color = Color.magenta;
                        break;
                    case Weapons.WeaponRarity.Legendary:
                        CurRank.color = Color.yellow;
                        break;
                }
                CurStat1.text = "Damage: x" + CurrentEquipment.GetComponent<Weapons>().DamageCoefficient;
                CurStat2.text = string.Format("Fire Rate: {0:0.00} Fire/sec", 1/CurrentEquipment.GetComponent<Weapons>().FireRateCoefficient);
                CurStat3.text = "Bullet: " + CurrentEquipment.GetComponent<Weapons>().bulletCount;
                CurStat4.text = "Type: " + CurrentEquipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().moveType;
                if(CurrentEquipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType == Bullet.AttackType.Common)
                    CurStat5.text = "";
                else if(CurrentEquipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType == Bullet.AttackType.Penetrate)
                    CurStat5.text = "Special: " + CurrentEquipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType + "  " + CurrentEquipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().penetrateCount;
                else if(CurrentEquipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType == Bullet.AttackType.Blast)
                    CurStat5.text = "Special: " + CurrentEquipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType + "  " + CurrentEquipment.GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().blastRadius;
                break;
            
            case Equipment.EquipmentType.Engine:
                NewEquipment.text = "New Engine";
                NewName.text = equipment.name;
                NewStat1.text = "Speed: " + equipment.GetComponent<Equipment>().EquipmentStatus.Speed;
                NewStat2.text = "Weight: " + equipment.GetComponent<Equipment>().EquipmentStatus.Weight;
                NewStat3.text = "Attack: " + equipment.GetComponent<Equipment>().EquipmentStatus.Attack;
                NewStat4.text = "Health: " + equipment.GetComponent<Equipment>().EquipmentStatus.Health;
                CurEquipment.text = "Current Engine";
                CurName.text = CurrentEquipment.name;
                CurStat1.text = "Speed: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Speed;
                CurStat2.text = "Weight: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Weight;
                CurStat3.text = "Attack: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Attack;
                CurStat4.text = "Health: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Health;
                break;

            case Equipment.EquipmentType.Shield:
                NewEquipment.text = "New Shield";
                NewName.text = equipment.name;
                NewStat1.text = "Shield: " + equipment.GetComponent<Equipment>().EquipmentStatus.Shield;
                NewStat3.text = "Weight: " + equipment.GetComponent<Equipment>().EquipmentStatus.Weight;
                NewStat4.text = "Speed: " + equipment.GetComponent<Equipment>().EquipmentStatus.Speed;
                CurEquipment.text = "Current Shield";
                CurName.text = CurrentEquipment.name;
                CurStat1.text = "Shield: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Shield;
                CurStat3.text = "Weight: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Weight;
                CurStat4.text = "Speed: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Speed;
                break;
            
            case Equipment.EquipmentType.Wing:
                NewEquipment.text = "New Wing";
                NewName.text = equipment.name;
                NewStat1.text = "Speed: " + equipment.GetComponent<Equipment>().EquipmentStatus.Speed;
                NewStat2.text = "Weight: " + equipment.GetComponent<Equipment>().EquipmentStatus.Weight;
                NewStat3.text = "Attack: " + equipment.GetComponent<Equipment>().EquipmentStatus.Attack;
                NewStat4.text = "Fire Rate: " + equipment.GetComponent<Equipment>().EquipmentStatus.Health;
                CurEquipment.text = "Current Wing";
                CurName.text = CurrentEquipment.name;
                CurStat1.text = "Speed: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Speed;
                CurStat2.text = "Weight: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Weight;
                CurStat3.text = "Damage: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Attack;
                CurStat4.text = "Fire Rate: " + CurrentEquipment.GetComponent<Equipment>().EquipmentStatus.Health;
                break;

        }
    }
    public void UpdateChangeUI()
    {
        if (newEquipment == null)
        {
            return;
        }
        UpdateChangeUI(newEquipment);
    }
    public void ChangeEquipment()
    {
        Equipment.EquipmentType type = newEquipment.GetComponent<Equipment>().Type;
        playerEquipment.SetEquipment(newEquipment);
        ToggleChangePanel();
        gameManager.gameEvents.callWeaponChange();
        Destroy(newEquipment);
    }
    public void CancelChange()
    {
        Destroy(newEquipment);
        ToggleChangePanel();
    }
}

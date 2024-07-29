using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class _PlayerManager : MonoBehaviour
{
    public static _PlayerManager Instance;
    private GameEvents gameEvents;

    [System.Serializable]
    public class Status
    {
        public int Health;
        public int Shield;
        public int Speed;
        public int Attack;
        public int Agility;
        public int Weight;

        //Combat Status
        public float Damage;
        public float FireRate;
        public float DodgeRate;
    }
    [SerializeField] private Status CurrentStatus;
    [SerializeField] private Status BaseStatus;
    [SerializeField] private Status BonusStatus;

    [System.Serializable]
    private class PlayerEquipment
    {
        public GameObject Weapons;
        public GameObject Engine;
        public GameObject Shield;
        public GameObject Wing;
    }
    [SerializeField] private PlayerEquipment CurrentEquipment;
    [SerializeField] private StatusUIManager statusUIManager;
    [SerializeField] private EquipmentChangeUIManager equipmentChangeUIManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameEvents = GameManager.instance.gameEvents;
        StatusInit();
        GetBonusStatus();
        StatusUpdate();
    }

    public void StatusInit()
    {
        CurrentStatus = new Status();
        BonusStatus = new Status();
        CurrentStatus.Health = BaseStatus.Health;
        CurrentStatus.Shield = BaseStatus.Shield;
        CurrentStatus.Speed = BaseStatus.Speed;
        CurrentStatus.Attack = BaseStatus.Attack;
        CurrentStatus.Agility = BaseStatus.Agility;
        CurrentStatus.Weight = BaseStatus.Weight;
        CalculateCombatStat();
        statusUIManager.UpdateStatusUI(CurrentStatus);
    }

    public void StatusUpdate()
    {
        CurrentStatus.Health = BaseStatus.Health + BonusStatus.Health;
        CurrentStatus.Shield = BaseStatus.Shield + BonusStatus.Shield;
        CurrentStatus.Speed = BaseStatus.Speed + BonusStatus.Speed;
        CurrentStatus.Attack = BaseStatus.Attack + BonusStatus.Attack;
        CurrentStatus.Agility = BaseStatus.Agility + BonusStatus.Agility;
        CurrentStatus.Weight = BaseStatus.Weight + BonusStatus.Weight;
        CalculateCombatStat();
        statusUIManager.UpdateStatusUI(CurrentStatus);
    }

    public void GetBonusStatus()
    {
        ResetBonusStatus();
        // Get bonus status from equipment
        Status weaponStatus = CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].GetComponent<Equipment>().EquipmentStatus;
        AddBonusStatus(weaponStatus);
        
        Status engineStatus = CurrentEquipment.Engine.GetComponent<Equipment>().EquipmentStatus;
        AddBonusStatus(engineStatus);

        Status shieldStatus = CurrentEquipment.Shield.GetComponent<Equipment>().EquipmentStatus;
        AddBonusStatus(shieldStatus);

        Status wingStatus = CurrentEquipment.Wing.GetComponent<Equipment>().EquipmentStatus;
        AddBonusStatus(wingStatus);
    }

    private void ResetBonusStatus()
    {
        BonusStatus.Health = 0;
        BonusStatus.Shield = 0;
        BonusStatus.Speed = 0;
        BonusStatus.Attack = 0;
        BonusStatus.Agility = 0;
        BonusStatus.Weight = 0;
    }

    public void AddBonusStatus(Status status)
    {
        BonusStatus.Health += status.Health;
        BonusStatus.Shield += status.Shield;
        BonusStatus.Speed += status.Speed;
        BonusStatus.Attack += status.Attack;
        BonusStatus.Agility += status.Agility;
        BonusStatus.Weight += status.Weight;
        statusUIManager.UpdateStatusUI(CurrentStatus);
    }
    public void AddBaseStatus(Status status)
    {
        BaseStatus.Health += status.Health;
        BaseStatus.Shield += status.Shield;
        BaseStatus.Speed += status.Speed;
        BaseStatus.Attack += status.Attack;
        BaseStatus.Agility += status.Agility;
        BaseStatus.Weight += status.Weight;
        StatusUpdate();
        statusUIManager.UpdateStatusUI(CurrentStatus);
    }
    private void CalculateCombatStat()
    {
        Weapons weapon = CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].GetComponent<Weapons>();
        float DamageCoefficient = weapon.DamageCoefficient;
        float FireRateCoefficient = weapon.FireRateCoefficient;
        // Damage = Attack * DamageCoefficient
        CurrentStatus.Damage = CurrentStatus.Attack * DamageCoefficient;
        // FireRate = FireRateCoefficient * (1 - (Agility /200))
        CurrentStatus.FireRate = FireRateCoefficient * (1 - (CurrentStatus.Agility / 200f));
        // DodgeRate = Agility - Weight
        if (CurrentStatus.Agility - CurrentStatus.Weight < 0)
        {
            CurrentStatus.DodgeRate = 0;
        }
        else
        {
            CurrentStatus.DodgeRate = CurrentStatus.Agility - CurrentStatus.Weight;
        }
        if (CurrentStatus.DodgeRate > 50)
        {
            CurrentStatus.DodgeRate = 50;
        }
    }
    public GameObject GetEquipment(Equipment.EquipmentType type)
    {
        switch (type)
        {
            case Equipment.EquipmentType.Weapon:
                return CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0];
            case Equipment.EquipmentType.Engine:
                return CurrentEquipment.Engine;
            case Equipment.EquipmentType.Shield:
                return CurrentEquipment.Shield;
            case Equipment.EquipmentType.Wing:
                return CurrentEquipment.Wing;
            default:
                return null;
        }
    }
    public void SetEquipment(GameObject equipment)
    {
        Equipment.EquipmentType type = equipment.GetComponent<Equipment>().Type;
        switch (type)
        {
            case Equipment.EquipmentType.Weapon:
                CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].name = equipment.name;
                CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].GetComponent<Weapons>().weaponRarity = equipment.GetComponent<Weapons>().weaponRarity;
                CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].GetComponent<Weapons>().Type = equipment.GetComponent<Weapons>().Type;
                CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].GetComponent<Weapons>().weaponType = equipment.GetComponent<Weapons>().weaponType;
                CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].GetComponent<Weapons>().BulletPrefab = equipment.GetComponent<Weapons>().BulletPrefab;
                CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].GetComponent<Weapons>().DamageCoefficient = equipment.GetComponent<Weapons>().DamageCoefficient;
                CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].GetComponent<Weapons>().FireRateCoefficient = equipment.GetComponent<Weapons>().FireRateCoefficient;
                CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[0].GetComponent<Weapons>().bulletCount = equipment.GetComponent<Weapons>().bulletCount;
                break;
            case Equipment.EquipmentType.Engine:
                CurrentEquipment.Engine = equipment;
                break;
            case Equipment.EquipmentType.Shield:
                CurrentEquipment.Shield = equipment;
                break;
            case Equipment.EquipmentType.Wing:
                CurrentEquipment.Wing = equipment;
                break;
        }
        GetBonusStatus();
        StatusUpdate();
    }

    public void WeaponSwap()
    {
        gameEvents.callWeaponSwap();
        GetBonusStatus();
        StatusUpdate();
        equipmentChangeUIManager.UpdateChangeUI();
    }
    public string GetWeaponName(int index)
    {
        return CurrentEquipment.Weapons.GetComponent<WeaponManager>().weapons[index].name;
    }

    public string GetEngineName()
    {
        return CurrentEquipment.Engine != null ? CurrentEquipment.Engine.name : "None";
    }

    public string GetShieldName()
    {
        return CurrentEquipment.Shield != null ? CurrentEquipment.Shield.name : "None";
    }

    public string GetWingName()
    {
        return CurrentEquipment.Wing != null ? CurrentEquipment.Wing.name : "None";
    }

    public Status GetCurrentStatus()
    {
        return CurrentStatus;
    }
}

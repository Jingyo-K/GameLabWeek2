using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] _PlayerManager.Status equipmentStatus;
    [SerializeField] public _PlayerManager.Status EquipmentStatus
    {
        set => equipmentStatus = value;
        get => equipmentStatus;
    }
    public enum EquipmentType
    {
        Weapon,
        Engine,
        Shield,
        Wing
    }
    public EquipmentType Type;
}

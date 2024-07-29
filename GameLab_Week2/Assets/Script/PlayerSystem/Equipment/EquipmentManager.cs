using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] _PlayerManager.Status equipmentStatus;

    [SerializeField] public _PlayerManager.Status EquipmentStatus
    {
        set => equipmentStatus = value;
        get => equipmentStatus;
    }
    public GameObject EquipmentSlot;
    void Awake()
    {
        EquipmentStatus = new _PlayerManager.Status();
        EquipmentStatus = EquipmentSlot.GetComponent<Equipment>().EquipmentStatus;
    }
}

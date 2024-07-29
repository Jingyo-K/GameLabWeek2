using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDrop : MonoBehaviour
{
    public GameObject[] CommonEquipmentDropList;
    public GameObject[] RareEquipmentDropList;
    public GameObject[] EpicEquipmentDropList;
    public GameObject[] LegendaryEquipmentDropList;
    public EquipmentChangeUIManager equipmentChangeUIManager;
    private string prefabFolder = "Prefab/Equipment/Weapon";

    void Start()
    {
        CommonEquipmentDropList = Resources.LoadAll<GameObject>(prefabFolder + "/Common");
        RareEquipmentDropList = Resources.LoadAll<GameObject>(prefabFolder + "/Rare");
        EpicEquipmentDropList = Resources.LoadAll<GameObject>(prefabFolder + "/Epic");
        LegendaryEquipmentDropList = Resources.LoadAll<GameObject>(prefabFolder + "/Legendary");
    }

    public void DropEquipment()
    {
        int random = Random.Range(0, 100);
        if (random < 50)
        {
            int randomEquipment = Random.Range(0, CommonEquipmentDropList.Length);
            GameObject equipment = Instantiate(CommonEquipmentDropList[randomEquipment], new Vector3(0,20,0), Quaternion.identity);
            equipment.name = equipment.name.Replace("(Clone)", "");
            equipment.GetComponent<Weapons>().DamageCoefficient *= (1+EnemyGenUI.Stage * 0.1f);
            equipmentChangeUIManager.UpdateChangeUI(equipment);
            equipmentChangeUIManager.ToggleChangePanel();
            //Destroy(equipment);
        }
        else if (random < 80)
        {
            int randomEquipment = Random.Range(0, RareEquipmentDropList.Length);
            GameObject equipment = Instantiate(RareEquipmentDropList[randomEquipment], new Vector3(0,20,0), Quaternion.identity);
            equipment.name = equipment.name.Replace("(Clone)", "");
            equipment.GetComponent<Weapons>().DamageCoefficient *= (1+EnemyGenUI.Stage * 0.1f);
            equipmentChangeUIManager.UpdateChangeUI(equipment);
            equipmentChangeUIManager.ToggleChangePanel();
            //Destroy(equipment);          
        }
        else if (random < 93)
        {
            int randomEquipment = Random.Range(0, EpicEquipmentDropList.Length);
            GameObject equipment = Instantiate(EpicEquipmentDropList[randomEquipment], new Vector3(0,20,0), Quaternion.identity);
            equipment.name = equipment.name.Replace("(Clone)", "");
            equipment.GetComponent<Weapons>().DamageCoefficient *= (1+EnemyGenUI.Stage * 0.1f);
            equipmentChangeUIManager.UpdateChangeUI(equipment);
            equipmentChangeUIManager.ToggleChangePanel();
            //Destroy(equipment);      
        }
        else
        {
            int randomEquipment = Random.Range(0, LegendaryEquipmentDropList.Length);
            GameObject equipment = Instantiate(LegendaryEquipmentDropList[randomEquipment], new Vector3(0,20,0), Quaternion.identity);
            equipment.name = equipment.name.Replace("(Clone)", "");
            equipment.GetComponent<Weapons>().DamageCoefficient *= (1+EnemyGenUI.Stage * 0.1f);
            equipmentChangeUIManager.UpdateChangeUI(equipment);
            equipmentChangeUIManager.ToggleChangePanel();
            //Destroy(equipment);
        }
    }
}

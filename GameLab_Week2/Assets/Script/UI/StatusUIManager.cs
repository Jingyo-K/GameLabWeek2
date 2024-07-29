using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUIManager : MonoBehaviour
{
    public GameObject statusPanel; // 스테이터스 패널
    public GameObject CombatStatusPanel; // 전투 스테이터스 패널
    public _PlayerManager playerEquipment;

    private void Start()
    {
        GameEvents.onPlayerTakeDamage += OnPlayerTakeDamage;
        // 게임 시작 시 스테이터스 패널 비활성화
        statusPanel.SetActive(false);
        CombatStatusPanel.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleStatusPanel();
        }
    }

    private void ToggleStatusPanel()
    {
        statusPanel.SetActive(!statusPanel.activeSelf);
        if(statusPanel.activeSelf)
        {
            TimeManager.isStatusPanelActive = true;
            CombatStatusPanel.SetActive(false);
        }
        else
        {
            TimeManager.isStatusPanelActive = false;
            CombatStatusPanel.SetActive(true);
        }
    }

    public void UpdateStatusUI(_PlayerManager.Status status)
    {
        UpdateStatus(status);
        UpdateEquipment();
        UpdateCombatStatus(status);
    }

    private void UpdateStatus(_PlayerManager.Status status)
    {
        // 자리수를 맞추기 위한 포맷
        string healthTextFormatted = string.Format("Health\t: {0}", status.Health);
        string ShieldTextFormatted = string.Format("Shield\t: {0}", status.Shield);
        string speedTextFormatted = string.Format("Speed\t: {0}", status.Speed);
        string AttackTextFormatted = string.Format("Attack\t: {0}", status.Attack);
        string AgilityTextFormatted = string.Format("Agility\t: {0}", status.Agility);
        string weightTextFormatted = string.Format("Weight\t: {0}", status.Weight);

        // Text 요소를 찾아서 상태 값을 업데이트
        TextMeshProUGUI healthText = statusPanel.transform.Find("HealthText").GetComponent<TextMeshProUGUI>();
        healthText.text = healthTextFormatted;

        TextMeshProUGUI ShieldText = statusPanel.transform.Find("ShieldText").GetComponent<TextMeshProUGUI>();
        ShieldText.text = ShieldTextFormatted;

        TextMeshProUGUI speedText = statusPanel.transform.Find("SpeedText").GetComponent<TextMeshProUGUI>();
        speedText.text = speedTextFormatted;

        TextMeshProUGUI AttackText = statusPanel.transform.Find("AttackText").GetComponent<TextMeshProUGUI>();
        AttackText.text = AttackTextFormatted;

        TextMeshProUGUI AgilityText = statusPanel.transform.Find("AgilityText").GetComponent<TextMeshProUGUI>();
        AgilityText.text = AgilityTextFormatted;

        TextMeshProUGUI weightText = statusPanel.transform.Find("WeightText").GetComponent<TextMeshProUGUI>();
        weightText.text = weightTextFormatted;

        UpdateCombatStatusUI();
    }
    private void UpdateEquipment()
    {
        // 장착된 장비명 업데이트
        TextMeshProUGUI weapon1Text = statusPanel.transform.Find("Weapon1Text").GetComponent<TextMeshProUGUI>();
        weapon1Text.text = string.Format("Weapon 1\t: {0}", playerEquipment.GetWeaponName(0));

        TextMeshProUGUI weapon2Text = statusPanel.transform.Find("Weapon2Text").GetComponent<TextMeshProUGUI>();
        weapon2Text.text = string.Format("Weapon 2\t: {0}", playerEquipment.GetWeaponName(1));

        TextMeshProUGUI engineText = statusPanel.transform.Find("EngineText").GetComponent<TextMeshProUGUI>();
        engineText.text = string.Format("Engine\t: {0}", playerEquipment.GetEngineName());

        TextMeshProUGUI shieldText = statusPanel.transform.Find("EquipShieldText").GetComponent<TextMeshProUGUI>();
        shieldText.text = string.Format("Shield\t: {0}", playerEquipment.GetShieldName());

        TextMeshProUGUI wingText = statusPanel.transform.Find("WingText").GetComponent<TextMeshProUGUI>();
        wingText.text = string.Format("Wing\t\t: {0}", playerEquipment.GetWingName());
    }
    private void UpdateCombatStatus(_PlayerManager.Status status)
    {
        string DamageTextFormatted = string.Format("Damage\n{0:F2}", status.Damage);
        string FireRateTextFormatted = string.Format("FireRate\n{0:F2} Fire/sec", 1/status.FireRate);
        string DamageTypeTextFormatted = string.Format("Type\n" + playerEquipment.GetEquipment(Equipment.EquipmentType.Weapon).GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().moveType);
        string Special = string.Format("Special\n"+ playerEquipment.GetEquipment(Equipment.EquipmentType.Weapon).GetComponent<Weapons>().BulletPrefab.GetComponent<Bullet>().attackType);
        string DodgeRateTextFormatted = string.Format("DodgeRate\n{0:F2}", status.DodgeRate);

        TextMeshProUGUI DamageText = statusPanel.transform.Find("DamageText").GetComponent<TextMeshProUGUI>();
        DamageText.text = DamageTextFormatted;
        TextMeshProUGUI FireRateText = statusPanel.transform.Find("FireRateText").GetComponent<TextMeshProUGUI>();
        FireRateText.text = FireRateTextFormatted;
        TextMeshProUGUI DamageTypeText = statusPanel.transform.Find("DamageTypeText").GetComponent<TextMeshProUGUI>();
        DamageTypeText.text = DamageTypeTextFormatted;
        TextMeshProUGUI DodgeRateText = statusPanel.transform.Find("DodgeRateText").GetComponent<TextMeshProUGUI>();
        DodgeRateText.text = DodgeRateTextFormatted;
        TextMeshProUGUI SpecialText = statusPanel.transform.Find("SpecialText").GetComponent<TextMeshProUGUI>();
        SpecialText.text = Special;
    }

    public void UpdateCombatStatusUI()
    {
        TextMeshProUGUI HPText = CombatStatusPanel.transform.Find("HP").GetComponent<TextMeshProUGUI>();
        HPText.text = "HP: " + PlayerHPSystem.instance.getHP() + "/" + PlayerHPSystem.instance.getMaxHP();
        TextMeshProUGUI SheildText = CombatStatusPanel.transform.Find("Shield").GetComponent<TextMeshProUGUI>();
        SheildText.text = "Shield: " + PlayerHPSystem.instance.Shield;
        TextMeshProUGUI DamageText = CombatStatusPanel.transform.Find("Damage").GetComponent<TextMeshProUGUI>();
        DamageText.text = "Damage: " + _PlayerManager.Instance.GetCurrentStatus().Damage;
        TextMeshProUGUI DodgeRateText = CombatStatusPanel.transform.Find("Dodge").GetComponent<TextMeshProUGUI>();
        DodgeRateText.text = "Dodge: " + _PlayerManager.Instance.GetCurrentStatus().DodgeRate + "%";
    }

    void OnPlayerTakeDamage(GameEvents gameEvents)
    {
        UpdateCombatStatusUI();
    }

    private void OnDestroy()
    {
        GameEvents.onPlayerTakeDamage -= OnPlayerTakeDamage;
    }
}

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerHPSystem : MonoBehaviour
{
    private float HP;
    private float maxHP;
    public int Shield;
    public static PlayerHPSystem instance;
    public bool invincible = false;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (_PlayerManager.Instance == null)
        {
            Debug.Log("PlayerManager is null");
            return;
        }
        StartCoroutine(GameSetting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        Shield = _PlayerManager.Instance.GetCurrentStatus().Shield;
        float DodgeRate = _PlayerManager.Instance.GetCurrentStatus().DodgeRate;
        if (invincible)
        {
            return;
        }
        if (Random.Range(0, 100) < DodgeRate)
        {
            return;
        }
        // Decrease HP
        if (Shield >= damage)
        {
            HP -= 1;
        }
        else
        {
            HP -= (damage - Shield);
        }
        if (HP <= 0)
        {
            GameManager.instance.gameEvents.callPlayerDeath();
        }
        StartCoroutine(InvincibleCooltime());
        GameManager.instance.gameEvents.callPlayerTakeDamage();
    }

    private IEnumerator InvincibleCooltime()
    {
        invincible = true;
        yield return new WaitForSeconds(1f);
        invincible = false;
    }

    public void gainHP(float hp)
    {
        HP += hp;
        if (HP > maxHP)
        {
            HP = maxHP;
        }
    }

    public void gainMaxHP(float maxhp)
    {
        maxHP += maxhp;
        HP += maxhp;
    }

    public int getHP()
    {
        return (int)HP;
    }

    public int getMaxHP()
    {
        return (int)maxHP;
    }

    private IEnumerator GameSetting()
    {
        yield return new WaitForSeconds(0.1f);
        maxHP = _PlayerManager.Instance.GetCurrentStatus().Health;
        HP = _PlayerManager.Instance.GetCurrentStatus().Health;
        Shield = _PlayerManager.Instance.GetCurrentStatus().Shield;
        GameObject.Find("StatusUI").GetComponent<StatusUIManager>().UpdateCombatStatusUI();
    }
}

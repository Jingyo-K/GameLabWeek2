using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;

public class EnemyHPSystem : MonoBehaviour
{
    private float HP = 100;
    public float maxHP = 100;
    public int EShield = 0;
    private bool isDead = false;
    public EquipmentDrop equipmentDrop;
    public TextMeshProUGUI HPText;
    public enum EnemyType
    {
        Normal,
        Elite,
        Boss
    }
    public EnemyType enemyType;
    void Start()
    {
       HP = maxHP; 
       equipmentDrop = GameObject.Find("EquipmentDropManager").GetComponent<EquipmentDrop>();
       if(enemyType == EnemyType.Boss)
       {
            HPText = GameObject.Find("BossHP").GetComponent<TextMeshProUGUI>();
       }
       else if(enemyType == EnemyType.Elite)
       {    
            HPText = GameObject.Find("BossHP").GetComponent<TextMeshProUGUI>();
       }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyType == EnemyType.Boss || enemyType == EnemyType.Elite)
        {
            if(HP<=0)
            {
                HPText.text = "";
            }
            else
                HPText.text = "" + HP;
        }
    }
    public void TakeDamage(float damage)
    {
        // Decrease HP
        if (EShield >= damage-1)
        {
            HP -= 1;
        }
        else
        {
            HP -= (damage - EShield);
        }

        // Check if HP is less than 0
        if (HP <= 0)
        {
            if(enemyType == EnemyType.Boss)
            {
                if (isDead)
                    return;
                StartCoroutine(BossDeath());
                isDead = true;
            }
            else if (enemyType == EnemyType.Elite)
            {
                GameManager.instance.gameEvents.callGameEnd();
            }
            else
            {
                GameObject xp = Instantiate(Resources.Load("Prefab/Xp"), transform.position, Quaternion.identity) as GameObject;
                Destroy(gameObject);
            }
            
        }
    }

    IEnumerator BossDeath()
    {
        Debug.Log("Boss is dead");
        GetComponent<EnemyPattern>().StopAllCoroutines();

        for (int i = 0; i < 10*EnemyGenUI.Stage * 2; i++)
        {
            float randX = Random.Range(-2f, 2f);
            float randY = Random.Range(-2f, 2f);
            Vector3 randPos = new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z);
            GameObject xp = Instantiate(Resources.Load("Prefab/Xp"), randPos, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(4f/(10*EnemyGenUI.Stage*2));
        }
        equipmentDrop.DropEquipment();
        yield return new WaitForSeconds(1f);
    if (GameManager.instance != null && GameManager.instance.gameEvents != null)
    {
        GameManager.instance.gameEvents.callNextStage();
        Debug.Log("Call Next Stage");
    }
        Debug.Log("Destroy Boss");
        Destroy(gameObject);
    }

    void OnDestroy()
    {
    }
}

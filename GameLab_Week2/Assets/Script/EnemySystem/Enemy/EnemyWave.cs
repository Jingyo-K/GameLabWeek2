using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public int WaveNumber;
    public GameObject[] enemies;
    GameObject enemy;
    public float WaveTime;

    void Start()
    {
        StartCoroutine(EnemyWaveSpawn());
    }

    void Update()
    {
        if(WaveNumber == 0 && transform.childCount == 0)
        {
            GameManager.instance.gameEvents.callNextStage();
            Destroy(gameObject);
        }
    }
    IEnumerator EnemyWaveSpawn()
    {
        WaveTime = (float)10f/WaveNumber;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float randX = Random.Range(-5f, 5f);
        while(WaveNumber > 0)
        {
            pos = new Vector3(pos.x + randX, pos.y, pos.z);
            int randomEnemy = Random.Range(0, enemies.Length);
            enemy = Instantiate(enemies[randomEnemy], pos, Quaternion.identity);
            enemy.transform.parent = transform;
            enemy.GetComponent<EnemyHPSystem>().maxHP = GetComponent<EnemyHPSystem>().maxHP;
            enemy.GetComponent<EnemyStatus>().Attack = GetComponent<EnemyStatus>().Attack;
            enemy.GetComponent<EnemyHPSystem>().EShield = GetComponent<EnemyHPSystem>().EShield;
            WaveNumber--;
            yield return new WaitForSeconds(WaveTime);
            pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            randX = Random.Range(-5f, 5f);
        }
    }
}

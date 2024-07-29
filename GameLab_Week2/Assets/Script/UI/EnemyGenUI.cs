using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenUI : MonoBehaviour
{
    public GameObject EnemyGenPanel;
    public static EnemyGenUI instance;
    public static int Stage = 0;
    public GameObject[] Wave;
    public GameObject[] Boss;
    GameObject stageEnemy;
    void Start()
    {
        Stage = 0;
        Wave = Resources.LoadAll<GameObject>("Prefab/Enemy/Wave");
        Boss = Resources.LoadAll<GameObject>("Prefab/Enemy/Boss");
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        GameEvents.onNextStage += NextStage;
        GameManager.instance.gameEvents.callNextStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateEnemyGenUI(GameObject stageEnemy)
    {
        // Update EnemyGenUI
        TimeManager.StageUIActive = true;
        EnemyGenPanel.SetActive(true);
        TextMeshProUGUI StageText = GameObject.Find("Stage").GetComponent<TextMeshProUGUI>();
        StageText.text = "Stage " + Stage;
        TextMeshProUGUI TypeText = GameObject.Find("Type").GetComponent<TextMeshProUGUI>();
        if (stageEnemy.tag == "Wave")
        {
            TypeText.text = "Wave";
        }
        else
        {
            TypeText.text = "Boss";
        }
        TextMeshProUGUI NameText = GameObject.Find("Number / Name").GetComponent<TextMeshProUGUI>();
        if(stageEnemy.tag == "Wave")
        {
            NameText.text = "Number\t: "+stageEnemy.GetComponent<EnemyWave>().WaveNumber;
        }
        else
        {
            NameText.text = "Name\t: " + stageEnemy.name;
        }
        TextMeshProUGUI HPText = GameObject.Find("EnemyHP").GetComponent<TextMeshProUGUI>();
        HPText.text = string.Format("HP\t\t: {00000}", stageEnemy.GetComponent<EnemyHPSystem>().maxHP);
        TextMeshProUGUI AttackText = GameObject.Find("Attack").GetComponent<TextMeshProUGUI>();
        AttackText.text = string.Format("Attack\t: {00000}", stageEnemy.GetComponent<EnemyStatus>().Attack);
        TextMeshProUGUI ShieldText = GameObject.Find("EnemyShield").GetComponent<TextMeshProUGUI>();
        ShieldText.text = string.Format("Shield\t: {00000}", stageEnemy.GetComponent<EnemyHPSystem>().EShield);
    }
    void NextStage(GameEvents gameEvents) 
    {
        Stage++;
        int random = Random.Range(0, 2);
        if(Stage == 1)
        {
            random = 1;
        }
        if(Stage % 4 == 0)
        {
            random = 1;
        }
        if(Stage == 20)
        {
            random = 2;
        }
        if (random == 0)
        {
            stageEnemy = Instantiate(Wave[Random.Range(0, Wave.Length)]);
            stageEnemy.name = Wave[Random.Range(0, Wave.Length)].name;
            stageEnemy.GetComponent<EnemyWave>().WaveNumber += Stage*5;
            stageEnemy.GetComponent<EnemyHPSystem>().maxHP *= Stage;
            stageEnemy.GetComponent<EnemyStatus>().Attack *= Stage;
            stageEnemy.GetComponent<EnemyHPSystem>().EShield *= Stage;
            stageEnemy.transform.position = new Vector3(0, 18, 0);
        }
        else if(random == 1)
        {
            stageEnemy = Instantiate(Boss[Random.Range(0, Boss.Length)]);
            stageEnemy.name = Boss[Random.Range(0, Boss.Length)].name;
            stageEnemy.GetComponent<EnemyHPSystem>().maxHP *= Stage;
            stageEnemy.GetComponent<EnemyStatus>().Attack *= Stage;
            stageEnemy.GetComponent<EnemyHPSystem>().EShield *= Stage;
            stageEnemy.transform.position = new Vector3(0, 14, 0);
        }
        else
        {
            stageEnemy = Instantiate(Resources.Load<GameObject>("Prefab/Enemy/FinalBoss"));
            stageEnemy.name = "FinalBoss";
            stageEnemy.transform.position = new Vector3(0, 14, 0);
        }   
        updateEnemyGenUI(stageEnemy);
    }

    public void StartStage()
    {
        EnemyGenPanel.SetActive(false);
        TimeManager.StageUIActive = false;
        StartCoroutine(StartStageCoroutine(stageEnemy));
    }

    IEnumerator StartStageCoroutine(GameObject enemy)
    {
        yield return new WaitForSeconds(1.0f);
    }

    void OnDestroy()
    {
        GameEvents.onNextStage -= NextStage;
    }
}

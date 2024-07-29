using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLVSystem : MonoBehaviour
{
    public static PlayerLVSystem instance;
    public int playerLV = 1;
    public int playerEXP = 0;
    public int playerMaxEXP = 10;
    public TextMeshProUGUI LVText;
    
    public GameObject LVUpChoicePanel1;
    public GameObject LVUpChoicePanel2;
    public GameObject LVUpChoicePanel3;

    void Start()
    {
        playerLV = 1;
        playerEXP = 0;
        playerMaxEXP = 10;
        PlayerLVSystem.instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        LVText.text = "LV " + playerLV + "\n\n" + playerEXP + "/" + playerMaxEXP;
        if(playerEXP >= playerMaxEXP)
        {
            playerEXP -= playerMaxEXP;
            playerLV++;
            playerMaxEXP = playerLV * 10;
            LVUp();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            playerEXP += 10;
        }
    }

    void LVUp()
    {
        TimeManager.LVUpChoicePanelActive = true;
        RandomLVUpChoice(LVUpChoicePanel1);
        RandomLVUpChoice(LVUpChoicePanel2);
        RandomLVUpChoice(LVUpChoicePanel3);
        LVUpChoicePanel1.GetComponent<LVUpChoice>().SetLVText();
        LVUpChoicePanel2.GetComponent<LVUpChoice>().SetLVText();
        LVUpChoicePanel3.GetComponent<LVUpChoice>().SetLVText();
        LVUpChoicePanel1.SetActive(true);
        LVUpChoicePanel2.SetActive(true);
        LVUpChoicePanel3.SetActive(true);
    }

    public void RandomLVUpChoice(GameObject LVUpChoicePanel)
    {
        LVUpChoice lv = LVUpChoicePanel.GetComponent<LVUpChoice>();
        int Option = Random.Range(0, 4);
        switch (Option)
        {
            case 0:
                lv.SetOption("HP");
                lv.SetAmount(Random.Range(1, 5));
                break;
            case 1:
                lv.SetOption("Shield");
                lv.SetAmount(Random.Range(1, 3));
                break;
            case 2:
                lv.SetOption("ATK");
                lv.SetAmount(Random.Range(1, 4));
                break;
            case 3:
                lv.SetOption("Agility");
                lv.SetAmount(Random.Range(2, 6));
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LVUpChoice : MonoBehaviour
{
    public TextMeshProUGUI LVText;
    public string Option;
    public int amount;
    public _PlayerManager.Status status;

    public void SetOption(string option)
    {
        Option = option;
    }
    public void SetAmount(int Amount)
    {
        amount = Amount;
    }
    public void SetLVText()
    {
        LVText.text = Option + " + " + amount;
    }
    public void Click()
    {
        if (Option == "HP")
        {
            status.Health += amount;
            _PlayerManager.Instance.AddBaseStatus(status);
            PlayerHPSystem.instance.gainMaxHP(amount);
            status.Health -= amount;
        }
        else if (Option == "ATK")
        {
            status.Attack += amount;
            _PlayerManager.Instance.AddBaseStatus(status);
            status.Attack -= amount;
        }
        else if (Option == "Shield")
        {
            status.Shield += amount;
            _PlayerManager.Instance.AddBaseStatus(status);
            PlayerHPSystem.instance.Shield += amount;
            status.Shield -= amount;
        }
        else if (Option == "Agility")
        {
            status.Agility += amount;
            _PlayerManager.Instance.AddBaseStatus(status);
            status.Agility -= amount;
        }
        _PlayerManager.Instance.StatusUpdate();
        TimeManager.LVUpChoicePanelActive = false;
        for(int i = 0; i < transform.parent.childCount-1; i++)
        {
            transform.parent.GetChild(i).gameObject.SetActive(false);
        }
    }
}

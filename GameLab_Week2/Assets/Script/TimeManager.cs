using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public static bool isStatusPanelActive = false;
    public static bool isChangePanelActive = false;
    public static bool isPausePanelActive = false;
    public static bool StageUIActive = false;
    public static bool LVUpChoicePanelActive = false;
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
        isStatusPanelActive = false;
        isChangePanelActive = false;
        isPausePanelActive = false;
        StageUIActive = false;
        LVUpChoicePanelActive = false;   
    }
    // Update is called once per frame
    void Update()
    {
        if(isStatusPanelActive || isChangePanelActive || isPausePanelActive || StageUIActive || LVUpChoicePanelActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }


}

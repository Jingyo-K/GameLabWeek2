using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverClear : MonoBehaviour
{
    public Canvas GameOverClearCanvas;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.onPlayerDeath += GameOver;
        GameEvents.onGameEnd += Clear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOver(GameEvents gameEvents)
    {
        GameOverClearCanvas.gameObject.SetActive(true);
        TimeManager.isPausePanelActive = true;
        TextMeshProUGUI GameOverText = GameObject.Find("GameOverClearText").GetComponent<TextMeshProUGUI>();
        GameOverText.text = "Game Over";
        TextMeshProUGUI StageText = GameObject.Find("Stage").GetComponent<TextMeshProUGUI>();
        StageText.text = "Stage " + EnemyGenUI.Stage;
        Debug.Log("Game Over");
    }

    void Clear(GameEvents gameEvents)
    {
        GameOverClearCanvas.gameObject.SetActive(true);
        TimeManager.isPausePanelActive = true;
        TextMeshProUGUI GameOverText = GameObject.Find("GameOverClearText").GetComponent<TextMeshProUGUI>();
        GameOverText.text = "Game Clear";
        TextMeshProUGUI StageText = GameObject.Find("Stage").GetComponent<TextMeshProUGUI>();
        StageText.text = "Stage " + EnemyGenUI.Stage;
        Debug.Log("Game Clear");
    }

    public void Restart()
    {   
        TimeManager.isPausePanelActive = false;
        SceneManager.LoadScene("PlayerSystem");
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        GameEvents.onPlayerDeath -= GameOver;
        GameEvents.onGameEnd -= Clear;
    }
}

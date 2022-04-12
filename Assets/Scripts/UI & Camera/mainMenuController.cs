using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuController : MonoBehaviour
{
    [SerializeField] private GameFlowController gfc;
    [SerializeField] private GameObject pausedCanvas;
    public bool paused = false;
    public int scene = 0;


    private void Update() {
        if(scene > 0)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                paused = true;
                pausedCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void loadGame()
    {
        gfc.LoadScene("carLoop");
        PlayerPrefs.SetInt("scene", 1);
        PlayerPrefs.Save();
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        paused = false;
        pausedCanvas.SetActive(false);
    }

    public void restart()
    {
        gfc.LoadScene("carLoop");
    }
}

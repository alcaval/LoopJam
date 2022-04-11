using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuController : MonoBehaviour
{
    [SerializeField] private GameFlowController gfc;
    private bool inPlayScene = false;
    [SerializeField] private GameObject pausedCanvas;

    private void Start() {
        PlayerPrefs.SetInt("scene", 1);
        PlayerPrefs.Save();
    }

    private void Update() {
        if(PlayerPrefs.GetInt("scene") > 0)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
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
        PlayerPrefs.SetInt("scene", 0);
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        pausedCanvas.SetActive(false);
    }
}

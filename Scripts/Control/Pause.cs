using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Pause : MonoBehaviour
{
    public Button pause;
    public GameObject pausePanel;
    public Button resume;
    public Button menu;
    public Button reload;
    
    void Start()
    {
        if(pause == null)
        {
            pause = GetComponent<Button>();
        }
        pause = GetComponent<Button>();
        pausePanel = GameObject.Find("GamePause");
        resume = GameObject.Find("Play").GetComponent<Button>();
        reload = GameObject.Find("Reload").GetComponent<Button>();
        menu = GameObject.Find("Menu").GetComponent<Button>();

        pause.onClick.AddListener(GamePause);
        resume.onClick.AddListener(Resume);
        //menu.onClick.AddListener(LoadMenu);
        reload.onClick.AddListener(Reload);
        pausePanel.SetActive(false);

    }

    void Update()
    {
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.instance.paused = false;
    }
    public void GamePause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        GameManager.instance.paused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Reload()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(y);
        Time.timeScale = 1.0f;
    }


}

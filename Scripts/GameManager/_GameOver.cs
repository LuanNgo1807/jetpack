using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject timeout;
    public AudioSource audioSource;
    private void Start()
    {
        gameOverPanel = GameObject.Find("GameOver");
        timeout = GameObject.Find("TimeOut");
        gameOverPanel.SetActive(false);
        timeout.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.instance.live <= 0)
        {
            Invoke("GameOver", 1);
        }
        if(GameManager.instance.time <= 0)
        {
            Invoke("TimeOut", 0);
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        audioSource.Play();
    }
    public void TimeOut()
    {
        timeout.SetActive(true);
        audioSource.Play();
    }
}

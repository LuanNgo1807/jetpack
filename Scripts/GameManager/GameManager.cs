using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coins;
    public int live;
    public bool powerUp;
    public float time;
    public int score;
    public bool paused = false;
    public int level = 1;
    public int amountApple = 3;

    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textCountScore;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject gameObject = new GameObject("GameManager");
                    instance = gameObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        LoadData();
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadData()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            textCoin = GameObject.Find("_textCoin").GetComponent<TextMeshProUGUI>();
            textTime = GameObject.Find("_textTime").GetComponent<TextMeshProUGUI>();
            textScore = GameObject.Find("_textScore").GetComponent<TextMeshProUGUI>();
            textCountScore = GameObject.Find("_textCountScore").GetComponent<TextMeshProUGUI>();
            UpdateCountApple();
            UpdateTextCoin();
            live = 1;
            time = 240;
        }
    }
    void Update()
    {
        if(textTime != null)
        {
            UpdateTextTime();
        }
        if (textCoin == null)
        {
            LoadData();
        }
        if(live <= 0)
        {

            audioSource2.Play();
        }
        if(time <= 0)
        {
            time = 0;
            live = 0;
        }
    }

    public void UpdateTextCoin()
    {
        textCoin.text = "COIN\n" + coins;
        audioSource.Play();
        Save();
    }
    public void UpdateCountApple()
    {
        textCountScore.text = "" + amountApple;
    }
    public void UpdateTextScore()
    {
        textScore.text = "SCORE\n" + score;
    }

    public void UpdateTextTime()
    {
        time = time - Time.deltaTime;
        int _time = (int)time;
        textTime.text = "TIME\n" + _time;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("coin", coins);
    }

    public void Load()
    {
        coins = PlayerPrefs.GetInt("coin");
    }
}

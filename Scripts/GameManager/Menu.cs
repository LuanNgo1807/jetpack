using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public int x;
    public Button buttonLevel;
    private void OnEnable()
    {
        buttonLevel = GetComponent<Button>();
        try
        {
            x = int.Parse(gameObject.name);
            buttonLevel.onClick.AddListener(LoadLevel);
        }
        catch (System.Exception e)
        {

        }

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(x);
        Time.timeScale = 1.0f;
    }

}
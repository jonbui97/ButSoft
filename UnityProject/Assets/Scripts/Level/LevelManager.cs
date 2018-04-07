using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private static LevelManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
        PlayerPrefs.SetString("Continue", "No");

    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void LoadLevelAndData()
    {
        SceneManager.LoadScene(SaveLoadManager.GetLevel());
        PlayerPrefs.SetString("Continue", "Yes");
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLoader : MonoBehaviour
{
    private static GameLoader _instance;
    public static GameLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameLoader>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadGame();
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
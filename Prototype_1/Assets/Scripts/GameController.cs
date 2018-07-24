using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Singleton Stuff
    public static GameController instance;

    // Game Condition

    public int finalGameLevel;
    public int currGameLevel;

    // Object Tracker

    //public GameObject pPlayer;
    //public GameObject pSlime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

	void Start ()
    {
        currGameLevel = 1;

    }
	
	void Update ()
    {
		
	}

    public void LevelFailed()
    {
        currGameLevel = 1;
        SceneManager.LoadScene("GameOverScene");
    }

    public void LevelPassed()
    {
        if (currGameLevel != finalGameLevel)
        {
            // Get to the next level
            currGameLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            // Go to final scene
            currGameLevel = 1;
            SceneManager.LoadScene("PrototypeEndScene");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

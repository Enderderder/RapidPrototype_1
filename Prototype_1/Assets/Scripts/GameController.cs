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

	private GameObject winCanvas;
	private GameObject lossCanvas;
	private GameObject playerUI;
		

    //public GameObject pPlayer;
    //public GameObject pSlime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

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
		SceneManager.LoadScene("GameOverScene");
    }

    public void LevelPassed()
    {
		winCanvas.SetActive(true);
		playerUI.SetActive(false);
		lossCanvas.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level2");
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
		currGameLevel = 1;
		SceneManager.LoadScene("MainMenu");
    }

	public void RetryLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
	}

	public void NextLevel()
	{
		if (currGameLevel != finalGameLevel)
		{
			// Get to the next level
			//currGameLevel++;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
			// Go to final scene
			//currGameLevel = 1;
			SceneManager.LoadScene("PrototypeEndScene");

		}
	}


}

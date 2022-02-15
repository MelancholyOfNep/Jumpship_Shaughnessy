using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;

	[SerializeField]
	Transform respawnPt; // player respawn location
	[SerializeField]
	GameObject playerPF; // prefab for respawning player
	[SerializeField]
	TextMeshProUGUI scoreText, livesText, timerText; // text fields for score, lives, timer
	[SerializeField]
	float timeLimit; // time limit for the level

	public int score = 0; // counts the score for the UI
	public int livesCount; // lives counter for the scene management and UI

	bool timerRunning = false; // shouldthe timer be running? y/n

	/* [SerializeField]
	int enemyCount; // # of enemies */

	private void Awake()
	{
		Instance = this;
		ScoreUp(0); // display the score by triggering the function without an increase
		Respawn(); // display the life counter by triggering function with an extra life
	}

	private void Start()
	{
		timerRunning = true;
	}

	void Update()
	{
		Timer(); // runs the timer
		DisplayTime(); // displays the timer

		// maybe something like:
		// if (timerRunning != true)
			// SceneManager.LoadScene("VictoryScene");

		if (livesCount == 0)
			SceneManager.LoadScene("GameOverScene");

		if (Input.GetButtonDown("Cancel"))
			SceneManager.LoadScene("MainMenuScene");
	}

	public void Respawn()
	{
		Instantiate(playerPF, respawnPt.position, Quaternion.identity); // instantiate player @ spawn pt
		livesCount--; // tick down lives count
		livesText.text = livesCount.ToString(); // lives text updated to match lives count
	}

	public void ScoreUp(int scoreValue)
	{
		score += scoreValue;
		scoreText.text = score.ToString();
	}

	void Timer()
	{
		if(timerRunning)
		{
			if (timeLimit > 0)
				timeLimit -= Time.deltaTime;
			else
			{
				Debug.Log("Time Out!");
				timeLimit = 0;
				timerRunning = false;
			}
		}
	}

	void DisplayTime()
    {
		float timeInt = Mathf.FloorToInt(timeLimit);
		timerText.text = timeInt.ToString();
    }
}

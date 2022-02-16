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
	TextMeshProUGUI scoreText, livesText; // text fields for score, lives

	public int score = 0; // counts the score for the UI
	public int livesCount = 3; // lives counter for the scene management and UI
	public bool bossDead = false;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		ScoreUp(0); // display the score by triggering the function without an increase
		livesText.text = livesCount.ToString();
	}

	void Update()
	{
		if (bossDead == true)
			// delay the transition so we can watch the fireworks!
			StartCoroutine(VictoryTransition());
		if (livesCount == 0)
			SceneManager.LoadScene("GameOverScene");
		if (Input.GetButtonDown("Cancel"))
			SceneManager.LoadScene("MainMenuScene");
	}

	public void Respawn()
	{
		// Instantiate(playerPF, respawnPt.position, Quaternion.identity); // instantiate player @ spawn pt
		livesCount--; // tick down lives count
		livesText.text = livesCount.ToString(); // lives text updated to match lives count
	}

	public void ScoreUp(int scoreValue)
	{
		score += scoreValue;
		scoreText.text = score.ToString();
	}

	IEnumerator VictoryTransition()
    {
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("VictoryScene");
	}
}


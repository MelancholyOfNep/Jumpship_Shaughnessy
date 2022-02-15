using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public static EnemySpawner Instance;

	[SerializeField]
	Transform[] enemySpawnPoints; // spawn points for enemies
	[SerializeField]
	GameObject enemyPF; // prefab for fallers
	[SerializeField]
	GameObject shooterEnemyPF; // prefab for shooters
	[SerializeField]
	GameObject whyDoIHearBossMusic; // prefab for boss

	public float shootersDefeated; // number of shooters defeated

	private void Start()
	{
		Instance = this;
	}

	public void SpawnEnemy(float enemyType)
	{
		if (enemyType == 0)
		{
			if (shootersDefeated < 5)
			{
				int randomNum = Mathf.RoundToInt(Random.Range(0f, enemySpawnPoints.Length - 1)); // picks random spawn pt
				Instantiate(enemyPF, enemySpawnPoints[randomNum].transform.position, Quaternion.identity); // spawns faller @ chosen spawn pt
			}
		}

		else if (enemyType == 1)
		{
			if (shootersDefeated < 5)
			{
				int randomNum = Mathf.RoundToInt(Random.Range(0f, enemySpawnPoints.Length - 1)); // picks random spawn pt
				Instantiate(shooterEnemyPF, enemySpawnPoints[randomNum].transform.position, Quaternion.identity); // spawns shooter @ chosen spawn pt
			}
			else if (shootersDefeated>=5)
			{
				Instantiate(whyDoIHearBossMusic, new Vector3(0, 10, 0), Quaternion.identity); // spawns boss @ boss position
				// maybe make it spawn a bit higher and then stop at trigger?
			}
		}
	}
}

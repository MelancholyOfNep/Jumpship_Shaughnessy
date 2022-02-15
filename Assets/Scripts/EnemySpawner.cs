using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public static EnemySpawner Instance;

	[SerializeField]
	Transform[] enemySpawnPoints;
	[SerializeField]
	GameObject enemyPF;
	[SerializeField]
	GameObject shooterEnemyPF;

	private void Start()
	{
		Instance = this;
	}

	public void SpawnEnemy(float enemyType)
	{
        if (enemyType == 0)
        {
            int randomNum = Mathf.RoundToInt(Random.Range(0f, enemySpawnPoints.Length - 1));
            Instantiate(enemyPF, enemySpawnPoints[randomNum].transform.position, Quaternion.identity);
        }

		else if (enemyType == 1)
        {
			int randomNum = Mathf.RoundToInt(Random.Range(0f, enemySpawnPoints.Length - 1));
			Instantiate(shooterEnemyPF, enemySpawnPoints[randomNum].transform.position, Quaternion.identity);
		}
	}
}

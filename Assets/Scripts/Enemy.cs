using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	Rigidbody2D rb;

	[SerializeField] // serialized fields for the benefit of using with multiple prefabs!
	float ySpeed, fireRate; // enemy's movement and firing frequency
	[SerializeField]
	int scoreVal; // scoring value of the enemy type
	[SerializeField]
	float enemyType; // designates enemy type, 0 for faller, 1 for shooter, 2 for boss
	[SerializeField]
	float health; // designates health for shooter
	[SerializeField]
	GameObject bullet, gun, deathExpl; // designates the enemy bullet, as well as firing location and explosion effect
	[SerializeField]
	SpriteRenderer sprite; // renderer for sprite for damageframes


	readonly float dmgAmount = 10; // designates damage of player bullet
	Color spriteColorHolder; // placeholder for default sprite color

	public static Enemy Instance;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>(); // get the damn rigidbody automatically
		sprite = GetComponent<SpriteRenderer>(); // get the sprite renderer
		spriteColorHolder = sprite.color; // get default sprite color
	}

	// Start is called before the first frame update
	void Start()
	{
		Instance = this;
		if (enemyType == 1)
			InvokeRepeating(nameof(Shoot), fireRate, fireRate);
	}

	// Update is called once per frame
	void Update()
	{
		rb.velocity = transform.up * ySpeed; // basic movement. Will generally just fall through the screen.
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (enemyType == 0)
		{
			if (collision.gameObject.CompareTag("Bullet"))  // upon hitting the bullet
			{
				Instantiate(deathExpl, transform.position, Quaternion.identity);
				LevelManager.Instance.ScoreUp(scoreVal);
				Die(); // suicide
			}

			else if (collision.gameObject.CompareTag("Player"))
			{
				Instantiate(deathExpl, transform.position, Quaternion.identity);
				Die(); // suicide
			}

			else if (collision.gameObject.CompareTag("EnemyBound"))
            {
				Die(); // suicide
            }
		}
		else if (enemyType == 1)
		{
			if (collision.gameObject.CompareTag("Bullet"))
			{
				Damage();
			}
			else if (collision.gameObject.CompareTag("EnemyBound"))
			{
				Die(); // suicide
			}
		}
	}

	void Damage()
	{
		health -= dmgAmount;
		StartCoroutine(nameof(DamageFlash));

		if (health <= 0)
		{
			LevelManager.Instance.ScoreUp(scoreVal);
			if (enemyType==1)
				EnemySpawner.Instance.shootersDefeated++;
			Instantiate(deathExpl, transform.position, Quaternion.identity);
			Die();
		}
	}

	void Die()
	{
		EnemySpawner.Instance.SpawnEnemy(enemyType);
		Destroy(gameObject); // suicide
	}

	void Shoot()
	{
		Instantiate(bullet, gun.transform.position, Quaternion.identity);
	}

	IEnumerator DamageFlash()
    {
		sprite.color = Color.white;
		yield return new WaitForSeconds(.1f);
		sprite.color = spriteColorHolder;
    }
}

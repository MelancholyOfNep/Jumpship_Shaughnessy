using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	Rigidbody2D rb;

	[SerializeField] // serialized fields for the benefit of using with multiple prefabs!
	float ySpeed, fireRate; // enemy's movement and firing frequency
	[SerializeField]
	int scoreVal; // scoring value of the enemy type
	[SerializeField]
	float enemyType; // designates enemy type. it's the boss, so it's 2.
	[SerializeField]
	float health; // designates health for boss
	[SerializeField]
	GameObject bullet, gun1, gun2, gun3, gun4, gun5; // designates the enemy bullet, as well as firing location

	readonly float dmgAmount = 10; // designates damage of player bullet

	bool BossStopped = false; // stops the boss from moving

	public static Boss Instance;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>(); // get the damn rigidbody automatically
	}

	// Start is called before the first frame update
	void Start()
	{
		Instance = this;
	}

	// Update is called once per frame
	void Update()
	{
		if (BossStopped != true)
			rb.velocity = transform.up * ySpeed; // basic movement. Will generally just fall through the screen.
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Bullet"))
		{
			Damage();
		}
		else if (collision.gameObject.CompareTag("BossStopper"))
		{
			BossStopped = true;
			rb.velocity = transform.up * 0;
			rb.position = new Vector3(0, 5, 0);
			InvokeRepeating(nameof(Shoot), fireRate, fireRate);
		}
	}

	void Damage()
	{
		health -= dmgAmount;

		if (health <= 0)
		{
			LevelManager.Instance.ScoreUp(scoreVal);
			Die();
		}
	}

	void Die()
	{
		// explosion effect
		// screen shake?
		// sound!
		Destroy(gameObject); // suicide
	}

	void Shoot()
	{
		Instantiate(bullet, gun1.transform.position, Quaternion.identity);
		Instantiate(bullet, gun2.transform.position, Quaternion.identity);
		Instantiate(bullet, gun3.transform.position, Quaternion.identity);
		Instantiate(bullet, gun4.transform.position, Quaternion.identity);
		Instantiate(bullet, gun5.transform.position, Quaternion.identity);
	}
}

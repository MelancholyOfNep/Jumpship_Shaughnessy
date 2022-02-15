using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	Rigidbody2D rb;

	[SerializeField]
	GameObject gun, bullet, explosion; // object for gun, bullet, and death expl
	[SerializeField]
	float moveSpeedX, moveSpeedY, fireRate; // speed at which player can move horizontally and vertically, and rate of fire
	[SerializeField]
	AudioSource deathExplSound; // death expl sfx
	[SerializeField]
	SpriteRenderer rend; // sprite renderer for Player
	[SerializeField]
	PolygonCollider2D col; // collider for Player
	
	float cooldown = 0f;


	public static Spaceship Instance;

	//Botched damage system. Remove later.
	/* [SerializeField]
	int health = 3; // health, which may or may not be moved to a central game controller for UI purposes */


	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>(); // get Rigidbody2D on wake
		Instance = this;
	}

	// Update is called once per frame
	void Update()
	{
		// receive yinput, turn that input into vertical movement using velocity modifier
		float yinput = Input.GetAxisRaw("Vertical");
		float moveByY = yinput * moveSpeedY;
		rb.velocity = new Vector2(rb.velocity.x, moveByY);

		// receive xinput, turn that input into horizontal movement using velocity modifier
		float xinput = Input.GetAxisRaw("Horizontal");
		float moveByX = xinput * moveSpeedX;
		rb.velocity = new Vector2(moveByX, rb.velocity.y);

		if (Input.GetButton("Fire1") && cooldown < Time.time)
		{
			Shoot();
			cooldown = Time.time + fireRate;
		}
	}

	// Botched damage system. Remove later
	/* public void Damage()
	{
		health--; // when hit, take damage / lose health
		if (health == 0)
			Destroy(gameObject); // suicide @ 0 HP
	} */

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy")
			|| collision.gameObject.CompareTag("EnemyBullet")) // when contacting an enemy
		{
			deathExplSound.Play();
			rend.enabled = false;
			col.enabled = false;
			// Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject); // die
			LevelManager.Instance.Respawn(); // respawn
		}
	}

	void Shoot()
	{
		Instantiate(bullet, gun.transform.position, Quaternion.identity);
	}
}

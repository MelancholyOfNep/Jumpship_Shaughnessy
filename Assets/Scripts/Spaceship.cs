using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	public static Spaceship Instance;

	Rigidbody2D rb;

	[SerializeField]
	GameObject gun, bullet; //explosion; // object for gun, bullet, and death expl
	[SerializeField]
	float moveSpeedX, moveSpeedY, fireRate; // speed at which player can move horizontally and vertically, and rate of fire
	[SerializeField]
	Collider2D coll;
	[SerializeField]
	SpriteRenderer rend;

	Color spriteColor; // stores original color of sprite


	float cooldown = 0f;


	private void Start()
	{
		rb = GetComponent<Rigidbody2D>(); // get Rigidbody2D on start
		Instance = this;
		coll = GetComponent<Collider2D>(); // get collider on start
		spriteColor = rend.color;
		StartCoroutine(HitboxCycle());
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy")
			|| collision.gameObject.CompareTag("EnemyBullet")) // when contacting an enemy
		{
			// Instantiate(explosion, transform.position, Quaternion.identity);
			StartCoroutine(HitboxCycle());
			LevelManager.Instance.Respawn(); // respawn
		}
	}

	public void Shoot()
	{
		Instantiate(bullet, gun.transform.position, Quaternion.identity);
	}

	IEnumerator HitboxCycle()
	{
		coll.enabled = false;
		InvokeRepeating(nameof(BlinkTrigger), 0f, .2f); // repeat the blink
		Debug.Log("Start");
		yield return new WaitForSeconds(1);
		CancelInvoke(nameof(BlinkTrigger)); // stop the blink
		StartCoroutine(ReturnToPositiveState()); // wait, re-enable collider and set color to normal
		Debug.Log("End");
	}

	void BlinkTrigger()
    {
		StartCoroutine(Blink());
    }

	IEnumerator Blink()
	{
		rend.color = Color.yellow;
		yield return new WaitForSeconds(.1f);
		rend.color = Color.clear;
	}

	IEnumerator ReturnToPositiveState()
    {
		yield return new WaitForSeconds(.1f);
		coll.enabled = true;
		rend.color = spriteColor;
	}
}

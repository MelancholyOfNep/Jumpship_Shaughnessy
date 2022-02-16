using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Spaceship : MonoBehaviour
{
	public static Spaceship Instance;

	Rigidbody2D rb;

	[SerializeField]
	GameObject gun, bullet; //explosion; // object for gun, bullet, and death expl
	[SerializeField]
	float moveSpeedX, moveSpeedY, fireRate; // speed at which player can move horizontally and vertically, and rate of fire
	public Collider2D coll; // the collider of the player
	[SerializeField]
	SpriteRenderer rend; // the sprite of the ship
	[SerializeField]
	AudioSource hitExplSfx; // sound effect of being hit

	Color spriteColor; // stores original color of sprite


	float cooldown = 0f;


	private void Start()
	{
		rb = GetComponent<Rigidbody2D>(); // get Rigidbody2D on start
		Instance = this;
		coll = GetComponent<Collider2D>(); // get collider on start
		coll.enabled = true; // ensures that the collider is on, just in case
		spriteColor = rend.color; // stores the standard color of the ship
		StartCoroutine(HitboxCycle()); // invincibility frames! see later code.
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
			hitExplSfx.Play();
			StartCoroutine(HitboxCycle()); // triggers invincibility frames for appx. 1.2s
			LevelManager.Instance.Respawn(); // respawn
		}
	}

	public void Shoot()
	{
		Instantiate(bullet, gun.transform.position, Quaternion.identity);
	}

	IEnumerator HitboxCycle()
	{
		coll.enabled = false; // turn off the collider
		InvokeRepeating(nameof(BlinkTrigger), 0f, .2f); // repeatedly calls the function to start the Blink coroutine
		yield return new WaitForSeconds(1); // keep the collider off and the blinks recurring for a second
		CancelInvoke(nameof(BlinkTrigger)); // stop the blink
		StartCoroutine(ReturnToPositiveState()); // wait, re-enable collider and set color to normal
	}

	void BlinkTrigger()
	{
		StartCoroutine(Blink());
		/*	function simply calls the coroutine due to not
			being able to Invoke IEnumerators repeatedly	*/
	}

	IEnumerator Blink()
	{
		rend.color = Color.yellow; // make the sprite yellow
		yield return new WaitForSeconds(.1f); // wait .1 seconds
		rend.color = Color.clear; // make the sprite clear
		// this then repeats, giving off a blink effect!
	}

	IEnumerator ReturnToPositiveState()
	{
		// critical! waits for the blink to stop! if it doesn't wait, the sprite may turn clear again!
		yield return new WaitForSeconds(.1f);
		coll.enabled = true; // re-enable collider
		rend.color = spriteColor; // return the sprite to its normal color
	}
}

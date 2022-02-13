using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	Rigidbody2D rb;

	[SerializeField]
	float xSpeed, ySpeed, fireRate, health;
	[SerializeField]
	bool shoots;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		rb.velocity = new Vector2(xSpeed, ySpeed);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag=="Player")
		{
			collision.gameObject.GetComponent<Spaceship>().Damage();
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}

	public void Damage()
	{
		health--;
		if (health == 0)
			Die();
	}
}

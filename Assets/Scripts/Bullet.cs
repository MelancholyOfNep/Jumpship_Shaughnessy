using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	Rigidbody2D rb;
	[SerializeField]
	int shotSpeed = 6;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		rb.velocity = transform.up * shotSpeed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}

	// Botched the damage system. Remove later.
	/* private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Enemy"))
		{
			Enemy.Instance.Damage();
		}
	} */
}

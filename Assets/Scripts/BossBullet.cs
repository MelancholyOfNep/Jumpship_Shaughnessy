using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
	public float shotSpeed;
	public Rigidbody2D rb;

	Vector2 moveDir;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		Vector3 spaceshipPos = (Spaceship.Instance.transform.position - transform.position).normalized * 10;
		moveDir = spaceshipPos;
		rb.velocity = new Vector2(moveDir.x, moveDir.y).normalized * shotSpeed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	public static EnemyBullet Instance;

	Rigidbody2D rb;
	[SerializeField]
	int shotSpeed = -3;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		Instance = this;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	[SerializeField]
	Rigidbody2D rb;
	[SerializeField]
	float moveSpeedX, moveSpeedY;
	[SerializeField]
	int health = 3;

	/* private void Awake()
	{
		rb = GetComponent<Rigidbody2D>(); // get Rigidbody2D on wake
	} */

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		float yinput = Input.GetAxisRaw("Vertical");
		float moveByY = yinput * moveSpeedY;
		rb.velocity = new Vector2(rb.velocity.x, moveByY);

		float xinput = Input.GetAxisRaw("Horizontal");
		float moveByX = xinput * moveSpeedX;
		rb.velocity = new Vector2(moveByX, rb.velocity.y);
	}

	public void Damage()
    {
		health--;
		if (health == 0)
			Destroy(gameObject);
    }
}

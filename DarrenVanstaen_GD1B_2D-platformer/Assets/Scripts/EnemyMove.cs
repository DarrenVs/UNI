using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	
	public float maxJumpCooldown;
	float jumpCooldown;

	Rigidbody2D rb;

	bool grounded = false;
	int dir = 1;

	void OnCollisionStay2D (Collision2D collision) {
		if (collision.transform.tag != transform.tag && transform.position.y > collision.transform.position.y) {
			Health health = collision.transform.GetComponent<Health> ();
			if (health)
				health.TakeDamage (100);
		}

		if (jumpCooldown <= 0 && transform.position.x > collision.transform.position.x && transform.position.y - 0.2f < collision.transform.position.y) {
			rb.velocity = new Vector3 (0, rb.velocity.y) + new Vector3 (5, 4);
			dir = 1;
			jumpCooldown = maxJumpCooldown;

		} else if (jumpCooldown <= 0 && transform.position.x < collision.transform.position.x && transform.position.y - 0.1f < collision.transform.position.y) {
			rb.velocity = new Vector3 (0, rb.velocity.y) + new Vector3 (-5, 4);
			dir = -1;
			jumpCooldown = maxJumpCooldown;
		}

		rb.velocity = new Vector2(rb.velocity.x/10, rb.velocity.y) + Vector2.right * dir;
	}
	
	
	void OnTriggerStay2D () {
		grounded = true;
	}
	
	void OnTriggerExit2D () {
		grounded = false;
	}

	
	void Start () {
		
		rb = transform.GetComponent<Rigidbody2D> ();
	}

	
	void FixedUpdate () {
		jumpCooldown -= Time.deltaTime;
	}
}

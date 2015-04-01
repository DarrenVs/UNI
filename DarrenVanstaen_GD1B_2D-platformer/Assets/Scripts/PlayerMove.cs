using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float maxJumpCooldown;
	float jumpCooldown;

	Rigidbody2D rb;
	Animator animator;

	bool grounded = false;

	void OnCollisionStay2D (Collision2D collision) {
		if (transform.position.y > collision.transform.position.y) {
			Health health = collision.transform.GetComponent<Health> ();
			if (health)
				health.TakeDamage (100);
		}
		
		if (!grounded && transform.position.x > collision.transform.position.x && Input.GetButtonDown ("Jump"))
			rb.velocity = new Vector3 (0, rb.velocity.y) + new Vector3 (5, 4);

		else if (!grounded && transform.position.x < collision.transform.position.x && Input.GetButtonDown ("Jump"))
			rb.velocity = new Vector3 (0, rb.velocity.y) + new Vector3 (-5, 4);
	}

	void OnTriggerStay2D () {
		grounded = true;
		animator.SetBool ("Grounded", true);
	}

	void OnTriggerExit2D () {
		grounded = false;
		animator.SetBool ("Grounded", false);
	}

	// Use this for initialization
	void Start () {

		rb = transform.GetComponent<Rigidbody2D> ();

		animator = transform.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		jumpCooldown -= Time.deltaTime;
		
		if (rb.velocity.y > 1)
			animator.SetBool("Jump", true);

		else
			animator.SetBool ("Jump", false);


		animator.SetFloat("VelocityX", rb.velocity.x);
		animator.SetFloat("VelocityY", rb.velocity.y);
		animator.speed = (rb.velocity.x + rb.velocity.y > 1 || rb.velocity.x + rb.velocity.y < -1) ? Mathf.Abs (rb.velocity.x + rb.velocity.y) : 1;

		float xInput = Input.GetAxis ("Horizontal");
		float zInput = Input.GetAxis ("Vertical");

		rb.velocity = grounded ? new Vector2 (0, rb.velocity.y) + new Vector2 (xInput * 3, jumpCooldown <= 0 && grounded && Input.GetButton ("Jump") ? 7 : 0) : rb.velocity;

		if (jumpCooldown <= 0 && Input.GetButton ("Jump"))
			jumpCooldown = maxJumpCooldown;


		if (rb.velocity.x > 0.1f)
			transform.localScale = new Vector3(1, 1, 1);
		else if (rb.velocity.x < -0.1f)
			transform.localScale = new Vector3(-1, 1, 1);
	}
}

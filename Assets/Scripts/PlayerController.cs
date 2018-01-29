using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 10f;

	private Rigidbody2D rb;
	private Animator animator;
	private Vector2 speed;
	private bool facingRight;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		speed = new Vector2(
			Input.GetAxisRaw("Horizontal") * maxSpeed,
			Input.GetAxisRaw("Vertical") * maxSpeed
		);

		bool isWalking = speed != Vector2.zero;
		animator.SetBool("walking", isWalking);

		if ((facingRight && speed.x < 0) || (!facingRight && speed.x > 0))
			flip();
	}

	void FixedUpdate() {
		if (speed != Vector2.zero)
			rb.AddForce(speed);
	}


	private void flip() {
		facingRight = !facingRight;
		var scale = transform.localScale;
		scale.x = -scale.x;
		transform.localScale = scale;
	}

}

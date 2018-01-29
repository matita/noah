using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalController : MonoBehaviour {

	public float walkSpeed = 10f;
	public float maxSpeed = 40f;
	private Transform player;
	public float distanceFromPlayer = 4f;

	private List<Transform> runFrom;
	private Rigidbody2D rb;
	private Vector2 walkDirection;
	private Vector2 speed;
	private bool facingRight = true;

	// Use this for initialization
	void Start () {
		var playerObj = GameObject.FindGameObjectWithTag("Player");
		if (playerObj != null)
			player = playerObj.GetComponent<Transform>();
		runFrom = new List<Transform>();
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine("walkWhenYouWant");
	}

	// Update is called once per frame
	void Update () {
		searchForRun();
		removeFromRun();
		runAway();

		if ((speed.x > 0 && !facingRight) || (speed.x < 0 && facingRight) || (walkDirection.x > 0 && !facingRight) || (walkDirection.x < 0 && facingRight))
			flip();
	}

	void FixedUpdate() {
		var finalSpeed = speed != Vector2.zero ? speed : walkDirection;
		rb.AddForce(finalSpeed);
	}

	private IEnumerator walkWhenYouWant() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(2f, 4f));
			if (runFrom.Count != 0)
				continue;

			var direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			walkDirection = direction * walkSpeed;
			yield return new WaitForSeconds(Random.Range(1f, 2f));

			walkDirection = Vector2.zero;
		}
	}


	private void runAway() {
		Vector2 pos = transform.position;
		Vector2 direction = Vector3.zero;
		foreach (var target in runFrom)
			direction += pos - new Vector2(target.position.x, target.position.y);

		if (runFrom.Count != 0) {
			direction = direction / (float)runFrom.Count;
			walkDirection = Vector2.zero;
		}

		speed = direction.normalized * maxSpeed;
	}

	private void flip() {
		facingRight = !facingRight;
		var scale = transform.localScale;
		scale.x = -scale.x;
		transform.localScale = scale;
	}

	private void searchForRun() {
		var pos = transform.position;
		if (Vector2.Distance(pos, player.position) <= distanceFromPlayer && !runFrom.Contains(player))
			runFrom.Add(player);
	}

	private void removeFromRun() {
		var pos = transform.position;
		var toRemove = runFrom.Where(t => Vector2.Distance(pos, t.position) > distanceFromPlayer).ToArray();
		
		foreach (var target in toRemove)
			runFrom.Remove(target);
	}
}

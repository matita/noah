using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownMask : MonoBehaviour {

	public Transform sea;

	private new Transform transform;
	private Transform parent;
	private float minDistance = 2f;

	void Start() {
		transform = GetComponent<Transform>();
		parent = GetComponentInParent<Transform>();
	}

	void Update () {
		if (!sea)
			return;
		var distance = parent.position.x - sea.position.x;
		if (distance < minDistance)
			transform.localScale = new Vector3(transform.localScale.x, 0f, transform.localScale.z);
		else
			transform.localScale = new Vector3(transform.localScale.x, (distance - minDistance) * 0.2f, transform.localScale.z);
	}
}

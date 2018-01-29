using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;

	// Update is called once per frame
	void Update () {
		var targetPos = new Vector3(target.position.x, target.position.y, target.position.z - 20f);
		var pos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
		transform.position = pos;
	}
}

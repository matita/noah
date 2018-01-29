using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour {

	void Start() {
		setPos();
	}

	// Update is called once per frame
	void LateUpdate () {
		setPos();
	}

	void setPos() {
		var pos = transform.position;
		//pos = new Vector3(Mathf.Round(pos.x * 8) / 8, Mathf.Round(pos.y * 8) / 8, Mathf.Round(pos.y * 8) / 8);
		pos = new Vector3(pos.x, pos.y, pos.y /10f);
		transform.position = pos;
	}

}

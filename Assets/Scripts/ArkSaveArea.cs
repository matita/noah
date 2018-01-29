using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkSaveArea : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "animal")
			GameManager.instance.rescue(collider.gameObject);
		else if (collider.gameObject.tag == "Player")
			GameManager.instance.playerIsSafe = true;
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player")
			GameManager.instance.playerIsSafe = false;
	}
}

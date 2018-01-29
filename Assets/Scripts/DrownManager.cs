using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownManager : MonoBehaviour {

	public Transform maskPrefab;
	private List<GameObject> inSea = new List<GameObject>();
	private List<GameObject> toDestroy = new List<GameObject>();
	private Transform sea;

	void Start() {
		sea = GetComponent<Transform>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		var collided = collider.gameObject;
		//Debug.Log("entered in sea " + collided.name);
		var renderer = collided.GetComponentInChildren<SpriteRenderer>();

		if (!renderer)
			return;

		var mask = collided.GetComponentInChildren<DrownMask>();
		if (!mask) {
			renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
			var child = (Transform)Instantiate(maskPrefab, collided.transform.position, Quaternion.identity, collided.transform);
			mask = child.GetComponent<DrownMask>();
		}

		if (!mask.sea)
			mask.sea = sea;

		inSea.Add(collided);
	}

	void OnTriggerExit2D(Collider2D collider) {
		var collided = collider.gameObject;
		if (inSea.Contains(collided))
			inSea.Remove(collided);
	}

	void Update() {
		toDestroy.Clear();

		var seaPos = sea.position;
		foreach (var collided in inSea) {
			var renderer = collided.GetComponentInChildren<SpriteRenderer>();
			var spriteSize = renderer.sprite.rect;
			var spriteHeight = spriteSize.height * collided.transform.localScale.y;
			Debug.Log("spriteHeight: " + spriteHeight);

			var mask = collided.GetComponentInChildren<DrownMask>().GetComponent<Transform>();
			var clipMask = mask.GetComponent<SpriteMask>();
			var maskHeight = clipMask.sprite.rect.height * mask.localScale.y;

			if (maskHeight >= spriteHeight)
				toDestroy.Add(collided);
		}

		foreach (var collided in toDestroy) {
			inSea.Remove(collided);
			Debug.Log("drowned: " + collided.name);
			collided.SetActive(false);
			if (collided.tag == "Player")
				GameManager.instance.finishGame(true);
		}
	}
}

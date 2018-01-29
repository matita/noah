using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject prefab;
	public int minCount;
	public int maxCount;
	public float minDistance;
	public float maxDistance;

	private List<GameObject> spawned = new List<GameObject>();

	public void Spawn() {
		foreach (var obj in spawned)
			Destroy(obj);
		spawned.Clear();

		var num = Random.Range(minCount, maxCount);
		for (int i = 0; i < num; i++) {
			var angle = Random.Range(0f, 2 * Mathf.PI);
			var dist = Random.Range(minDistance, maxDistance);
			var pos = new Vector3(Mathf.Cos(angle) * dist, Mathf.Sin(angle) * dist, 0f);

			var gameObj = Instantiate(prefab, pos, Quaternion.identity);
			spawned.Add(gameObj);
		}
	}
}

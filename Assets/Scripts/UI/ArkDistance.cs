using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArkDistance : MonoBehaviour {

	public Transform player;
	public Transform ark;
	public TextMeshProUGUI distanceText;
	public float paddingX = 50f;
	public float paddingY = 50f;

	private RectTransform textTransform;
	private Camera cam;

	void Start() {
		cam = GetComponent<Camera>();
		textTransform = distanceText.GetComponent<RectTransform>();
	}


	void Update () {
		var distance = (ark.position - player.position).magnitude;
		distanceText.text = Mathf.Round(distance) + " m";

		var screenPos = cam.WorldToScreenPoint(ark.position);
		if (cam.pixelRect.Contains(screenPos)) {
			
			if (distanceText.gameObject.activeInHierarchy)
				distanceText.gameObject.SetActive(false);

		} else {
			
			if (!distanceText.gameObject.activeInHierarchy)
				distanceText.gameObject.SetActive(true);

			textTransform.position = new Vector2(
				Mathf.Clamp(screenPos.x, paddingX, cam.pixelWidth - paddingX * 2),
				Mathf.Clamp(screenPos.y, paddingY, cam.pixelHeight - paddingY * 2)
			);
			
		}
		
	}
}

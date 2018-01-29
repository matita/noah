using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float secondsOfGame = 100f;
	public static GameManager instance;
	public GameObject instructionsText;
	public TextMeshProUGUI scoreText;
	public Transform ark;
	public Transform sea;
	public float maxSeaArkDistance;

	public GameObject gameplayHud;
	public GameObject losePanel;
	public TextMeshProUGUI loseCauseText;
	public GameObject winPanel;
	public TextMeshProUGUI winText;

	public GameObject player;
	[HideInInspector]
	public bool playerIsSafe;


	private int rescuedAnimals = 0;
	private Transform cam;
	private Spawner[] spawners;
	private bool gameIsFinished;

	void Awake() {
		if (instance != null && instance != this)
			Destroy(instance);
		instance = this;
	}

	void Start() {
		cam = Camera.main.GetComponent<Transform>();
		spawners = GetComponents<Spawner>();
		instructionsText.SetActive(false);

		StartGame();
	}

	public void StartGame() {
		rescuedAnimals = 0;
		sea.position = new Vector3(ark.position.x + secondsOfGame, cam.position.y, 100f);
		playerIsSafe = false;
		gameIsFinished = false;

		foreach (var spawner in spawners)
			spawner.Spawn();
		
		player.transform.position = ark.position - new Vector3(10f, ark.position.y, ark.position.z);
		player.SetActive(true);

		gameplayHud.SetActive(true);
		losePanel.SetActive(false);
		winPanel.SetActive(false);

		StopCoroutine("hideInstructions");
		instructionsText.SetActive(true);
		StartCoroutine("hideInstructions");

		updateScoreText();
	}

	IEnumerator hideInstructions() {
		yield return new WaitForSeconds(4f);
		instructionsText.SetActive(false);
	}

	void Update() {
		sea.position = new Vector3(
			sea.position.x - Time.deltaTime,
			cam.position.y,
			100f
		);

		if ((ark.position - sea.position).x >= maxSeaArkDistance)
			finishGame();
	}

	public void rescue(GameObject animal) {
		Destroy(animal);
		rescuedAnimals++;
		updateScoreText();
	}

	private void updateScoreText() {
		scoreText.text = rescuedAnimals + " animals";
	}

	public void finishGame(bool playerIsDrowned = false) {
		if (gameIsFinished)
			return;

		gameIsFinished = true;
		gameplayHud.SetActive(false);

		if (playerIsDrowned) {
			losePanel.SetActive(true);
			loseCauseText.text = "You've drowned";
		} else if (playerIsSafe) {
			if (rescuedAnimals <= 0) {
				losePanel.SetActive(true);
				loseCauseText.text = "No animal rescued?\nSelfish coward!";
			} else {
				winPanel.SetActive(true);
				winText.text = "You rescued " + rescuedAnimals + " animals";
			}
		} else {
			losePanel.SetActive(true);
			loseCauseText.text = "You were not in the ark";
		}
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*
 * This script manages our game.  It will handle score keeping, displaying score,
 * game state, powerups, game over conditions, counting player lives
 * 
 */
public class GameManager : MonoBehaviour {
	private bool gameOver = false;
	public GameObject ball;
	private static GameManager _instance;

	// make a GameMananager singleton
	public static GameManager Instance {
		get {
			if (_instance == null) {
				GameObject manager = new GameObject ("[GameManager]");
				_instance = manager.AddComponent<GameManager> ();
				DontDestroyOnLoad (manager);

			}

			return _instance;
		}
	}

	void Update () {
		if (ball) {
			if (ball.GetComponent<BallController>().getState() == "cpu_score") {
				this.gameOver = true;

			}

			if (gameOver) {
				if (Input.anyKeyDown) {
					// really we'd be keeping track of the current level too
					SceneManager.LoadScene ("Level1");
					gameOver = false;

				}
			}
		}
	}

	// when called, draws gui elts
	void OnGUI () {
		if (gameOver) {
			GUILayout.Label ("Game Over, luser....press a key to try again");

		}
	}
}

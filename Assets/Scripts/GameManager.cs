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
    private bool levelOver = false;
	private static GameManager _instance;
    private static int playerScore = 0;
    private static int enemyScore = 0;
    private int currentLevel = 1;
    private int numberOfLevels = 1;

	public GameObject ball;
    [HideInInspector]
    public int pointsPerRound = 1;

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

    public static void resetPlayerScore () {
        playerScore = 0;
    }

    public static void resetEnemyScore () {
        enemyScore = 0;
    }

    public static void resetScores () {
        resetPlayerScore ();
        resetEnemyScore ();

    }

    private void incrementAndDisplayeEnemyScore () {
        ++enemyScore;
        // this.updateScore('enemy');
        // or
        // ScoreUpdater.updateScore('enemy');
        // ScoreBoard.updateScore('enemy');

    }

    private void incrementAndDisplayePlayerScore () {
        ++playerScore;
        // this.updateScore('player');

    }

    private bool playerWinsRound () {
        if (playerScore >= pointsPerRound) {
            return true;

        } else {
            return false;

        }
    }

    private bool playerLosesRound () {
        if (enemyScore >= pointsPerRound) {
            return true;

        } else {
            return false;

        }
    }

	void Update () {
		if (ball) {
            BallController ballController = ball.GetComponent<BallController>();

            //if (ball.GetComponent<BallController>().compareState("cpu_score")) {
            if (ballController.compareState("cpu_score")) {
                ballController.clearState();
                this.incrementAndDisplayeEnemyScore();
                if (playerLosesRound()) {
                    this.gameOver = true;

                }
            } else if (ballController.compareState("player_score")) {
                ballController.clearState();
                this.incrementAndDisplayePlayerScore();
                if (playerWinsRound()) {
                    this.levelOver = true;

                }
            }

			if (gameOver) {
				if (Input.anyKeyDown) {
					// really we'd be keeping track of the current level too
					SceneManager.LoadScene ("Level1");
					gameOver = false;

				}
			} else if (levelOver) {
                ++this.currentLevel;
                if (this.currentLevel > this.numberOfLevels) {
                    Debug.Log("victory is mine");
                    SceneManager.LoadScene ("VictoryScreen");

                } else {
                    this.loadNextLevel();

                }
            }
		}
	}

    private void loadNextLevel () {
        Debug.Log("i r load next level");
    }

	// when called, draws gui elts
	void OnGUI () {
		if (gameOver) {
			GUILayout.Label ("Game Over, luser....press a key to try again");

		}
	}
}

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
    private int heroScore = 0;
    private int enemyScore = 0;
    private ScoreBoard scoreBoard;
    private int currentLevel = 1;
    private int numberOfLevels = 1;
    private bool gamePaused = false;
    private bool enemyScored = false;
    private bool playerScored = false;
    private int pointsPerRound = 5;

    public GameObject ball;

    public void resetPlayerScore () {
        this.heroScore = 0;
    }

    public void resetEnemyScore () {
        this.enemyScore = 0;
    }

    public void resetScores () {
        this.resetPlayerScore ();
        this.resetEnemyScore ();

    }

    private void incrementAndDisplayeEnemyScore () {
        // the score should probably the deferred to the ScoreBoard
        ++this.enemyScore;
        this.scoreBoard.updateEnemeyScore(this.enemyScore);

    }

    private void incrementAndDisplayePlayerScore () {
        ++this.heroScore;
        this.scoreBoard.updateHeroScore(this.heroScore);

    }

    private bool playerWinsRound () {
        if (heroScore >= pointsPerRound) {
            return true;

        } else {
            return false;

        }
    }

    private bool playerLosesRound () {
        if (enemyScore >= pointsPerRound) {
            Debug.Log("player loses round");
            Debug.Log("enemyScore: " + this.enemyScore.ToString());
            Debug.Log("pointsPerRound: " + this.pointsPerRound.ToString());
            return true;

        } else {
            Debug.Log("player does not lose  round");
            return false;

        }
    }

    void Start () {
        this.scoreBoard = new ScoreBoard();
        this.scoreBoard.initScoreBoard();

    }

    void Update () {
        if (Input.GetKeyDown("escape")) {
            if (this.gamePaused) {
                this.gamePaused = false;
                Time.timeScale = 1.0f;

            } else {
                this.gamePaused = true;
                Time.timeScale = 0.0f;

            }
        }

        if (ball) {
            BallController ballController = ball.GetComponent<BallController>();

            if (ballController.compareState("cpu_score")) {
                ballController.clearState();
                this.incrementAndDisplayeEnemyScore();

                if (playerLosesRound()) {
                    this.gameOver = true;

                } else {
                    // it seems misleading to not set this either way
                    this.enemyScored = true;

                }
            } else if (ballController.compareState("player_score")) {
                ballController.clearState();
                this.incrementAndDisplayePlayerScore();
                if (playerWinsRound()) {
                    this.levelOver = true;

                } else {
                    this.playerScored = true;

                }
            }

            if (this.gameOver) {
                if (Input.anyKeyDown) {
                    // really we'd be keeping track of the current level too
                    SceneManager.LoadScene ("Level1");
                    this.gameOver = false;

                }
            } else if (this.enemyScored) {
                if (Input.anyKeyDown) {
                    ballController.serve();
                    this.enemyScored = false;

                }
            } else if (this.playerScored) {
                if (Input.anyKeyDown) {
                    ballController.serve();
                    this.playerScored = false;

                }
            } else if (this.levelOver) {
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
        if (this.gameOver) {
            GUILayout.Label ("Game Over, luser....press a key to try again");

        } else if (this.enemyScored) {
            GUILayout.Label ("Villian Scored, luser....press a key to serve the ball");

        } else if (this.playerScored) {
            GUILayout.Label ("You Scored....press a key to serve the ball");

        }
    }
}

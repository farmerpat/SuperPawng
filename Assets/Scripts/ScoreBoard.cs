using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public class ScoreBoard : MonoBehaviour {
public class ScoreBoard {
    private ScoreBoard _instance;
    private Dictionary<int, string> scoreMap = new Dictionary<int, string>() {
        { 0, "Score Zero" },
        { 1, "Score One"},
        { 2, "Score Two"}

    };

    public void initScoreBoard () {
        if (scoreMap.ContainsKey (0)) {
            // make these tag string private class memeber
            this.displayScore(0, "HeroScore");
            this.displayScore(0, "EnemyScore");

        }
    }

    public void updateHeroScore (int newScore) {
        if (this.scoreMap.ContainsKey (newScore)) {
            this.displayScore(newScore, "HeroScore");

        }
    }

    public void updateEnemeyScore (int newScore) {
        if (this.scoreMap.ContainsKey (newScore)) {
            this.displayScore(newScore, "EnemyScore");

        } else {
            Debug.Log("scoreMap does not contain " + newScore.ToString());

        }
    }

    private void displayScore (int score, string targetTag) {
        string resourceName;
        GameObject[] gos;
        GameObject go;
        SpriteRenderer sr;

        if (scoreMap.ContainsKey(score)) {
            resourceName = this.scoreMap[score];
            gos = GameObject.FindGameObjectsWithTag(targetTag);
            // we will dangerously assume that this tag is unique
            // ...seriously, there has to be a better way
            go = gos[0];
            sr = (SpriteRenderer)go.GetComponent("SpriteRenderer");
            sr.sprite = Resources.Load<Sprite>(resourceName);

        }
    }
}

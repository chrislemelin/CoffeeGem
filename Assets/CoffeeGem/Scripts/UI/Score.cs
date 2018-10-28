using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    private int score = 0;
    private int displayScore = 0;
    
    private int scoreSpeed = 10;

    [SerializeField]
    TextMesh scoreTextMesh;

    private float lastUpdate;


	// Use this for initialization
	void Start () {
        lastUpdate = Time.time;
        StartCoroutine(ScoreUpdater());
    }
	
    public void addScore(int score) {
        this.score += score;
    }

    private IEnumerator ScoreUpdater() {
        while (true) {
            if (displayScore < score) {
                displayScore ++;
                if (displayScore > score) {
                    displayScore = score;
                }

                scoreTextMesh.text = displayScore.ToString(); //Write it to the UI
            }
            yield return new WaitForSeconds(0.02f); 
        }
    }
}

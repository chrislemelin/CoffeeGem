using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScore : MonoBehaviour {

    [SerializeField]
    List<GameObject> gemPips = new List<GameObject>();

    private int score = 0;

    public void Start() {
        ClearScore();
    }

    public void AddScore(int score) {
        this.score += score;
        SetScore(this.score);
    }

    public void SendScoreToCustomer() {
        FindObjectOfType<CustomerManager>().ScoreNextCustomer(score);
        ClearScore();
        score = 0;
    }

    public void SetScore(int score) {
        for(int a = 0; a < gemPips.Count && a < score; a++) {
            gemPips[a].GetComponent<Fade>().setShow(true);
        }
    }
	
    public void ClearScore() {
        foreach (GameObject pip in gemPips) {
            pip.GetComponent<Fade>().setShow(false);
        }
    }
	
}

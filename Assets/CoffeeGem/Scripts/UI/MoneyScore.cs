using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScore : MonoBehaviour {

    [SerializeField]
    AudioClip moneySound;

    [SerializeField]
    GameObject spawnScoreFade;

    [SerializeField]
    TextMesh scoreTextMesh;

    [SerializeField]
    GameObject scorePF;

    [SerializeField]
    private float pauseTime = .02f;
    private int score = 0;
    private int displayScore = 0;
    private int scoreSpeed = 10;



    private float lastUpdate;


    // Use this for initialization
    void Start() {
        lastUpdate = Time.time;
        StartCoroutine(ScoreUpdater());
    }

    public void addScore(int score) {
        this.score += score;

        if (score > 0) {
            GameObject scoreFade = Instantiate(scorePF);
            scoreFade.transform.position = spawnScoreFade.transform.position;
            scoreFade.GetComponent<ScoreFade>().SetTextAnchor(TextAnchor.MiddleLeft);
            scoreFade.GetComponent<ScoreFade>().setText("+$" + (score / 100.0f).ToString("0.00"));

            FindObjectOfType<SoundEffectPlayer>().PlaySoundEffect(moneySound);
        }
    }

    private IEnumerator ScoreUpdater() {
        while (true) {
            if (displayScore < score) {
                displayScore++;
                if (displayScore > score) {
                    displayScore = score;
                }
                scoreTextMesh.text = "$" + (score / 100.0f).ToString("0.00"); //Write it to the UI
                float newValue = (score - (float)displayScore) / 10.0f;
                scoreTextMesh.color = new Color(1.0f - Mathf.Min(1.0f, newValue), 1.0f, 1.0f - Mathf.Min(1.0f, newValue));
            }
            yield return new WaitForSeconds(pauseTime);
        }
    }
}

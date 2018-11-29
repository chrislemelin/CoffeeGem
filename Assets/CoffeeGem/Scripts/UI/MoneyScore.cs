using System;
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
    private float pauseTime = .01f;
    private int score = 0;
    private int displayScore = 0;

    [SerializeField]
    private int scoreSpeed = 10;

    public event Action<int> moneyChangedEvent;

    private float lastUpdate;

    // Use this for initialization
    void Start() {
        lastUpdate = Time.time;
        StartCoroutine(ScoreUpdater());
    }

    public int getScore() {
        return score;
    }

    public void removeMoney(int money) {
        score -= money;
        moneyChangedEvent?.Invoke(score);

        displayScore = score;
        scoreTextMesh.text = "$" + (displayScore / 100.0f).ToString("0.00");
    }

    public void updateMoneyBalance() {
        moneyChangedEvent?.Invoke(score);
    }

    public void addScore(int score) {
        this.score += score;
        moneyChangedEvent?.Invoke(this.score);

        if (score > 0) {
            GameObject scoreFade = Instantiate(scorePF);
            scoreFade.transform.position = spawnScoreFade.transform.position;
            scoreFade.GetComponent<ScoreFade>().SetTextAnchor(TextAnchor.MiddleLeft);
            scoreFade.GetComponent<ScoreFade>().setText("+$" + (score / 100.0f).ToString("0.00"));

            FindObjectOfType<SoundEffectPlayer>().PlaySoundEffect(moneySound);

            pauseTime = (1.0f / score) * scoreSpeed;
        }
    }

    private IEnumerator ScoreUpdater() {
        while (true) {
            if (displayScore < score) {
                displayScore++;
                if (displayScore > score) {
                    displayScore = score;
                }
                scoreTextMesh.text = "$" + (displayScore / 100.0f).ToString("0.00"); //Write it to the UI
                float newGreenValue = (score - (float)displayScore) / 10.0f;
                scoreTextMesh.color = new Color(1.0f - Mathf.Min(1.0f, newGreenValue), 1.0f, 1.0f - Mathf.Min(1.0f, newGreenValue));
            }
            yield return new WaitForSeconds(pauseTime);
        }
    }
}

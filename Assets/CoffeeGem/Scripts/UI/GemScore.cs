using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GemScore : MonoBehaviour {

    [SerializeField]
    GameObject coffeePF;

    [SerializeField]
    List<GameObject> gemPips = new List<GameObject>();

    [SerializeField]
    GameObject scorePipPF;

    [SerializeField]
    AudioClip coffeeFillSound;

    private Board board;
    private CustomerManager customerManager;
    private SoundEffectPlayer soundEffectPlayer;

    private float coffeeDuration = 1f;

    private List<GameObject> pipScores = new List<GameObject>();

    private int maxScore = 3;
    private int score = 0;

    public void Start() {
        ClearScore();
        customerManager = FindObjectOfType<CustomerManager>();
        soundEffectPlayer = FindObjectOfType<SoundEffectPlayer>();
        board = FindObjectOfType<Board>();
    }


    public void AddMatches(List<Match> matches, Board board) {
        int currentScore = matches.Aggregate(0, (acc, match) => acc + match.getScoreValue());
    
        int tempscore = score;
        foreach (Match match in matches) {
            for (int a = 0; a < Mathf.Min(match.getScoreValue(), maxScore - tempscore); a++) {
                GameObject pipScore = Instantiate(scorePipPF);
                pipScore.GetComponent<ScorePip>().setGem(match.type, board.get(match.position).transform.position);
                pipScore.GetComponent<ILerpable>().lerpTo(gemPips[a + tempscore].transform.position, 5);
                pipScores.Add(pipScore);
            }
            tempscore += match.getScoreValue();
        }
        AddScore(currentScore);
    }


    public void AddScore(int score) {
        this.score += score;
        SetScore(this.score);
    }

    public void SendScoreToCustomer() {
        GameObject coffee = Instantiate(coffeePF);
        coffee.transform.position = transform.position + new Vector3(.5f,0,0);
        coffee.GetComponent<Fade>().init();
        coffee.GetComponent<Fade>().setShow(true);
        coffee.GetComponent<ILerpable>().lerpTo(coffee.transform.position + new Vector3(0f, 1.25f, 0), 3.0f);
        ClearScore();
        soundEffectPlayer.PlaySoundEffect(coffeeFillSound);

        if (customerManager.customersServed +1 < customerManager.customersPerDay) {
            Core.core.ExecuteAfterTime(1f, () => {
                board.unlockBoard();
            });
        }
       
        Core.core.ExecuteAfterTime(coffeeDuration, () => {
            coffee.GetComponent<FadeOnDestroy>().Destroy();
            SendScoreToCustomerDelayed();
        });
        
    }

    private void SendScoreToCustomerDelayed() {
        customerManager.scoreNextCustomer(score);
        score = 0;
    }

    public void SetScore(int score) {
        for(int a = 0; a < gemPips.Count && a < score; a++) {
            gemPips[a].GetComponent<Fade>().setShow(true);
        }
    }

    public void ClearScore() {
        foreach (GameObject pip in pipScores) {
            pip.GetComponent<FadeOnDestroy>().Destroy(1);
        }
        pipScores.Clear();
    }
	
}

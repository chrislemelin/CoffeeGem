using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFade : MonoBehaviour {

    private float speed = 1.5f;

	// Use this for initialization
	void Start () {
        Invoke("DestroyMe", 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(0, 1, 0) * speed * Time.deltaTime; 
	}

    public void setScore(int score) {
        GetComponentInChildren<TextMesh>().text = score + "";
    }
    public void setText(string text) {
        GetComponentInChildren<TextMesh>().text = text;
    }

    public void SetTextAnchor(TextAnchor textAnchor) {
        GetComponentInChildren<TextMesh>().anchor = textAnchor;
    }



    private void DestroyMe() {
        GetComponent<FadeOnDestroy>().Destroy();
    }
}

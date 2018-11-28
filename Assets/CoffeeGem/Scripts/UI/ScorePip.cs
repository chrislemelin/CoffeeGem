using UnityEngine;

public class ScorePip : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGem(IBoardEntity boardEntity) {
        transform.position = boardEntity.transform.position;
        Core.CopySpriteRender(boardEntity.GetComponentInChildren<SpriteRenderer>(), gameObject);
    }

}

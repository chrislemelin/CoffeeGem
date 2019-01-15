using UnityEngine;

public class ScorePip : MonoBehaviour {

    public void setGem(GemType type, Vector3 position) {
        transform.position = position;
        Core.CopySpriteRender(LibraryManager.instance.get<BELibrary>().get(type).GetComponentInChildren<SpriteRenderer>(), gameObject);
    }

}

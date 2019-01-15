using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    private static bool created = false;

    public void Awake() {
        if (!created) {
            created = true;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

}

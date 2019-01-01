using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour {
    public static bool created = false;

	void Awake () {
        if (created) {
            Destroy(gameObject);
        }
        else {
            created = true;
            DontDestroyOnLoad(gameObject);
        }
    }
}

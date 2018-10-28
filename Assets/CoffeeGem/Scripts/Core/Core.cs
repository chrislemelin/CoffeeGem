using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {

    static public Core core; 

    void Awake() {
        core = this; 
    }

    public void ExecuteAfterTime(float time, Action action) {
        StartCoroutine(ExecuteAfterTimeHelper(time, action));
    }
    public static IEnumerator ExecuteAfterTimeHelper(float time, Action action) {
        yield return new WaitForSeconds(time);
        action();
    }
}

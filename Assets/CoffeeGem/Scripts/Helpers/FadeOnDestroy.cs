using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnDestroy : Fade {

    private float fadeTime = 1;

    public void Destroy(float fadeTime, float delay = 0, Action action = null) {
        setDuration(fadeTime);
        setShow(false);

        StartCoroutine(DestroyHelper(fadeTime, action));
    }

    IEnumerator DestroyHelper(float fadeTime, Action action) {
        yield return new WaitForSeconds(fadeTime);
        action?.Invoke();
        Destroy(target);
    }

    public void Destroy() {
        Destroy(fadeTime);
    }


}

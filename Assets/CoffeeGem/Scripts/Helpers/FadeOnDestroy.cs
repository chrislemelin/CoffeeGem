using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnDestroy : MonoBehaviour {

    private float fadeTime = 1;
    private float? startFade = null;
    [SerializeField]
    List<SpriteRenderer> renderers = new List <SpriteRenderer>();
    [SerializeField]
    List<TextMesh> textMeshes = new List<TextMesh>();

    Action returnAction;

    public void Destroy(float fadeTime, float delay, Action action = null) {
        returnAction = action;
        StartCoroutine(DestroyHelper(fadeTime, delay));
    }

    IEnumerator DestroyHelper(float fadeTime, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        Destroy(fadeTime);
    }

    public void Destroy(float fadeTime) {
        this.fadeTime = fadeTime;
        startFade = Time.time;
    }

    public void Destroy() {
        startFade = Time.time;
    }

    public void Update() {
        if(startFade != null) {
            float newAlpha = (Time.time - (float)startFade) / fadeTime;
            newAlpha = newAlpha - 1;
            float absAlpha = Mathf.Abs(newAlpha);

            foreach (SpriteRenderer render in renderers) {
                Color newColor = render.color;
                newColor.a = absAlpha;
                render.color = newColor;
            }

            foreach (TextMesh textMesh in textMeshes) {
                Color newColor = textMesh.color;
                newColor.a = absAlpha;
                textMesh.color = newColor;
            }

            if (newAlpha > 0) {
                returnAction?.Invoke();
                Destroy(gameObject);
            }
        }
    }



}

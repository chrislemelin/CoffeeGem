using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fade : MonoBehaviour {

    [SerializeField]
    private float targetAlpha = 1;
    bool active = false;
    bool started = false;

    [SerializeField]
    private float duration = 1;
    [SerializeField]
    protected GameObject target;
    List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    List<TextMesh> textMeshes = new List<TextMesh>();

    public void Start() {
        if (!started) {
            if (target == null) {
                target = gameObject;
            }

            spriteRenderers = target.GetComponentsInChildren<SpriteRenderer>().OfType<SpriteRenderer>().ToList();
            textMeshes = target.GetComponentsInChildren<TextMesh>().OfType<TextMesh>().ToList();

            setTransparcyHelper(targetAlpha);
            started = true;
        }
    }

    public void checkShow() {
        Start();
    }

    public void setTransparent(float value, bool instant = false) {
        targetAlpha = value;
        if (instant) {
            setTransparcyHelper(value);
        } else {
            active = true;
        }
    }

    private void setTransparcyHelper(float value) {
        foreach (SpriteRenderer render in spriteRenderers) {
            Color newColor = render.color;
            newColor.a = value;
            render.color = newColor;
        }
        foreach (TextMesh textMesh in textMeshes) {
            Color newColor = textMesh.color;
            newColor.a = value;
            textMesh.color = newColor;
        }
    }

    public void setShow(bool show, bool instant = false) {
        float value = show ? 1.0f : 0.0f;
        setTransparent(value, instant);
    }

    public void setDuration(float duration) {
        this.duration = duration;
    }

    public void Update() {
        if (active) {
            float currentAlpha = 0;
            if (spriteRenderers.Count > 0) {
                currentAlpha = spriteRenderers[0].color.a;
            } else if (textMeshes.Count > 0) {
                currentAlpha = textMeshes[0].color.a;
            }

            float alphaDelta = Time.deltaTime / duration;
            float distanceFromTarget = targetAlpha - currentAlpha;
            float ratio = Mathf.Abs(alphaDelta / distanceFromTarget);
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, ratio);    

            setTransparcyHelper(newAlpha);
            if (ratio > 1) {
                active = false;
            }
            
        }
    }
}

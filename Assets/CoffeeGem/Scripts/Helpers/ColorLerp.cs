using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour {

    [SerializeField]
    GameObject target;

    [SerializeField]
    private float duration = .1f;

    private List<SpriteRenderer> spriteRenderers;

    private float startTime;
    private bool isLerping = false;
    private Color lerpingColor;
    private Color startColor;

    // Use this for initialization
    void Awake() {
        if (target == null) {
            target = gameObject;
        }
        spriteRenderers = new List<SpriteRenderer>(target.GetComponentsInChildren<SpriteRenderer>());
    }

    public void lerpToColor(Color color) {
        isLerping = true;
        lerpingColor = color;
        startTime = Time.time;
        if (spriteRenderers.Count > 0) {
            startColor = spriteRenderers[0].color;
        }
    }

    public void cancelLerpToColor() {
        isLerping = false;
    }

    // Update is called once per frame
    void Update() {
        if (isLerping) {
            float ratio;
            if (duration <= 0) {
                ratio = 1;
            } else {
                ratio = (Time.time - startTime) / duration;
            }

            foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                spriteRenderer.color = Color.Lerp(startColor, lerpingColor, ratio);
            }

            if (ratio >= 1) {
                isLerping = false;
            }
        }
    }
}

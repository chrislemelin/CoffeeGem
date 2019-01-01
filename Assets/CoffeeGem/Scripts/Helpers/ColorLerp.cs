using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour {

    [SerializeField]
    GameObject target;

    [SerializeField]
    private float duration = .1f;

    private List<SpriteRenderer> spriteRenderers;
    private List<TextMeshProUGUI> textMeshes;
    private List<Image> images;

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
        textMeshes = new List<TextMeshProUGUI>(target.GetComponentsInChildren<TextMeshProUGUI>());
        images = new List<Image>(target.GetComponentsInChildren<Image>());

        if (spriteRenderers.Count > 0) {
            startColor = spriteRenderers[0].color;
        } else if (textMeshes.Count > 0) {
            startColor = textMeshes[0].color;
        } else if (images.Count > 0) {
            startColor = images[0].color;
        }
        lerpingColor = startColor;
    }

    public  Color getColor() {
        return lerpingColor;
    }

    public void lerpToAlpha(float alpha) {
        Color newColor = new Color(lerpingColor.r, lerpingColor.g, lerpingColor.b, alpha);
        lerpToColor(newColor);
    }

    public void lerpToColor(Color color) {
        isLerping = true;
        lerpingColor = color;
        startTime = Time.time;

        if (spriteRenderers.Count > 0) {
            startColor = spriteRenderers[0].color;
        } else if (textMeshes.Count > 0) {
            startColor = textMeshes[0].color;
        } else if (images.Count > 0) {
            startColor = images[0].color;
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
            foreach (TextMeshProUGUI textMesh in textMeshes) {
                textMesh.color = Color.Lerp(startColor, lerpingColor, ratio);
            }
            foreach (Image image in images) {
                image.color = Color.Lerp(startColor, lerpingColor, ratio);
            }

            if (ratio >= 1) {
                isLerping = false;
            }
        }
    }
}

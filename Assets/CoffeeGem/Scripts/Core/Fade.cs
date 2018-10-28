using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    private bool show = true;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    public void setShow(bool show) {
        this.show = show;
    }

    public void setShow(bool show, float speed) {
        this.speed = speed;
    }


    public void Update() {
        float timeDelta = Time.deltaTime * speed;
        float currentAlpha = spriteRenderer.color.a;

        if ((currentAlpha < 1.0 && show) || (currentAlpha > 0.0 && !show)) {
            float newAlpha = currentAlpha;
            if (show) {
                newAlpha += timeDelta;
            } else {
                newAlpha -= timeDelta;
            }
            newAlpha = Mathf.Clamp(newAlpha, 0.0f, 1.0f);
            Color newColor = spriteRenderer.color;
            newColor.a = newAlpha;
            spriteRenderer.color = newColor;
        }
        
    }
}

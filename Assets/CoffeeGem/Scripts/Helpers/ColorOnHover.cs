using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOnHover : MonoBehaviour {

    [SerializeField]
    private Color standardColor = Color.white;

    [SerializeField]
    private Color hoverColor;

    [SerializeField]
    private ColorLerp target;

    private OnEvent onEvent;

    private bool selected = false;

    void Start() {
        if (target == null) {
            target = gameObject.GetComponent<ColorLerp>();
        }
        onEvent = gameObject.GetComponent<OnEvent>();

        onEvent.enter += setHoverColor;
        onEvent.exit += stopHoverColor;
    }

    public void setSelected(bool selected) {
        this.selected = selected;
        if (selected) {
            setHoverColor();
        } else {
            stopHoverColor();
        }
    }
    
    private void setHoverColor() {
        target.lerpToColor(hoverColor);
    }

    private void stopHoverColor() {
        if (!selected) {
            target.lerpToColor(standardColor);
        }
    }
}

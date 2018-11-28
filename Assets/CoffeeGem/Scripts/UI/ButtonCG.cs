using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCG : ColorLerp {

    [SerializeField]
    private Color normalColor = new Color(1, 1, 1);
    [SerializeField]
    private Color onHoverColor = new Color(.85f, .85f, .85f);
    [SerializeField]
    private Color clickColor = new Color(.75f, .75f, .75f);
    [SerializeField]
    private Color disabledColor = new Color(.5f, .5f, .5f);

    [SerializeField]
    private bool enabled = true;

    private void Start() {
        setEnabled(enabled);
    }

    public void setEnabled(bool enabled) {
        this.enabled = enabled;
        if (!enabled) {
            lerpToColor(disabledColor);
        } else {
            lerpToColor(normalColor);
        }
    }

    public void OnMouseEnter() {
        if (enabled) {
            lerpToColor(onHoverColor);
        }
    }

    public void OnMouseExit() {
        if (enabled) {
            lerpToColor(normalColor);
        }
    }

    public void OnMouseDown() {
        if (enabled) {
            lerpToColor(clickColor);
        }
    }

    public void OnMouseUp() {
        if (enabled) {
            lerpToColor(onHoverColor);
        }
    }
}

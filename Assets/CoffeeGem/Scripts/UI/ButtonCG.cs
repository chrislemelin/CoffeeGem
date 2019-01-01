﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCG : ColorLerp, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

    public event Action clicked;

    [SerializeField]
    private Color normalColor = new Color(1, 1, 1);
    [SerializeField]
    private Color onHoverColor = new Color(.85f, .85f, .85f);
    [SerializeField]
    private Color clickColor = new Color(.75f, .75f, .75f);
    [SerializeField]
    private Color disabledColor = new Color(.5f, .5f, .5f);

    [SerializeField]
    private bool toggle = false;
    private bool toggled = false;

    [SerializeField]
    private bool buttonEnabled = true;

    private void Start() {
        setEnabled(buttonEnabled);
    }

    public void setEnabled(bool buttonEnabled) {
        this.buttonEnabled = buttonEnabled;
        if (!buttonEnabled) {
            lerpToColor(disabledColor);
        } else if (toggle) {
            if (toggled) {
                lerpToColor(clickColor);
            } else {
                lerpToColor(normalColor);
            }
        } else {
            lerpToColor(normalColor);
        }

    }

    public void OnMouseEnter() {
        if (buttonEnabled && showHover()) {
            lerpToColor(onHoverColor);
        }
    }

    public void OnMouseExit() {
        if (buttonEnabled && showHover()) {
            lerpToColor(normalColor);
        }
    }

    public void OnMouseDown() {
        if (buttonEnabled) {
            lerpToColor(clickColor);
        }
    }

    public void OnMouseUp() {
        if (buttonEnabled) {
            clicked?.Invoke();
            toggled = !toggled;
            if (toggledOn()) {

            } else {
                lerpToColor(onHoverColor);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        OnMouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData) {
        OnMouseExit();
    }

    public void OnPointerDown(PointerEventData eventData) {
        OnMouseDown();
    }

    public void OnPointerUp(PointerEventData eventData) {
        OnMouseUp();
    }

    private bool toggledOn() {
        return toggle && toggled;
    }

    private bool showHover() {
        if (toggle && toggled) {
            return false;
        } else {
            return true;
        }
    }

    public void toggleOff() {
        if (toggle && toggled) {
            toggled = false;
            lerpToColor(normalColor);
        }
    }
}

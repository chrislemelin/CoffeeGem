using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnEvent : MonoBehaviour, IPointerClickHandler, IPointerUpHandler {

    public event Action enter;
    public event Action exit;
    public event Action click;
    public event Action clickUp;

    public event Action onMouseClick;

    //debugging
    void OnMouseDown() {
        click?.Invoke();
    }

    void OnMouseUp() {
        clickUp?.Invoke();
    }

    //debugging
    void OnMouseEnter() {
        enter?.Invoke();
    }

    //debugging
    void OnMouseExit() {
        exit?.Invoke();
    }

    public void ManualMouseEnter() {
        enter?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData) {
        onMouseClick?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData) {
        OnMouseUp();
    }
}

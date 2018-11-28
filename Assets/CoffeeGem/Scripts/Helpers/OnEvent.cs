using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEvent : MonoBehaviour {

    public event Action enter;
    public event Action exit;
    public event Action click;

    //debugging
    void OnMouseDown() {
        click?.Invoke();
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
}

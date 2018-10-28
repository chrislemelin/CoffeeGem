using System;
using UnityEngine;

public class OnClick : MonoBehaviour {

    public event Action click;

    //debugging
    void OnMouseDown() {
        click?.Invoke();
    }
}

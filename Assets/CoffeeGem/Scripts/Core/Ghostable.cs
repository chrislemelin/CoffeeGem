using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostable : MonoBehaviour {

    [SerializeField]
    SpriteRenderer spriteRender;

    public void ghostOn() {
        Color temp = spriteRender.color;
        temp.a = .5f;
        spriteRender.color = temp;
    }

    public void ghostOff() {
        Color temp = spriteRender.color;
        temp.a = 1;
        spriteRender.color = temp;
    }
}


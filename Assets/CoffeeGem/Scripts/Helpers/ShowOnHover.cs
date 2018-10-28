using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnHover : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer spriteRender;

    //debugging
    public void show() {
        Color tempColor = spriteRender.color;
        tempColor.a = 1;
        spriteRender.color = tempColor;
    }

    //debugging
    public void hide() {
        Color tempColor = spriteRender.color;
        tempColor.a = 0;
        spriteRender.color = tempColor;
    }
}

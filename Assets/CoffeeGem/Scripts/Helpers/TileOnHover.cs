using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOnHover : OnEvent {

    private bool mouseEnter;

	// Use this for initialization
	void Start () {
        enter += () => {
            gameObject.GetComponent<Fade>().setShow(true, true);
        };

        enter += () => mouseEnter = true;
        exit += () => {
            gameObject.GetComponent<Fade>().setShow(false, true);
            gameObject.GetComponent<ColorLerp>().cancelLerpToColor();
        };
        exit += () => mouseEnter = false;
    }

    public void checkEntered () {
        if (mouseEnter) {
            ManualMouseEnter();
        }
    }



}

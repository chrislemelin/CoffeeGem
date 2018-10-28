using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOnHover : OnHover {

    private bool mouseEnter;

	// Use this for initialization
	void Start () {
        enter += gameObject.GetComponent<ShowOnHover>().show;
        enter += () => mouseEnter = true;
        exit += gameObject.GetComponent<ShowOnHover>().hide;
        exit += () => mouseEnter = false;
    }

    public void checkEntered () {
        if (mouseEnter) {
            ManualMouseEnter();
        }
    }



}

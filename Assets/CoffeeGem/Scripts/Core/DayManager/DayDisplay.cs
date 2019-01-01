using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayDisplay : MonoBehaviour {

    private Fade fade;
    private TextMeshProUGUI text;
    [SerializeField]
    GameObject background;

    private void Start() {
        fade = GetComponent<Fade>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        background.SetActive(true);
        fade.recheckDict();
    }


    public void displayDay(int number, Action callback = null, bool setShow = false) {
        text.text = "Day " + number;
        fade.setShow(true);
        if (setShow) {
            fade.setTransparent(1.0f, true);
        }
        Core.core.ExecuteAfterTime(2.0f, () => {
            fade.setShow(false);
            Core.core.ExecuteAfterTime(1.0f, () => callback?.Invoke());
        });

    }
}

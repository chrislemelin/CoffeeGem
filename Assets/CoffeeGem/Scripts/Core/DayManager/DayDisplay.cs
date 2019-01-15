using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void displayDay(DateTime time, Action callback = null, bool start = false) {
        background.SetActive(true);

        if (start) {
            text.text = time.ToString("dddd, d MMMM");
            fade.setShow(true);
            fade.setTransparent(1.0f, true);

            Core.core.ExecuteAfterTime(2.0f, () => {
                fade.setShow(false);
                Core.core.ExecuteAfterTime(1.0f, () => callback?.Invoke());
            });
        } else {
            text.text = "";
            fade.setShow(true);
            Core.core.ExecuteAfterTime(1.0f, () => { SceneManager.LoadScene(0); });
        }
    

    }
}

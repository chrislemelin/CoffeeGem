using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDayUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ButtonCG buttonCG = GetComponent<ButtonCG>();
        MenuLibrary.Instance.checkFull();
        MenuLibrary.Instance.onFull += () => buttonCG.setEnabled(true);
        MenuLibrary.Instance.onNotFull += () => buttonCG.setEnabled(false);
        buttonCG.clicked += startDay;
	}

    private void startDay() {
        SceneManager.LoadScene(1);
    }
}

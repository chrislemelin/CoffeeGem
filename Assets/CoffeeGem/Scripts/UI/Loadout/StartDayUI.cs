using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDayUI : MonoBehaviour {

    private ButtonCG buttonCG;

    private MenuLibrary menuLibrary;

    // Use this for initialization
    void Start () {
        buttonCG = GetComponent<ButtonCG>();
        menuLibrary = LibraryManager.instance.get<MenuLibrary>();
        menuLibrary.onFull += () => buttonCG.setEnabled(true);
        menuLibrary.onNotFull += () => buttonCG.setEnabled(false);
        menuLibrary.checkFull();
        buttonCG.clicked += startDay;
	}

    private void OnDestroy() {
        LibraryManager.instance.get<MenuLibrary>().onFull -= () => buttonCG.setEnabled(true);
        menuLibrary.onNotFull -= () => buttonCG.setEnabled(false);
    }

    private void startDay() {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI text;

    private MoneyLibrary moneyLibrary;

    private void Start() {
        moneyLibrary = LibraryManager.instance.get<MoneyLibrary>();
        moneyLibrary.moneyChanged += setMoneyText;
        setMoneyText(moneyLibrary.getMoney());
    }

    private void OnDestroy() {
        moneyLibrary.moneyChanged -= setMoneyText;
    }

    private void setMoneyText(int money) {
        text.text = "$" + (money/100.0f).ToString("0.00");
    }
}

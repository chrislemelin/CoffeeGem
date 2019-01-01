using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerDisplay : MonoBehaviour {

    private TextMeshProUGUI text;

    public void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        FindObjectOfType<CustomerManager>().customersServedChanged += customerDisplayUpdate;
    }

    public void customerDisplayUpdate (int customersServed, int customersPerDay) {
        text.text = "Customers " + customersServed + "/" + customersPerDay;
    }
}

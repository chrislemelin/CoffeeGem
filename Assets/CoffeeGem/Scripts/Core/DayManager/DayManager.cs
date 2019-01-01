using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour {

    public static DayManager dayManager;
    public event Action start;
    public event Action end;

    private int day = 1;

    [SerializeField]
    private DayDisplay dayDisplay;

    [SerializeField]
    private int customersPerDay;

    [SerializeField]
    private CustomerManager customerManager;

    private void Awake() {
        dayManager = this;
    }

    private void Start() {
        customerManager = FindObjectOfType<CustomerManager>();
        customerManager.setCustomersPerDay(customersPerDay);

        dayDisplay.displayDay(day, startDay, true);
    }

    public void endDay() {
        day++;
        end?.Invoke();
        dayDisplay.displayDay(day, () => SceneManager.LoadScene(0));
    }

    public void startDay() {
        start?.Invoke();
    }

}

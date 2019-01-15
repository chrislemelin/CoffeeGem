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
    private DayLibrary dayLibrary;

    private void Awake() {
        dayManager = this;
    }

    private void Start() {
        dayLibrary = LibraryManager.instance.get<DayLibrary>();

        customerManager = FindObjectOfType<CustomerManager>();
        customerManager.setCustomersPerDay(customersPerDay);

        dayDisplay.displayDay(dayLibrary.getDateTime(), startDay, true);
    }

    public void endDay() {
        day++;
        end?.Invoke();
        dayLibrary.incrementDay();
        dayDisplay.displayDay(DateTime.Now, start: false);
    }

    public void startDay() {
        start?.Invoke();
    }

}

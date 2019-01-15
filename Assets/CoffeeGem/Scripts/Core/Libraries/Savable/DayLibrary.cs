using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLibrary : ILibrary {

    public event Action<DateTime> dayChanged;
    private DateTime startTime;
    private int day;

    public void init() {
        startTime = new DateTime(2018, 1, 1);
        day = 0;
    }

    public int getDay() {
        return day;
    }

    public void incrementDay() {
        day++;
    }

    public DateTime getDateTime() {
        return startTime.AddDays(day);
    }

    public String getDateString() {
        return getDateTime().ToString("t");
    }
}

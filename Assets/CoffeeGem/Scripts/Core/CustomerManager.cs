using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour {

    public delegate void customersServedChangedDelegate(int customersServed, int customersPerDay);
    public event customersServedChangedDelegate customersServedChanged;

    [SerializeField]
    private GameObject sendCustomerTo;

    [SerializeField]
    GameObject lineStartPosition;

    [SerializeField]
    float lineSpacing;

    [SerializeField]
    float speed = .2f;

    [SerializeField]
    int maxLineLength;

    [SerializeField]
    List<Customer> customerLine = new List<Customer>();

    [SerializeField]
    GameObject customerPF;

    [SerializeField]
    DayManager dayManager;

    public int customersPerDay { get; protected set; } = 20;
    public int customersServed { get; protected set; } = 0;

	// Use this for initialization
	void Start () {
        dayManager = FindObjectOfType<DayManager>();
        
        dayManager.start += startLine;
        updateEvent();
	}

    public void updateEvent() {
        customersServedChanged?.Invoke(customersServed, customersPerDay);
    }

    public void startLine() {
        addCustomerToLine(Mathf.Min(maxLineLength, customersPerDay));
        customersServed = 0;
        updateEvent();
    }
	
    public void setCustomersPerDay(int customersPerDay) {
        this.customersPerDay = customersPerDay;
        updateEvent();
    }

    public void scoreNextCustomer(int score) {
        score = Mathf.Min(score, 3);
        Expression expression;
        int moneyGained = 0;
        if (score == 0) {
            expression = Expression.sad;
        } else {
            expression = Expression.happy;
            moneyGained = (int)Mathf.Pow(5, score);
        }
        customerLine[0].served(expression, moneyGained, sendCustomerTo.transform.localPosition, .25f);
        customersServed++;
        popCustomerOffLine();

        int customersLeft = customersPerDay - customersServed;
        if (customersLeft >= maxLineLength) {
            addCustomerToLine(1);
        }
        if (customersLeft == 0) {
            Core.core.ExecuteAfterTime(1.5f, dayManager.endDay);
        }
        updateEvent();
    }

    private void popCustomerOffLine() {
        for (int a = 1; a < customerLine.Count; a++) {
            customerLine[a].GetComponent<ILerpable>().lerpTo(new Vector3(-lineSpacing, 0, 0) * (a - 1), .20f);
        }
        customerLine.RemoveAt(0);
    }

    private void addCustomerToLine(int quantity) {
        //for (int a = quantity; a < customerLine.Count; a++) {
        //    customerLine[a].GetComponent<ILerpable>().lerpTo(new Vector3(-lineSpacing, 0, 0) * (a-1), .20f);
        //}
        int gap = maxLineLength - customerLine.Count ;
        for (int a = maxLineLength; a < maxLineLength + quantity; a++) {
            GameObject customer = Instantiate(customerPF);
            customer.transform.SetParent(lineStartPosition.transform, false);
            customer.transform.localPosition = new Vector3(-lineSpacing, 0, 0) * a;
            customer.GetComponent<Fade>().setShow(true);
            customer.GetComponent<ILerpable>().lerpTo(new Vector3(-lineSpacing, 0, 0) * (a - gap), Mathf.Clamp(gap * speed, speed, speed * 2));
            customerLine.Add(customer.GetComponent<Customer>());
        }
        //if (remove) {
        //    customerLine.RemoveRange(0, quantity);
        //}
    }
}

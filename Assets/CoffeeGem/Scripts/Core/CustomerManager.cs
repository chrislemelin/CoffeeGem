using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour {

    [SerializeField]
    GameObject lineStartPosition;

    [SerializeField]
    float lineSpacing;

    [SerializeField]
    int maxLineLength;

    [SerializeField]
    List<Customer> customerLine = new List<Customer>();

    [SerializeField]
    GameObject customerPF;

	// Use this for initialization
	void Start () {
        addCustomerToLine(maxLineLength, false);
        //addNewCustomer();
	}
	
	// Update is called once per frame
	void Update () {
		
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
        customerLine[0].served(expression, moneyGained, new Vector3(.5f, 0, 0), .25f);
        addCustomerToLine(1);
       
        //Core.core.ExecuteAfterTime(.5f, addNewCustomer);
    }

    private void addCustomerToLine(int quantity, bool remove = true) {
        for (int a = quantity; a < customerLine.Count; a++) {
            customerLine[a].GetComponent<ILerpable>().lerpTo(new Vector3(-lineSpacing, 0, 0) * (a-1), .20f);
        }
        for (int a = maxLineLength; a < maxLineLength + quantity; a++) {
            GameObject customer = Instantiate(customerPF);
            customer.transform.SetParent(lineStartPosition.transform, false);
            customer.transform.localPosition = new Vector3(-lineSpacing, 0, 0) * a;
            customer.GetComponent<Fade>().setShow(true);
            customer.GetComponent<ILerpable>().lerpTo(new Vector3(-lineSpacing, 0, 0) * (a-quantity), .20f);
            customerLine.Add(customer.GetComponent<Customer>());
        }
        if (remove) {
            customerLine.RemoveRange(0, quantity);
        }
    }
}

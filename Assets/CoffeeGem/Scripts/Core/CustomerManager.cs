using System.Collections;
using System.Collections.Generic;
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
        AddNewCustomer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ScoreNextCustomer(int score) {
        score = Mathf.Min(score, 3);
        if (score == 0) {
            customerLine[0].Served(Expression.happy, score * 10, new Vector3(.5f, 0, 0), 2);

        } else {
            customerLine[0].Served(Expression.sad, score * 10, new Vector3(.5f, 0, 0), 2);
        }
        customerLine.RemoveAt(0);
        AddNewCustomer();
    }

    private void AddNewCustomer() {
        GameObject customer = Instantiate(customerPF);
        customer.transform.SetParent(lineStartPosition.transform, false);
        customer.transform.localPosition = new Vector3(0, 0, 0);
        customerLine.Add(customer.AddComponent<Customer>());
    }
}

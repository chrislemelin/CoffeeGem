using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

	public void Served(Expression expression, int money, Vector3 position, float speed) {
        FindObjectOfType<MoneyScore>().addScore(money);
        GetComponent<ILerpable>().lerpToTimed(position, speed);
        GetComponent<FadeOnDestroy>().Destroy(1, Mathf.Max(0, speed - 1.0f));
    }
}

public enum Expression { normal, happy, sad }
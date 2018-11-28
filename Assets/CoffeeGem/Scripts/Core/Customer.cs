using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

    [SerializeField]
    float moneyWaitTime = 1.0f;

    [SerializeField]
    Sprite happySprite;

    [SerializeField]
    Sprite sadSprite;

	public void served(Expression expression, int money, Vector3 position, float speed) {
        Core.core.ExecuteAfterTime(moneyWaitTime, () => FindObjectOfType<MoneyScore>().addScore(money));

        GetComponent<ILerpable>().lerpTo(position, speed);
        GetComponent<FadeOnDestroy>().Destroy(1, Mathf.Max(0, 1.0f));

        if (expression == Expression.happy) {
            GetComponentInChildren<SpriteRenderer>().sprite = happySprite;
        } else if (expression == Expression.sad) {
            GetComponentInChildren<SpriteRenderer>().sprite = sadSprite;
        }
    }
}

public enum Expression { normal, happy, sad }
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyLibrary : ILibrary {

    //public static MoneyLibrary Instance;

    public event Action<int> moneyChanged;
    private int money = 5000;

    public void init() {
    
    }

    // Update is called once per frame
 //   void Awake () {
	//	if (Instance == null) {
 //           Instance = this;
 //       }
	//}

    public void removeMoney(int money) {
        this.money -= money;
        moneyChanged?.Invoke(this.money);
    }

    public void addMoney(int money) {
        this.money += money;
        moneyChanged?.Invoke(this.money);
    }

    public void triggerMoneyChanged() {
        moneyChanged?.Invoke(money);
    }

    public int getMoney() {
        return money;
    }

}

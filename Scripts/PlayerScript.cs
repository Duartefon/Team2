using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // public for debug reasons
    public int money = 0;

    public int getMoney() {
        return money;
    }
    public void addMoney(int value) {
        money += value;
    }

    public void payPrice(int value) {
        money -= value;
    }

}

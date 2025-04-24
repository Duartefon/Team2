using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
    // some pills will have levels of effect
    public Dictionary<string, int> pillLevels = new Dictionary<string, int>();


    private int money = 100;

    private void Start() {
        pillLevels.Add("Parkinson", 0);
        pillLevels.Add("Red Vision", 0);
    }

    // we try to get the level of the pill in question
    public int GetPillLevel(string pillName) {
        if (pillLevels.TryGetValue(pillName, out int level)) {
            return level;
        }
        return 0;
    }

    // if less than the max level we upgrade the current pill level
    public void UpgradePill(string pillName) {
        if (pillLevels.ContainsKey(pillName) && pillLevels[pillName] < GameManager.MAX_LEVEL) {
            pillLevels[pillName]++;
        }
    }

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
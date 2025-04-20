using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int bodyCount = 0;
    [SerializeField] private GameObject gameOverInterface;
    [SerializeField] private GameObject redImage;
    [SerializeField] private GameObject time;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject parkinsonButton;
    [SerializeField] private GameObject redVisionButton;
    [SerializeField] private GameObject placeholder1;
    [SerializeField] private GameObject placeholder2;
    [SerializeField] private GameObject placeholder3;
    [SerializeField] private GameObject placeholder4;
    private PlayerScript playerScript;
    private Dictionary<string, int> items = new();
    public bool gameOver;

    public static GameManager Instance { get; private set; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        items.Add("Parkinson", 10);
        items.Add("Red Vision", 15);
        items.Add("Placeholder", 20);
        items.Add("Placeholder1", 25);
        items.Add("Placeholder2", 30);
        items.Add("Placeholder3", 40);
    }


    // this should be in the camera script item selector method, but for now keep it here
    private void Update() {
        if (playerScript.getMoney() >= items["Parkinson"]) {
            parkinsonButton.GetComponent<Button>().interactable = true;
        } else {
            parkinsonButton.GetComponent<Button>().interactable = false;
        }

        if (playerScript.getMoney() >= items["Red Vision"]) {
            parkinsonButton.GetComponent<Button>().interactable = true;
        } else {
            parkinsonButton.GetComponent<Button>().interactable = false;
        }
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public void addBodyCounter() {
        bodyCount++;

        if(bodyCount == 3) {
            time.SetActive(false);
            redImage.SetActive(false);
            gameOverInterface.SetActive(true);
            gameOver = true;


        // this doesnt belong here, just wanted to know if it worked, should be placed in a script of the "finished" table to the left of our operating table in the OnCollisionEnter method, not on the trash one lol
        } else {
            playerScript.addMoney(10);
        }
    }

    public void wantToBuyItem(string itemName) {
        switch (itemName) {
            case "Parkinson":
                if (canBuyItem(itemName)) {
                    playerScript.payPrice(items[itemName]);
                    // does the effect of the item
                }
                break;
            case "Red Vision":
                if (canBuyItem(itemName)) {
                    playerScript.payPrice(items[itemName]);
                    // does the effect of the item
                }
                break;
            case "asd":
                if (canBuyItem(itemName)) {
                    playerScript.payPrice(items[itemName]);
                    // does the effect of the item
                }
                break;
            case "sdasdsa":
                if (canBuyItem(itemName)) {
                    playerScript.payPrice(items[itemName]);
                    // does the effect of the item
                }
                break;
            case "rdsfa":
                if (canBuyItem(itemName)) {
                    playerScript.payPrice(items[itemName]);
                    // does the effect of the item
                }
                break;
            case "ddfsadsa":
                if (canBuyItem(itemName)) {
                    playerScript.payPrice(items[itemName]);
                    // does the effect of the item
                }
                break;
        }
    }

    private bool canBuyItem(string itemName) {
        int playerMoney = playerScript.getMoney();
        int costMoney = items[itemName];

        return playerMoney >= costMoney;
    }

}

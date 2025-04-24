using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
    private int bodyCount = 0;
    public bool gameOver;
    public static int MAX_LEVEL = 3;


    [SerializeField] private GameObject gameOverInterface;
    [SerializeField] private GameObject redImage;
    [SerializeField] private GameObject time;
    [SerializeField] private GameObject player;

    // BUTTONS
    [SerializeField] private GameObject parkinsonButton;
    [SerializeField] private GameObject redVisionButton;
    [SerializeField] private GameObject placeholder1;
    [SerializeField] private GameObject placeholder2;
    [SerializeField] private GameObject placeholder3;
    [SerializeField] private GameObject placeholder4;

    [SerializeField] private TextMeshProUGUI parkinsonLevelText;
    [SerializeField] private TextMeshProUGUI redVisionLevelText;

    private PlayerScript playerScript;
    private Dictionary<string, int[]> itemPrices = new Dictionary<string, int[]>();

    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start() {
        playerScript = player.GetComponent<PlayerScript>();

        // adding the different pills with their prices on each level
        itemPrices.Add("Parkinson", new int[] { 10, 15, 20 });
        itemPrices.Add("Red Vision", new int[] { 15, 20, 25 });
        itemPrices.Add("Placeholder", new int[] { 20, 25, 30 });
        itemPrices.Add("Placeholder1", new int[] { 25, 30, 35 });
        itemPrices.Add("Placeholder2", new int[] { 30, 35, 40 });
        itemPrices.Add("Placeholder3", new int[] { 40, 45, 50 });

        UpdateButtonStates();
    }

    private void Update() {
        UpdateButtonStates();
    }

    private void UpdateButtonStates() {
        int parkinsonLevel = playerScript.GetPillLevel("Parkinson");

        // if we haven't yet bought the max level pills we can keep upgrading the ones we have, if not then the button (to buy) is no longer interactable
        if (parkinsonLevel < MAX_LEVEL) {
            int price = itemPrices["Parkinson"][parkinsonLevel];
            parkinsonButton.GetComponent<Button>().interactable = playerScript.getMoney() >= price;

            if (parkinsonLevelText != null) {
                parkinsonLevelText.text = $"Level {parkinsonLevel}/3 - ${price}";
            }
        } else {
            parkinsonButton.GetComponent<Button>().interactable = false;
            if (parkinsonLevelText != null) {
                parkinsonLevelText.text = "MAX LEVEL";
            }
        }

        int redVisionLevel = playerScript.GetPillLevel("Red Vision");
        if (redVisionLevel < MAX_LEVEL) {
            int price = itemPrices["Red Vision"][redVisionLevel];
            redVisionButton.GetComponent<Button>().interactable = playerScript.getMoney() >= price;

            if (redVisionLevelText != null) {
                redVisionLevelText.text = $"Level {redVisionLevel}/3 - ${price}";
            }
        } else {
            redVisionButton.GetComponent<Button>().interactable = false;
            if (redVisionLevelText != null) {
                redVisionLevelText.text = "MAX LEVEL";
            }
        }

    }

    // if the player has sent 3 clones to the trash it's game over
    public void addBodyCounter() {
        bodyCount++;

        if (bodyCount == 3) {
            time.SetActive(false);
            redImage.SetActive(false);
            gameOverInterface.SetActive(true);
            gameOver = true;
        } else {
            playerScript.addMoney(10); // the player gets money when a clone is rightfully done, not thrown into the trash but... testing purposes :)
        }
    }

    // each button on the UI will call this method with its item name
    public void BuyItem(string itemName) {
        int currentLevel = playerScript.GetPillLevel(itemName);

        // only works if we haven't yet reached the max level of the pill (the button won't be interactable anyways)
        if (currentLevel < MAX_LEVEL) {
            int price = itemPrices[itemName][currentLevel];

            if (playerScript.getMoney() >= price) {
                playerScript.payPrice(price);
                playerScript.UpgradePill(itemName);
                UpdateButtonStates();
            }
        }
    }
}
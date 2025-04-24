using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
    private int bodyCount = 0;
    public bool isGameOver;
    public static int MAX_LEVEL = 3;

    //for game loop

    [SerializeField] private GameObject bodyPrefab;

    private bool isWaitingForNextBody = false;
    private int bodyFailCount = 0;
    private int currentMoney = 0;

    //-------

    [SerializeField] private GameObject gameOverInterface;
    [SerializeField] private GameObject redImage;
    [SerializeField] private GameObject time;
    [SerializeField] private GameObject player;

    // BUTTONS
    [SerializeField] private GameObject parkinsonButton;
    [SerializeField] private GameObject redVisionButton;

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

        UpdateButtonStates();
    }

    private void Update() {
        UpdateButtonStates();
    }

    //for game loop
    public void SpawnNextBody()
    {
        //Instantiate(bodyPrefab, bodySpawnPoint.position, Quaternion.identity);
        isWaitingForNextBody = false;
    }

    public void NextBodyReady()
    {
        isWaitingForNextBody = true;
        //nextBodyButton.SetActive(true);
    }
    //chamar a função no botão
    public void OnNextBodyButtonPressed()
    {
        if (!isWaitingForNextBody) return;
        //nextBodyButton.SetActive(false);
        SpawnNextBody();
    }
    //chamada no game loop
    public void FailBody()
    {
        bodyFailCount++;
        Debug.Log("Bodies Failed: " + bodyFailCount);
    }
    public void gameOver()
    {
        time.SetActive(false);
        redImage.SetActive(false);
        gameOverInterface.SetActive(true);
        isGameOver = true;
        SoundManager.Instance.stopAllSounds();
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

        if (bodyCount == 3)
        {
            gameOver();
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
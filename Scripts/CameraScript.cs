using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    [SerializeField] private GameObject computerInterface;
    private Coroutine currentShake;
    private GameObject player;
    private PlayerScript playerScript;
    private bool isUsingComputer = false;

    private float[] baseShakeMagnitudes = { 1f, 1.1f, 1.2f };

    private void Start() {
        player = transform.parent.gameObject;
        playerScript = player.GetComponent<PlayerScript>();
    }

    private void FixedUpdate() {
        // while the game is running and the computer interface isn't on, the camera may rotate
        if (GameManager.Instance.isGameOver == false && computerInterface.activeSelf == false) {
                

        if (!isUsingComputer && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if (hit.collider.CompareTag("PC")) {
                    computerInterface.SetActive(true);
                    isUsingComputer = true;
                }
            }
        }

        if (isUsingComputer && Input.GetKeyDown(KeyCode.Escape))
{
    computerInterface.SetActive(false);
    isUsingComputer = false;
}
        } else if (GameManager.Instance.isGameOver) {
            // if game over then we stop all coroutines
            StopAllCoroutines();
        }
    }

    
    public void Shake(int stage) {
        if (currentShake != null) {
            StopCoroutine(currentShake); // if there already is a coroutine we stop it and start a new one with a different state of shaking effect
        }

        int parkinsonLevel = playerScript.GetPillLevel("Parkinson");

        float reductionFactor = 1f;
        switch (parkinsonLevel) {
            case 1:
                reductionFactor = 0.7f;
                break;
            case 2:
                reductionFactor = 0.4f;
                break;
            case 3:
                reductionFactor = 0.1f;
                break;
        }

        float adjustedMagnitude = baseShakeMagnitudes[stage] * reductionFactor;
        currentShake = StartCoroutine(ShakeEffect(adjustedMagnitude, transform.position));
    }

    private IEnumerator ShakeEffect(float magnitude, Vector3 originalPos) {
        while (true) {
            transform.position = originalPos;
            float y = Random.Range(-0.005f, 0.005f) * magnitude;
            float z = Random.Range(-0.005f, 0.005f) * magnitude;
            transform.position = originalPos + new Vector3(0, y, z);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void turnOffComputerInterface()
    {
        computerInterface.SetActive(false);
    }
}
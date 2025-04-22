using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    [SerializeField] private GameObject computerInterface;
    private Coroutine currentShake;
    private GameObject player;
    private PlayerScript playerScript;

    private float[] baseShakeMagnitudes = { 1f, 1.1f, 1.2f };

    private void Start() {
        player = transform.parent.gameObject;
        playerScript = player.GetComponent<PlayerScript>();
    }

    private void FixedUpdate() {
        // while the game is running and the computer interface isn't on, the camera may rotate
        if (GameManager.Instance.gameOver == false && computerInterface.activeSelf == false) {
            float screenWidth = Screen.width;
            float mouseX = Input.mousePosition.x;
            if (mouseX < screenWidth / 3) {
                gameObject.transform.rotation = Quaternion.Euler(28f, 235f, 0f);
            } else if (mouseX >= screenWidth / 3 && mouseX <= screenWidth * 2 / 3) {
                gameObject.transform.rotation = Quaternion.Euler(28f, 270f, 0);
            } else {
                gameObject.transform.rotation = Quaternion.Euler(28f, 305f, 0);
            }
        } else if (GameManager.Instance.gameOver) {
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
}
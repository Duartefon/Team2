using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour {
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject redImage;
    [SerializeField] private TMP_Text timeText;

    private PlayerScript playerScript;

    private Color imageColor;

    private bool hasFaded;

    private float fadeDuration = 60f;
    private float startOpacity = 0f;
    private float endOpacity = 0.3f;

    void Start() {
        imageColor = redImage.GetComponent<Image>().color;
        imageColor.a = 0f;
        playerScript = player.GetComponent<PlayerScript>();
    }

    // depending on the current time, the effects are going to progress
    void Update() {

        // only applies effects if the game is still running
        if (!GameManager.Instance.isGameOver) {
            timeText.text = Time.time.ToString("F2"); // to appear at most 2 decimal numbers

            if (Time.time > 20f) {
                GameManager.Instance.gameOver();
            } else if (Time.time > 15f) {
                camera.GetComponent<CameraScript>().Shake(2);
                if (!hasFaded) {
                    StartCoroutine(FadeRedImage());
                    hasFaded = true;
                }
            } else if (Time.time > 10f) {
                camera.GetComponent<CameraScript>().Shake(1);
            } else if (Time.time > 5f) {
                camera.GetComponent<CameraScript>().Shake(0);
            }

            int redVisionLevel = playerScript.GetPillLevel("Red Vision");
            switch (redVisionLevel) {
                case 1:
                    endOpacity = 0.2f;
                    break;
                case 2:
                    endOpacity = 0.1f;
                    break;
                case 3:
                    endOpacity = 0f;
                    break;
                default:
                    endOpacity = 0.3f;
                    break;
            }

            int parkinsonLevel = playerScript.GetPillLevel("Parkinson");
        }
    }

    // smoothly changes the opacity of the red image that causes red vision
    private IEnumerator FadeRedImage() {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startOpacity, endOpacity, elapsedTime / fadeDuration);
            imageColor.a = newAlpha;
            redImage.GetComponent<Image>().color = imageColor;
            yield return null;
        }
    }
}
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject redImage;
    [SerializeField] private TMP_Text timeText;
    private Color imageColor;
    private bool hasFaded;
    private float fadeDuration = 60f;
    private float startOpacity = 0f;
    private float endOpacity = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageColor = redImage.GetComponent<Image>().color;
        imageColor.a = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameOver) {
            timeText.text = Time.time.ToString("F2");

            if (Time.time > 15f) {
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
        }
    }

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

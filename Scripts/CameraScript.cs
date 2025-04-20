using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    private float[] shakeMagnitudes = {1f, 1.1f, 1.2f};
    private Coroutine currentShake;
    [SerializeField] private GameObject computerInterface;

    private void Start() {
        Vector3 originalPos = transform.position;
    }


    private void FixedUpdate() {

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
        } else if(GameManager.Instance.gameOver){
            StopAllCoroutines();
        }
    }

    public void Shake (int stage) {
        if (currentShake != null) {
            StopCoroutine(currentShake);
        }

        currentShake = StartCoroutine(ShakeEffect(shakeMagnitudes[stage], transform.position));

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

using UnityEngine;

public class TrashScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // if a clone or a utensil falls on the trash it's gone, if it's a clone it also adds our body counter that determined how many closes the player has lost
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Clone") || collision.gameObject.CompareTag("Utensil"))
            Destroy(collision.gameObject, 5f);
        if (collision.gameObject.CompareTag("Clone")) {
            GameManager.Instance.addBodyCounter();
        }
    }
}

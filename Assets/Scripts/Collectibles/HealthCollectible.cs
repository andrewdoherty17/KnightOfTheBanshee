using UnityEngine;

// Health collectible class that handles collision between player and the health object
public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue; // Value of health object

    // Method that handles collision with the player, the object will vanish when colliding with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

}

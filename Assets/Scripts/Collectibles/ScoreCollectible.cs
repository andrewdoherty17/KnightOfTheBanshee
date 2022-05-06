using UnityEngine;

// Score Collectible Class with main job to handle collision with the player
public class ScoreCollectible : MonoBehaviour
{
    public AudioSource CoinSound; // Plays an audio of picking up a coin

    // If the Coin collides with the Player's collider, the coin sound will play, and the coin object will vanish
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CoinSound.Play();
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Life Collectible Class with main job to handle collision with the player
public class LifeCollectible : MonoBehaviour
{
    public AudioSource LifeSound; // Plays an audio of picking up a life

    // If the Life collides with the Player's collider, the Life sound will play, and the Life object will vanish
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LifeSound.Play();
            gameObject.SetActive(false);
        }
    }
    }

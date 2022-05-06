using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that handles Checkpoint collision with the player
public class Checkpoint : MonoBehaviour
{

    // if the Checkpoint collider collides with the Player's collider, the Checkpoint flag will do an animation
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().SetBool("checkpoint", true);
        }
    }
}

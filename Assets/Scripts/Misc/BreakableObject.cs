using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that handles collision for a breakable object
public class BreakableObject : MonoBehaviour
{
    public AudioSource BreakBoxSound; // Sound plays when the box is broken

    // If the box collides with the Player, the sound will play and the box will vanish
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.tag == "Player")
        {
            BreakBoxSound.Play();
            gameObject.SetActive(false);
        }
    }
    
       
    

}

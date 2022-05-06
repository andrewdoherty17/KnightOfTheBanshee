using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used by any enemy type
public class Enemy : MonoBehaviour
{
    public Animator EnemyAnimator; // The animator controller for the enemy
    public AudioSource DeathSound; // The sound that plays when an enemy is killed
    private BoxCollider2D EnemyBox; // The enemy's Box collider
    private BoxCollider2D GroundBox; // The box collider to detect ground collisions
    int currentHealth; // Enemy's current health
    [SerializeField]int maxHealth = 100; // Maximum health of the enemy
    public GameObject TrophyWall; // Invisible wall which will be destroyed when the player kills a boss
    private int hitcount; // How many times the player has hit an enemy
    void Start()
    {
        currentHealth = maxHealth;

        // Components are Initialised

        EnemyBox = GetComponent<BoxCollider2D>(); 
        GroundBox = GetComponent<BoxCollider2D>();
    }

    // Method that when called from another class will allow the enemy to be damaged by a certain parameter
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // current health minus the damage value passed in

        if (currentHealth <= 0) // If health is less than or equal to 0, die
        {
            EnemyDeath();
        }

        else // If the enemy is not dead
        {
            hitcount = hitcount + 1;
            EnemyAnimator.SetTrigger("Hurt");
            DeathSound.Play();

            if (hitcount == 4) // If the boss has been hit four times
            {
                TrophyWall.SetActive(false); // The trophy wall is disabled and the player can get through
            }
        }
    }
    public void Die()
    {
         Destroy(this.gameObject, 0.2f); // destroys the enemy object after the animation is done, 0.2f waits 0.2/second before destroying the object
    }

    // Method that handles the death of an enemy
    public void EnemyDeath()
    {
        EnemyBox.enabled = false;
        GroundBox.enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        EnemyAnimator.SetTrigger("Dead");
        DeathSound.Play();
        Die();
    }
  
}

using System.Collections;
using UnityEngine;

// Class that handles Health, primarily of the player
public class Health : MonoBehaviour
{
    
    [SerializeField] private float startingHealth; // The starting health variable
    public float currentHealth { get; private set; } // set and get methods to data can't be changed outwith this script
    
    private void Awake()
    {
        currentHealth = startingHealth;

        currentHealth = PlayerPrefs.GetFloat("health", 3); // Player prefs initialised
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("health", currentHealth); // Player prefs are set
    }

    // Method for taking damage, the damage is a parameter passed in from another method

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
    }

    // Method that adds health to current health

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }
}

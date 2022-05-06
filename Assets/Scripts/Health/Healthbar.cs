using UnityEngine;
using UnityEngine.UI;

// Health Bar class 
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth; // Creates an instance of the Health Object
    [SerializeField] private Image totalhealthBar; // Image used by the totalhealthBar
    [SerializeField] private Image currenthealthBar; // Image used by the currenthealthBar

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 5;
        // In the sprite sheet there are 5 hearts lined up next to each other hence why 5 is the value
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 5;
    }

}

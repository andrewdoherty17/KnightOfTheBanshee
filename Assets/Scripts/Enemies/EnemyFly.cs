using UnityEngine;

// Class that has taken code from a Youtube tutorial, it is not mine
// Full credit goes to GDTitans: https://www.youtube.com/watch?v=74jShtK2dKM
public class EnemyFly : MonoBehaviour
{
    
    public float health = 1;

    [SerializeField] public float flyspeed = 0.8f;
    public float distance = 3;

    float enemyPosY;
    int direction = 1;

    private void Start()
    {
        enemyPosY = transform.position.y;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * flyspeed * Time.deltaTime * direction);
        if (transform.position.y < enemyPosY || transform.position.y > enemyPosY + distance)
            direction *= -1;
    }

}




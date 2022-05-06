using UnityEngine;

// Class that has been created but I have taken code from a Youtube tutorial, it is not mine
// Full credit goes to Kap Koder: https://www.youtube.com/watch?v=MPnN9i1SD6g
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    private Rigidbody2D enemyBody;
    private BoxCollider2D colliderNew;

    

    private void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        colliderNew = GetComponent<BoxCollider2D>();
    }

    private void Update()
    { 
        if (facingRight())
        {
            enemyBody.velocity = new Vector2(speed, 0f);
        }
        else
        {
            enemyBody.velocity = new Vector2(-speed, 0f);
        }
        
    }
    
        private bool facingRight()
        {
            return transform.localScale.x > Mathf.Epsilon;
        }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    



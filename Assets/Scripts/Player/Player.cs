using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector3 StartingPosition; // The starting co-ordinates for the Player
    public GameObject Checkpoint; // Will store the position of a checkpoint for a level

    private Animator PlayerAnimator; // The Animator in charge of the Player object
    private Rigidbody2D PlayerBody; // This is used so that physics can be applied to the Player object 
    private BoxCollider2D PlayerBox; // Used to detect collisions
    [SerializeField] private LayerMask groundLayer; // The Layer that the Tilemap for each level will be based on

    private float LeftRight;
    private float PlayerSpeed = 13f; // the Speed of the player
    private float PlayerJump = 25f; // the Jump force of the player
    
    public Transform AttackPosition; // A position where the player can attack from
    [SerializeField] private float weaponRange = 0.5f; // the range that the weapon has

    // Layer masks for two different enemies
    public LayerMask Enemy;
    public LayerMask Boss;

    [SerializeField]private Health PlayerHealth; // Instance of the Health Class is created

    // Different Audio clips used for different events

    public AudioSource JumpSound;
    public AudioSource AttackSound;
    public AudioSource HurtSound;
    public AudioSource DeathSound;
    public AudioSource CheckpointSound;
    public AudioSource HeartSound;
    public AudioSource FinishGame;

    // Text Boxes where the score, amount of lives and the final score will be shown

    [SerializeField]private Text scoreText;
    [SerializeField] private Text LivesText;
    [SerializeField] private Text FinalScore;
    [SerializeField] private Text FinalTotalScore;

    // Score variable and lives variable

    private int score = 0;
    [SerializeField]private int lives = 5;

    public GameObject GameOver; // This object will make the game over screen show up
    public GameObject GameFinish; // This object will make the final screen show up


    public float cooldownTime = 2; // Cooldown time for attacking
    private float nextStrike = 0; // When the player can attack next

    private bool canJump; // Can the player double jump

    private void Awake()
    {
        StartingPosition = transform.position; // Starting position is set to the current position of the player

        // Components are initialised

        PlayerAnimator = GetComponent<Animator>();
        PlayerBody = GetComponent<Rigidbody2D>();
        PlayerBox = GetComponent<BoxCollider2D>();
        
        // Player prefs are used to store lives, score and current health so that they are not discarded when going to a different level

        lives = PlayerPrefs.GetInt("lives", 5); // Lives inital value is 5
        LivesText.text = lives.ToString();

        score = PlayerPrefs.GetInt("score", 0); // Score initial value is 0
        scoreText.text = score.ToString();

    }

    // DESIGN PATTERN - UPDATE METHOD
    private void Update()
    {
        // Player prefs are set

        PlayerPrefs.SetInt("lives", lives);
        LivesText.text = lives.ToString();
       
        PlayerPrefs.SetInt("score", score);
        scoreText.text = score.ToString();
        

        LeftRight = Input.GetAxis("Horizontal");

        // The player is flipped depending on if they are going left or right

        if (LeftRight > 0.01f) // if player is facing right
            transform.localScale = new Vector3(2,2,2); 
        else if (LeftRight < -0.01f) // if player is facing left
            transform.localScale = new Vector3(-2, 2, 2);
        
        // Player will move depending on if the left or right arrow key is used, and this is multiplied by the player's speed
        PlayerBody.velocity = new Vector2(LeftRight * PlayerSpeed, PlayerBody.velocity.y); 

        // Animations are initialised

        PlayerAnimator.SetBool("Run", LeftRight != 0);
        PlayerAnimator.SetBool("Grounded", isGrounded());

        PlayerBody.gravityScale = 7;
       
        // Algorithm to allow the player jump, and subsequently double jump 
        // If the up arrow is pressed the player will jump

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded())
            {
                Jump();
                canJump = true;
            }
            else if (canJump)
            {
                Jump();
                canJump = false;
            }
        }

        // If the space bar is pressed the player will attack
        // Cooldown for attack is present here

        if (Time.time > nextStrike) // as long as time passed is greated than the value of nextStrike
        {

            if (Input.GetKeyDown("space")) // The player can attack
            {
                playerAttack();
                nextStrike = Time.time + cooldownTime; // Cooldown is reset
            }
        }

    }

    // Jump method that is called when a player presses the space bar

    private void Jump()
    {
        JumpSound.Play();
        PlayerBody.velocity = Vector2.up * PlayerJump; // Player will move in y direction multiplied by the jump force
    }

    // Attack Method for the player
    private void playerAttack()
    {
        PlayerAnimator.SetTrigger("Attack"); // Attack animation is triggered
        AttackSound.Play(); // Attack sound plays

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPosition.position, weaponRange, Enemy); // A circular collider is set up to detect the weapon collision with enemies
        
        foreach  (Collider2D Enemy in hitEnemies) // For every enemy hit by the sword
        {
            Enemy.GetComponent<Enemy>().TakeDamage(100); // Enemy takes 100 damage
            addScore(100); // The score counter should increase by 100
        }
    }

    // Method to detect if the player is on the ground or not
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(PlayerBox.bounds.center, PlayerBox.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // Method that handles every collision the player has
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "FallDeath") // If the player falls of the level
        {
            Death(); // The player will die
        }

        else if (collision.tag == "Checkpoint") // If the player collides with the checkpoint
        {
            StartingPosition = transform.position; // Starting position is set to the checkpoint's location
            CheckpointSound.Play(); // Checkpoint sound will play
        }

        else if (collision.tag == "Finish") // If the player collides with the finish point at the end of the first level
        {
            SceneManager.LoadScene("Levels/Level3"); // Load the next level

        }

        else if (collision.tag == "Finish2") // If the player collides with the finish point at the end of the Second level
        {
            SceneManager.LoadScene("Levels/Level4"); // Load the next level

        }

        else if (collision.tag == "FinishGame") // If the player collides with the finish point at the end of the Second level
        {
            FinalTotalScore.text = score.ToString(); // Final score is updated
            GameFinish.SetActive(true); // Game panel visibility is set to true
            FinishGame.Play(); // Finish game sound plays
        }

        else if (collision.tag == "Coins") // If the player collides with a coin
        {
            addScore(100); // add 100 to the score
        }

        else if (collision.tag == "Lives") // If the player collides with a life collectible
        {
            lives = lives + 1; // Lives increases by 1
            LivesText.text = lives.ToString(); // The increase in lives is shown in the UI
            addScore(1000); // The score is increased by 1000
        }

        else if (collision.tag == "Heart") // If the player collides with a life collectible
        {
            PlayerHealth.AddHealth(1.0f); // add 1 to current health
            HeartSound.Play(); // Play sound
        }

        else if (collision.tag == "Enemy") // If the player collides with an enemy
        {
            PlayerHealth.TakeDamage(1.0f); // Player takes 1 damage
            HurtSound.Play(); // Sound plays
            PlayerAnimator.SetTrigger("Hurt"); // Hurt animation plays

            if (PlayerHealth.currentHealth < 1) // If the current health is less than 1
            {
                PlayerAnimator.SetTrigger("Dead"); // Play dead animation
                Death();
            }
        }

        // Method that depending on how many hearts of health the player has at the time of death, will decrease the hearts to 0 then add one back on when the player respawns
    }
    private void Die()
    {
        if (PlayerHealth.currentHealth == 3)
        {
            PlayerHealth.TakeDamage(3);
        }

        else if (PlayerHealth.currentHealth == 2)
            {
                PlayerHealth.TakeDamage(2);
            }
       
        else if (PlayerHealth.currentHealth == 1)
        {
            PlayerHealth.TakeDamage(1);
        }

            PlayerHealth.AddHealth(1);
    }

   
    // Method that handles the death of the player
    private void Death()
    {
        transform.position = StartingPosition; // Starting position is set

        lives = lives - 1; // lives is decreased by 1
        LivesText.text = lives.ToString(); // Lives are shown in the ui
        DeathSound.Play(); // Death sound plays
        PlayerAnimator.SetTrigger("Dead"); // Death animation plays

        Die(); // User dies

        if (lives <= 0) // If the player has no lives there is a gameover
        {
            FinalScore.text = score.ToString();
            GameOver.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    // Method that adds to the score
    private void addScore(int newscore)
    {
        score = score + newscore;
        scoreText.text = score.ToString();
    }

    }
    

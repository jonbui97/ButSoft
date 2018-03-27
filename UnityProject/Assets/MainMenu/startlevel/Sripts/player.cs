using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // References
    private Rigidbody2D myrigidBody;
    private Animator myAnimator;

    // Booleans
    private bool facingRight;
    private bool isGrounded;
    private bool jump;

    // Stats
    [SerializeField]
    public int currHealth;
    [SerializeField]
    public int maxHealth = 5;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;

    public Vector3 respawnPosition;
    public FollowPlayer camera;

	// Use this for initialization
	void Start ()
    {
        facingRight = true;
        myrigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        currHealth = maxHealth;

        respawnPosition = transform.position;
        
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInput();

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }

        if (currHealth <= 0)
        {
            Die();
        }
    }

    // FixedUpdate is called once per frame. Frame amount is set.
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();

        handleMovement(horizontal);
        Flip(horizontal);
        ResetValues();
	}

    // Reads inputs
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jump = true;
    }

    // Calculates player's movement amounts
    private void handleMovement(float horizontal)
    {
        if(isGrounded || airControl)
            myrigidBody.velocity = new Vector2(horizontal * movementSpeed, myrigidBody.velocity.y);

        if (isGrounded && jump)
        {
            isGrounded = false;
            myrigidBody.AddForce(new Vector2(myrigidBody.velocity.x, jumpForce));
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void ResetValues()
    {
        jump = false;
    }

    // Flips player depending on which direction looking at
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    // Checks if the player is standing on the ground or in the air.
    private bool IsGrounded()
    {
        if (myrigidBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void Die()
    {
        currHealth = maxHealth;
        transform.position = respawnPosition;
    }

    public void TakeDamage(int amount)
    {
        currHealth -= amount;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            respawnPosition = other.transform.position;
            //respawnPosition.y += 0.5f;    jei pakelt reikia kur spawn'ina
            currHealth = maxHealth;
        }
    }
}

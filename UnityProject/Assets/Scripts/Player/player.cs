using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    #region Referencees

    private Rigidbody2D myrigidBody;
    private Animator myAnimator;

    #endregion

    #region Booleans

    private bool facingRight;
    private bool isGrounded;
    private bool jump;
    private bool canDoubleJump;

    #endregion

    #region Fields

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    public Transform groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;

    #endregion

    #region Properties

    [SerializeField]
    public int currHealth;
    [SerializeField]
    public int maxHealth = 5;

    public Vector3 respawnPoint;

    #endregion

    #region Unity methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        facingRight = true;
        myrigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        RestoreHealth(maxHealth);
        respawnPoint = transform.position;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
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

    /// <summary>
    /// FixedUpdate is called once per frame. Frame amount is set.
    /// </summary>
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();

        handleMovement(horizontal);
        Flip(horizontal);
        ResetValues();
    }

    /// <summary>
    /// When he hit checkpoint flag  
    /// </summary>
    /// <param name="other">
    /// The other.
    /// </param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
            currHealth = maxHealth;
        }
    }

    #endregion

    #region Movement methods

    /// <summary>
    /// Reads inputs
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jump = true;
    }

    /// <summary>
    /// Calculates player's movement amounts.
    /// </summary>
    private void handleMovement(float horizontal)
    {
        if (isGrounded || airControl)
            myrigidBody.velocity = new Vector2(horizontal * movementSpeed, myrigidBody.velocity.y);

        // Jumping script
        if (isGrounded && jump)
        {
            SounManagerScriptSFX.PlaySound("jump_takeoff");    // plays jump sound

            isGrounded = false;
            myrigidBody.AddForce(new Vector2(0, jumpForce));
            canDoubleJump = true;
        }
        else if (!isGrounded && canDoubleJump && jump)          // double jump script
        {
            SounManagerScriptSFX.PlaySound("jump_takeoff");    // if double jumped, plays jump sound again

            canDoubleJump = false;
            myrigidBody.velocity = new Vector2(myrigidBody.velocity.x, 0);
            myrigidBody.AddForce(new Vector2(0, jumpForce));
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    /// <summary>
    /// Resets values
    /// </summary>
    private void ResetValues()
    {
        jump = false;
    }

    /// <summary>
    /// Flips player depending on which direction looking at
    /// </summary>
    /// <param name="horizontal"></param>
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

    /// <summary>
    /// Checks if the player is standing on the ground or in the air.
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        if (myrigidBody.velocity.y <= 0)
        {
            Collider2D collider = Physics2D.OverlapCircle(groundPoints.position, groundRadius, whatIsGround);

            if (collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    #endregion

    #region Health methods

    /// <summary>
    /// When currHealth reaches 0, player re-spawn and health restored  
    /// </summary>
    private void Die()
    {
        RestoreHealth(maxHealth);
        transform.position = respawnPoint;
    }

    /// <summary>
    /// when you got hit, health reduced and SFX played.
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        SounManagerScriptSFX.PlaySound("damage");
        currHealth -= amount;
    }

    /// <summary>
    /// TO restore currHealth
    /// </summary>
    /// <param name="amount"></param>
    public void RestoreHealth(int amount)
    {
        if ((currHealth + amount) <= maxHealth)
        {
            currHealth += amount;
        }
        else
        {
            currHealth = amount;
        }
    }

    #endregion
}

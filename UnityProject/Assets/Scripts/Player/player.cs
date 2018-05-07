using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class player : NetworkBehaviour
{
    #region Referencees

    private Rigidbody2D myrigidBody;

    private Animator myAnimator;

    private AudioManager _audioManager;

    [SerializeField] public Transform groundPoint;

    #endregion

    #region Booleans

    public bool isGrounded;

    private bool facingRight;
    private bool jump;
    private bool canDoubleJump;
    private bool enableDoubleJump = false;
    private bool inNoJumpingZone = false;

    #endregion

    #region Fields

    [SerializeField] private float movementSpeed;

    

    [SerializeField] private float groundRadius;

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private bool airControl;

    [SerializeField] private float jumpForce;

    private Vector3 _teleporterPosition;

    #endregion

    #region Properties

    [SerializeField] public int currHealth;

    [SerializeField] public int maxHealth = 5;

    public Vector3 respawnPoint;

    #endregion

    #region Unity methods
    /// <summary>
    /// It's restore last game stats and position
    /// </summary>
    private void Awake()
    {
        if (PlayerPrefs.GetString("Continue") == "Yes") { LoadData(); }
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        facingRight = true;
        myrigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();


        RestoreHealth(maxHealth);
        respawnPoint = this.transform.position;
        respawnPoint = this.transform.position;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        HandleInput();

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        else if (currHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// FixedUpdate is called once per frame. Frame amount is set.
    /// </summary>
    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

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
            respawnPoint.z = this.transform.position.z;
            currHealth = maxHealth;
            SaveLoadManager.SavePlayer(this);
        }

        if (other.gameObject.tag == "Teleporter")
        {
            _teleporterPosition = other.transform.position;

        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //FindObjectOfType<Camera_Controller>().GetComponent<Camera_Controller>().TurnCameraOn();
    }

    #endregion

    #region Movement methods

    /// <summary>
    /// Reads inputs
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"))))
            jump = true;
        myAnimator.SetFloat("speed", 0);
        if (Time.timeScale != 0f)
        {
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow"))))
            {
                myAnimator.SetFloat("speed", (float)0.5);
            }
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow"))))
            {
                myAnimator.SetFloat("speed", (float)0.5);
            }
        }
    }

    /// <summary>
    /// Calculates player's movement amounts.
    /// </summary>
    private void handleMovement(float horizontal)
    {
        if (isGrounded || airControl)
        {
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow"))) ||
             Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow"))))
            {
                _audioManager.PlayMovement("Run", false);
            }
            // myrigidBody.velocity = new Vector2(horizontal * movementSpeed, myrigidBody.velocity.y);
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow"))))
            {
                myrigidBody.velocity = new Vector2((float)0.75 * movementSpeed, myrigidBody.velocity.y);
            }
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow"))))
            {
                myrigidBody.velocity = new Vector2((float)-0.75 * movementSpeed, myrigidBody.velocity.y);
            }
        }


        // Jumping script
        if (isGrounded && jump && inNoJumpingZone == false)
        {
            _audioManager.PlayMovement("Jump", false);    // plays jump sound

            isGrounded = false;

            myrigidBody.AddForce(new Vector2(0, jumpForce));
            canDoubleJump = true;
        }
        else if (enableDoubleJump && !isGrounded && canDoubleJump && jump && inNoJumpingZone == false)          // double jump script
        {
            _audioManager.PlayMovement("Jump", true);    // if double jumped, plays jump sound again

            canDoubleJump = false;
            myrigidBody.velocity = new Vector2(myrigidBody.velocity.x, 0);
            myrigidBody.AddForce(new Vector2(0, jumpForce));
        }


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
        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow"))) && !facingRight ||
            Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow"))) && facingRight)
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
        if (this.myrigidBody.velocity.y <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.groundPoint.position, this.groundRadius, this.whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[0].gameObject != this.gameObject) return true;
            }
        }
        return false;
    }

    #endregion

    #region Health methods

    /// <summary>
    /// When currHealth reaches 0, player re-spawn and health restored  
    /// </summary>
    public void Die()
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
        _audioManager.PlayDamage();
        currHealth -= amount;
        //this.transform.position = _teleporterPosition;
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

    #region Load Data Method

    private void LoadData()
    {
        PlayerData data = SaveLoadManager.LoadPlayer();
        Vector3 spot = new Vector3(data.xyz[0], data.xyz[1], data.xyz[2]);

        this.respawnPoint = spot;
        this.transform.position = spot;
        this.enableDoubleJump = data.enableDoubleJump;
    }

    #endregion

    #region Enable Double Jump Methods
    public void EnableDoubleJump()
    {
        enableDoubleJump = true;
    }

    public bool GetEnableDoubleJump()
    {
        return enableDoubleJump;
    }
    #endregion

    #region No Jumping Methods

    public void DisableJumping()
    {
        inNoJumpingZone = true;
    }

    public void EnableBackJumping()
    {
        inNoJumpingZone = false;
    }

    #endregion
}

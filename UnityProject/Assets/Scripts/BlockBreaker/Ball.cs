using UnityEngine;

public class Ball : MonoBehaviour
{
    private Paddle paddle;
    private Vector3 paddleToballVector;
    private Rigidbody2D rigidbody2D;
    private bool isGameStarted;

    [SerializeField] private float speed;

	// Use this for initialization
    private void Start ()
	{
	    this.paddle = FindObjectOfType<Paddle>();
        this.paddleToballVector = this.transform.position - this.paddle.transform.position;
	    this.rigidbody2D = this.GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    private void Update ()
	{
	    if (!this.isGameStarted)
	    {
	        this.transform.position = this.paddle.transform.position + this.paddleToballVector;

            if (Input.GetMouseButtonDown(0))
	        {
	            this.rigidbody2D.velocity = new Vector2(2f, speed);
	            this.isGameStarted = true;
	        }
	    }
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 tweak = new Vector2(Random.Range(0f, 0.15f), Random.Range(0f, 0.15f));

        if (isGameStarted)
        {
            if (!other.gameObject.GetComponent<Brick>())
            {
                this.GetComponent<AudioSource>().Play();
            }

            rigidbody2D.velocity += tweak;
        }
    }
}

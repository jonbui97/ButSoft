using UnityEngine;

public class Switch_Gravity : MonoBehaviour {

    Vector2 defaultGravity;

    private void Start()
    {
        defaultGravity = Physics2D.gravity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Physics2D.gravity = new Vector2(Physics2D.gravity.x, -1 * Physics2D.gravity.y);
            collision.GetComponent<player>().GravitySwitch();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Physics2D.gravity = defaultGravity;
            collision.GetComponent<player>().GravityReset();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_Effect_No_Jumping : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<player>())
        {
            collision.GetComponent<player>().DisableJumping();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<player>())
        {
            collision.GetComponent<player>().EnableBackJumping();
        }
    }
}

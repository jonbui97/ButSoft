using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_Effect_No_Jumping : MonoBehaviour {
    public player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.DisableJumping();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.EnableBackJumping();
    }
}

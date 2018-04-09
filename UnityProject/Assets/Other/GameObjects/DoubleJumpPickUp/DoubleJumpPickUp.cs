using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPickUp : MonoBehaviour {

    public GameObject blockade;
    public player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(blockade);
        player.EnableDoubleJump();
    }
}

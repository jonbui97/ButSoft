using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPickUp : MonoBehaviour {

    public GameObject blockade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(blockade);
    }
}

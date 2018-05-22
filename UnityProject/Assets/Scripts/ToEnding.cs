using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToEnding : MonoBehaviour {
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<LevelManager>().LoadLevel("Ending");
        }
    }
}

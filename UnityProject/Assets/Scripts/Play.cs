using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public GameObject FloatingTextPrefab;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           FindObjectOfType<LevelManager>().LoadLevel("BB_Start Menu");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ShowFloatingText();
    }

    void ShowFloatingText()
    {
        Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
    }
}

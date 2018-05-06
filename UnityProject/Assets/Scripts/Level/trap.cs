using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trap : MonoBehaviour
{
    public player Player;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Trap")
        {
            if (String.Equals(PlayerPrefs.GetString("Rewind"), "yes"))
            {
                Player.TakeDamage(damage);
            }
            
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}

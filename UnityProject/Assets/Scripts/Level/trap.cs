using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trap : MonoBehaviour
{
    private static int count = 1;
    public player Player;
    public int damage = 1;

    //void OnCollision2dEnter(Collision col)
    //{
    //    if(col.gameObject.tag == "Trap")
    //        Player.TakeDamage(3);
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Trap")
        {
            if (String.Equals(PlayerPrefs.GetString("Rewind"), "yes"))
            {
                Player.TakeDamage(damage);
            print(count);
            count++;
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

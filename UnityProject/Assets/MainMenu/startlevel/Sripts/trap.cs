﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{

    public player Player;

    //void OnCollision2dEnter(Collision col)
    //{
    //    if(col.gameObject.tag == "Trap")
    //        Player.TakeDamage(3);
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Trap")
        {
            Player.TakeDamage(5);
            Debug.Log("lol");
        }
    }

    void Update()
    {
    }
}

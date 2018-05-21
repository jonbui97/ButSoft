﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBG : MonoBehaviour {

    public GameObject bg;
    public GameObject cam; // camera

    private bool enabled = false;
    private Color alpha;

    private void Start()
    {
        alpha = bg.GetComponent<SpriteRenderer>().color;
        alpha.a = 0;
    }

    private void FixedUpdate()
    {
        if (enabled && alpha.a < 1f)
        {
            alpha.a += 0.0001f;
            bg.GetComponent<SpriteRenderer>().color = alpha;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bg.transform.SetParent(cam.transform);
            bg.GetComponent<SpriteRenderer>().color = alpha;
            enabled = true;
            bg.SetActive(true);
        }
    }
}

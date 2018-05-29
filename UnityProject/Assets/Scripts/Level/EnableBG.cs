using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBG : MonoBehaviour {

    public GameObject forest_bg;
    public GameObject cave_bg;
    public GameObject cam; // camera

    private bool enabled = false;
    private Color alpha;

    private void Start()
    {
        alpha = forest_bg.GetComponent<SpriteRenderer>().color;
        alpha.a = 0;
    }

    private void FixedUpdate()
    {
        if (enabled && alpha.a < 1f)
        {
            alpha.a += 0.0001f;
            forest_bg.GetComponent<SpriteRenderer>().color = alpha;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            forest_bg.transform.SetParent(cam.transform);
            forest_bg.GetComponent<SpriteRenderer>().color = alpha;
            enabled = true;
            cave_bg.SetActive(false);
            forest_bg.SetActive(true);
        }
    }
}

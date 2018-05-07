using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour
{
    //public List<Sprite> HeartSprites;
    public Sprite[] HeartSprites;

    public Image HeartUI;

    private player player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<player>();
    }

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindObjectOfType<player>();
            return;
        }

        if (player.currHealth >= 0)
        {
            HeartUI.sprite = HeartSprites[player.currHealth];
        }
    }
}

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

    public player player;

    public static float offsetY = 5;
    public static float offsetZ = -7;
    public Vector3 offset = new Vector3(0, offsetY, offsetZ);

    void Update()
    {
        transform.position = player.transform.position + offset;
        if (player.currHealth >= 0)
        {
            HeartUI.sprite = HeartSprites[player.currHealth];
        }
    }
}

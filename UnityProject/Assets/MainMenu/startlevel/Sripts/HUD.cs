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

    void Update()
    {
        HeartUI.sprite = HeartSprites[player.currHealth];
    }
}

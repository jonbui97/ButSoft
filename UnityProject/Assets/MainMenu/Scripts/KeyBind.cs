﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBind : MonoBehaviour {

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text left, right, jump;
    private GameObject currentKey;
    private Color32 normal = new Color(255, 255, 255, 255);
    private Color32 pressed = new Color(251, 255, 0, 255);
	// Use this for initialization
	void Start () {
        //Starting keys
        keys.Add("Left", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow")));
        keys.Add("Right", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow")));
        keys.Add("Jump", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));

        //Names displayed on buttons
        if (keys["Left"] == KeyCode.LeftArrow)
        {
            left.text = "◄";
        }
        if (keys["Left"] == KeyCode.RightArrow)
        {
            right.text = "►";
        }
        jump.text = keys["Jump"].ToString();
    }
	
	// Update is called once per frame
	void Update () {
        SaveKeys();
	}

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                if (keys[currentKey.name] == KeyCode.LeftArrow)
                {
                    currentKey.transform.GetChild(0).GetComponent<Text>().text  = "◄";
                }
                else if (keys[currentKey.name] == KeyCode.RightArrow)
                {
                    currentKey.transform.GetChild(0).GetComponent<Text>().text  = "►";
                }
                else
                {
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                }
                currentKey.GetComponent<Image>().color = normal;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = pressed;
    }
    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
    }
}

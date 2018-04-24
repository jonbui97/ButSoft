using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBind : MonoBehaviour
{

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text left, right, jump, pause, cameraDown, cameraUp;
    private GameObject currentKey;
    private Color32 normal = new Color(255, 255, 255, 255);
    private Color32 pressed = new Color(251, 255, 0, 255);
    // Use this for initialization
    void Start()
    {

        //Starting keys
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        keys.Add("Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause", "Escape")));
        keys.Add("CameraUp", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CameraUp", "UpArrow")));
        keys.Add("CameraDown", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CameraDown", "DownArrow")));

        //Names displayed on buttons
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        pause.text = keys["Pause"].ToString();
        jump.text = keys["Jump"].ToString();
        cameraUp.text = keys["CameraUp"].ToString();
        cameraDown.text = keys["CameraDown"].ToString();
        if (keys["Left"] == KeyCode.LeftArrow)
        {
            left.text = "◄";
        }
        if (keys["Right"] == KeyCode.RightArrow)
        {
            right.text = "►";
        }
        if (keys["CameraUp"] == KeyCode.UpArrow)
        {
            cameraUp.text = "▲";
        }
        if (keys["CameraDown"] == KeyCode.DownArrow)
        {
            cameraDown.text = "▼";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //SaveKeys();
    }

    void OnGUI()
    {

        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey && !keys.ContainsValue(e.keyCode))
            {
                keys[currentKey.name] = e.keyCode;
                if (keys[currentKey.name] == KeyCode.LeftArrow)
                {
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = "◄";
                }
                else
                {
                    if (keys[currentKey.name] == KeyCode.RightArrow)
                    {
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "►";
                    }
                    else
                    {
                        if (keys[currentKey.name] == KeyCode.UpArrow)
                        {
                            currentKey.transform.GetChild(0).GetComponent<Text>().text = "▲";
                        }
                        else
                        {
                            if (keys[currentKey.name] == KeyCode.DownArrow)
                            {
                                currentKey.transform.GetChild(0).GetComponent<Text>().text = "▼";
                            }
                            else
                            {
                                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                            }
                        }

                    }
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

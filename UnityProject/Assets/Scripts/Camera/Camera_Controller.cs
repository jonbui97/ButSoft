using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{

    private player player;
    [SerializeField]
    private float amount;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        InitializePlayer();
    }

    void Update()
    {
        if (!player)
        {
            InitializePlayer();
            return;
        }

        if (Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CameraUp", "UpArrow"))))
        {
            Vector3 target = new Vector3(player.transform.position.x + offset.x,
                player.transform.position.y + (2 * offset.y) + amount, player.transform.position.z + offset.z);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }

        else if (Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CameraDown", "DownArrow"))))
        {
            Vector3 target = new Vector3(player.transform.position.x + offset.x,
                player.transform.position.y - offset.y - amount, player.transform.position.z + offset.z);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }

        else
        {
            Vector3 target = new Vector3(player.transform.position.x + offset.x,
                player.transform.position.y + offset.y, player.transform.position.z + offset.z);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }
    }

    private void InitializePlayer()
    {
        player = GameObject.FindObjectOfType<player>();

        if (player)
        {
            Vector3 target = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
            transform.position = target;
        }
    }

    public void TurnCameraOn()
    {
        GetComponent<Camera>().enabled = true;
    }

    public void TurnCameraOff()
    {
        GetComponent<Camera>().enabled = false;
    }
}

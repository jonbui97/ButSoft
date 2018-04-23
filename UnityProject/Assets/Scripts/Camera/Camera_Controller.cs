using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{

    public GameObject player;
    [SerializeField]
    private float amount;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector3 offset;

    void Update()
    {
        if (Input.GetAxis("Vertical") == 0)
        {         
            Vector3 target = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            Vector3 target = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + (2 * offset.y) + amount, player.transform.position.z + offset.z);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }

        else if (Input.GetAxis("Vertical") < 0)
        {
            Vector3 target = new Vector3(player.transform.position.x + offset.x, player.transform.position.y - offset.y - amount, player.transform.position.z + offset.z);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }
    }
}

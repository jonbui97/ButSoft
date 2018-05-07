using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSetup : NetworkBehaviour
{
	void Start () {
	    if (isLocalPlayer)
	    {
	        FindObjectOfType<Camera_Controller>().GetComponent<Camera_Controller>().TurnCameraOn();
        }
    }
}

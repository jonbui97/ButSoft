using UnityEngine;
using UnityEngine.Networking;

public class RotationSyncController : NetworkBehaviour
{
    [SyncVar] private Quaternion syncPlayerRotation;
    [SyncVar] private Quaternion syncCameraRotation;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform camTransform;
    [SerializeField] private float lerpRate = 15f;

    private void Start()
    {
        camTransform = FindObjectOfType<Camera_Controller>().GetComponent<Camera>().transform;
    }

    private void FixedUpdate()
    {
        TransmitRotations();
        LerpRotations();
    }

    private void LerpRotations()
    {
        if (!isLocalPlayer)
        {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
            playerTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCameraRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    private void CmdProvideRotatationsToserver(Quaternion playerRot, Quaternion camRot)
    {
        syncPlayerRotation = playerRot;
        syncCameraRotation = camRot;
    }

    [Client]
    private void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            CmdProvideRotatationsToserver(playerTransform.rotation, camTransform.rotation);
        }
    }
}

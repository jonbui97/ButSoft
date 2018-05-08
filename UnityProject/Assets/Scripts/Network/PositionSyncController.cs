using UnityEngine;
using UnityEngine.Networking;

public class PositionSyncController : NetworkBehaviour
{

    [SyncVar] private Vector3 syncPos;
    [SyncVar] private Vector3 syncTransform;

    [SerializeField] private Transform myTransform;
    [SerializeField] private float lerpRate = 15;

    private void Update()
    {
        LerpPosition();
    }

    private void FixedUpdate()
    {
        TransmitPosition();
    }

    private void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.localScale = syncTransform;
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    private void CmdProvidePositionToServer(Vector3 pos, Vector3 transform)
    {
        syncPos = pos;
        syncTransform = transform;
    }

    [ClientCallback]
    private void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.position, myTransform.localScale);
        }
    }
}

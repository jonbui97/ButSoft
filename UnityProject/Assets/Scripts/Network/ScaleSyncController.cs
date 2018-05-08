using UnityEngine;
using UnityEngine.Networking;

class ScaleSyncController : NetworkBehaviour
{
    [SyncVar] private Vector3 syncScale;

    [SerializeField] private Transform myTransform;

    private void Update()
    {
        SyncScale();
    }

    private void FixedUpdate()
    {
        TransmitPosition();
    }

    private void SyncScale()
    {
        if (!isLocalPlayer)
        {
            myTransform.localScale = syncScale;
        }
    }

    [Command]
    private void CmdProvidePositionToServer(Vector3 scale)
    {
        syncScale = scale;
    }

    [ClientCallback]
    private void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.localScale);
        }
    }
}

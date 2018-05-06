using UnityEngine;
using UnityEngine.Networking;

namespace Singletons
{
    public class MyNetworkManager : NetworkManager
    {
        #region Referencees



        #endregion

        #region Booleans



        #endregion

        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Unity methods

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {

        }

        /// <summary>
        /// Use this for initialization
        /// </summary>
        private void Start()
        {

        }

        #endregion

        #region New

        public void MyStartHost()
        {
            Debug.Log(Time.timeSinceLevelLoad + " Starting Host.");
            StartHost();
        }

        public override void OnStartHost()
        {
            Debug.Log(Time.timeSinceLevelLoad + " Host started.");
        }

        public override void OnStartClient(NetworkClient myClient)
        {
            Debug.Log(Time.timeSinceLevelLoad + " Client start requested.");
            InvokeRepeating("PrintDots", 0f, 1f);
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            Debug.Log(Time.timeSinceLevelLoad + " Client is connect to IP: " + conn.address);
            CancelInvoke();
        }

        void PrintDots()
        {
            Debug.Log(".");
        }

        #endregion
    }
}

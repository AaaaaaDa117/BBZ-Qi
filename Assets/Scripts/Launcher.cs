
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace TurnBase
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Field

        private string gameVersion = "1";

        private const string playerNamePrefKey = "PlayerName";

        private InputField inputPlayerName;
        

        #endregion

        #region Public Field

        public GameObject controlPanel;
        public GameObject loadingPanel;

        public GameObject textConnectFailed;

        public Button buttonConnect;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            // Debug purpose
            Debug.Assert(controlPanel != null);
            Debug.Assert(loadingPanel != null);
            Debug.Assert(textConnectFailed != null);
            Debug.Assert(buttonConnect != null);

            //
            controlPanel.SetActive(true);
            loadingPanel.SetActive(false);
            textConnectFailed.SetActive(false);

            // 
            inputPlayerName = controlPanel.GetComponentInChildren<InputField>();
            Debug.Assert(inputPlayerName != null);

            string defaultName = string.Empty;
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
            }

            inputPlayerName.text = defaultName;

            PhotonNetwork.NickName = defaultName;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region PUN Callbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("Lacuncher: Connected to Master");
            SceneManager.LoadScene("Lobby");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnect");

            if (controlPanel != null) 
            {
                controlPanel.SetActive(true);
            }
            if (loadingPanel != null)
            {
                loadingPanel.SetActive(false);
            }
            if (textConnectFailed != null)
            {
                textConnectFailed.SetActive(true);
            }
        }
        

        #endregion

        #region Public Methods

        public void Connect()
        {
            Debug.Log("Lacuncher: Try Connecting...");
            if (!PhotonNetwork.IsConnected)
            {
                PlayerPrefs.SetString(playerNamePrefKey, PhotonNetwork.NickName);

                controlPanel.SetActive(false);
                loadingPanel.SetActive(true);

                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = this.gameVersion;
                PhotonNetwork.AutomaticallySyncScene = true;
            }
            else
            {
                // Todo
            }
        }

        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                buttonConnect.interactable = false;
                return;
            }

            buttonConnect.interactable = true;
            
            PhotonNetwork.NickName = value;

            //Debug.Log("New name: " + value);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion
    }
}


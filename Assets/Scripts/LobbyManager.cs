using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


namespace TurnBase
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        #region Private Fields

        private string myNickName;
        private byte maxPlayersPerRoom = 2;

        #endregion

        #region Public Fields
        public Transform transformLobbyUI;
        public Transform transformMatchingUI;

        public Transform targetTrans;

        public Camera mCamera;
        #endregion

        #region MonoBehaviour
        // Start is called before the first frame update
        void Start()
        {
            myNickName = PhotonNetwork.NickName;

            mCamera = Camera.main;
            targetTrans = transformLobbyUI;
        }

        // Update is called once per frame
        void Update()
        {
            // Quick and dirty
            // Need to rework
            if (!mCamera.transform.position.Equals(targetTrans.position)) 
            {
                mCamera.transform.position = Vector3.MoveTowards(mCamera.transform.position, targetTrans.position, 600.0f*Time.deltaTime);
            }

            if (!mCamera.transform.rotation.Equals(targetTrans.rotation))
            {
                mCamera.transform.rotation = Quaternion.RotateTowards(mCamera.transform.rotation, targetTrans.rotation, 600.0f * Time.deltaTime);
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                targetTrans = transformMatchingUI;
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                targetTrans = transformLobbyUI;
            }
        }

        #endregion

        #region Public Methods

        public void QuickMatch()
        {
            PhotonNetwork.JoinRandomRoom();
        }
        public void OnClickBack()
        {
            SceneManager.LoadScene(0);
            PhotonNetwork.Disconnect();
        }

        #endregion

        #region PUN Callbacks

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Lobby: On Join Random Failed");
            Debug.Log("Lobby: Creating a Room...");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Lobby: On Joined Room()");
            targetTrans = transformMatchingUI;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("Lobby: On Player Entered Room(), Player: " + newPlayer.NickName);
            if (PhotonNetwork.CurrentRoom.PlayerCount == this.maxPlayersPerRoom)
            {
                Debug.Log("Lobby: Opponent found! Loading battle scene");

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.LoadLevel("BattleScene");
                }

            }
        }
        #endregion
    }
}


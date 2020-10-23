using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Unity.Collections.LowLevel.Unsafe;

namespace TurnBase
{

    public class GameManager : MonoBehaviourPunCallbacks, IPunTurnManagerCallbacks
    {
        #region Public Fields

        #endregion

        #region Private Fields

        // Avatars for local and remote players
        [SerializeField]
        private Avatar poMyAvatar;
        [SerializeField]
        private Avatar poOtherAvatar;

        // Turn base manager, a PUN interface
        private PunTurnManager poTurnManager;

        // Used to manage all UI stuff
        private UIManager pUIManager;

        // Used to manage player input
        private InputManager pInputManager;

        [SerializeField]
        [Tooltip("Initial HP for Players")]
        private int initialHP = 3;

        [SerializeField]
        [Tooltip("Time of each turn")]
        private float turnDuration = 2.0f;

        [SerializeField]
        [Tooltip("Time interval between turns")]
        private float turnInterval = 1.0f;

        #endregion

        #region Monobehaviour Callbacks
        void Start()
        {
            // Demo code, will be removed after test
            //*
            //DemoStart();
            //*/

            // Add and initialize PunTurnManager
            poTurnManager = this.gameObject.AddComponent<PunTurnManager>();
            poTurnManager.TurnManagerListener = this;
            poTurnManager.TurnDuration = this.turnDuration;

            // Create avatars for local and remote players
            poMyAvatar = new Avatar();
            poOtherAvatar = new Avatar();

            poMyAvatar.Initialize(PhotonNetwork.LocalPlayer, this.initialHP);
            // TODO: replace null with an actual player
            if (PhotonNetwork.PlayerList[0] == PhotonNetwork.LocalPlayer)
            {
                poOtherAvatar.Initialize(PhotonNetwork.PlayerList[1], this.initialHP);
            }
            else
            {
                poOtherAvatar.Initialize(PhotonNetwork.PlayerList[0], this.initialHP);
            }
           

            // Initialize ActionManager, it's a singleton
            ActionManager.Initialize(poMyAvatar, poOtherAvatar);

            // Initialize UIManager
            this.pUIManager = this.gameObject.GetComponentInChildren<UIManager>();
            Debug.Assert(pUIManager != null);

            pUIManager.OnInitialize(poMyAvatar, poOtherAvatar);

            // Initialize InputManager
            this.pInputManager = this.gameObject.GetComponentInChildren<InputManager>();
            Debug.Assert(pInputManager != null);

            pInputManager.Initialize(this.poTurnManager);

            //


            StartCoroutine("StartBattle");

        }

        void Update()
        {
            pUIManager.UpdateTimeSlider(this.poTurnManager.ElapsedTimeInTurn, this.turnDuration);
        }

        #endregion

        #region IEnumerators

        private IEnumerator StartBattle()
        {
            Debug.LogWarning("Game: StartBattle Begin");
            yield return new WaitForSeconds(3.0f);
            Debug.LogWarning("Game: StartBattle End");

            if (PhotonNetwork.IsMasterClient)
            {
                this.poTurnManager.BeginTurn();
            }

        }

        /// <summary>
        /// Start next turn with a certain time interval
        /// </summary>
        /// <param name="time_interval"></param>
        /// <returns></returns>
        private IEnumerator NextTurn(float time_interval)
        {
            yield return new WaitForSeconds(time_interval);

            if (PhotonNetwork.IsMasterClient)
            {
                this.poTurnManager.BeginTurn();
            }

        }

        /// <summary>
        /// Goto game over stage
        /// -1) update UI
        /// 
        /// </summary>
        /// <param name="time_interval"></param>
        /// <returns></returns>
        private IEnumerator GameOver(float time_interval)
        {
            yield return new WaitForSeconds(time_interval);

            this.pUIManager.UpdateOnGameOver();

        }

        #endregion


        #region Turn Manager Callbacks

        /// <summary>
        /// Called when a player finishes a turn
        /// --- Do nothing for now
        /// </summary>
        /// <param name="player"></param>
        /// <param name="turn"></param>
        /// <param name="move"></param>
        public void OnPlayerFinished(Player player, int turn, object move)
        {
            Debug.Log("Game: OnPlayerFinished, Turn: " + turn + ", Player: " + player.NickName);




        }

        /// <summary>
        /// Called when a player makes a move in a turn
        /// - 1) Apply 'move' to player's avatar, 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="turn"></param>
        /// <param name="move"></param>
        public void OnPlayerMove(Player player, int turn, object move)
        {
            Debug.Log("Game: OnPlayerMove, Turn: " + turn + ", Player: " + player.NickName);

            // Use 'move' to set players' action
            if (player.IsLocal)
            {
                poMyAvatar.SetAction((Action.Type)move);
            }
            else
            {
                poOtherAvatar.SetAction((Action.Type)move);
            }
        }

        /// <summary>
        /// Called when a turn begins
        /// - 1) Set up UI
        /// - 2) Set a default action for local player
        /// </summary>
        /// <param name="turn"></param>
        public void OnTurnBegins(int turn)
        {
            Debug.Log("Game: OnTurnBegins, Turn: " + turn);

            // Reset UI for turn begins
            pUIManager.UpdateOnTurnBegins();


            pInputManager.ResetInput();

            // Set a default action for local player
            this.ResetAvatarAction();
        }

        /// <summary>
        /// Called when a turn is completed
        /// - 1) Determine the players' gaming status
        /// - 2）Update UI (play animation)
        /// - 3) Start next turn
        /// </summary>
        /// <param name="turn"></param>
        public void OnTurnCompleted(int turn)
        {
            Debug.Log("Game: OnTurnCompleted, Turn: " + turn);

            //*******************************************************
            // Demo Test Code
            // Simple AI opponent for quick test
            //{
            //    int i = turn % 3;

            //    if (i == 1)
            //    {
            //        poOtherAvatar.SetAction(Action.Type.Charge);
            //    }

            //    if (i == 2)
            //    {
            //        poOtherAvatar.SetAction(Action.Type.Defend);
            //    }

            //    if (i == 0)
            //    {
            //        poOtherAvatar.SetAction(Action.Type.Attack);
            //    }
            //}
            // Demo Test Code
            //*******************************************************

            // Determine the players' gaming status
            // 

            Debug.Assert(poMyAvatar != null);
            Debug.Assert(poOtherAvatar != null);

            poMyAvatar.Against(poOtherAvatar);

            // Update UI
            pUIManager.UpdateOnTurnCompleted();


            if (this.poMyAvatar.IsDead() || this.poOtherAvatar.IsDead()) 
            {
                StartCoroutine("GameOver", this.turnInterval);
            }
            else
            {
                // Start next turn
                StartCoroutine("NextTurn", this.turnInterval);
            }

        }

        /// <summary>
        /// Called when a turn is ended by time out
        /// - 1) Finish players' turn
        /// </summary>
        /// <param name="turn"></param>
        public void OnTurnTimeEnds(int turn)
        {
            Debug.Log("Game: OnTurnTimeEnds, Turn: " + turn);

            // Finish the turn
            if (!this.poTurnManager.IsFinishedByMe)
            {
                //Debug.LogError("Turn not finished by me");
                this.poTurnManager.SendMove(null , true);
            }
        }

        #endregion



        #region Private Method

        private void ResetAvatarAction()
        {
            Debug.Assert(this.poMyAvatar != null);
            Debug.Assert(this.poOtherAvatar != null);

            this.poMyAvatar.ResetAction();
            this.poOtherAvatar.ResetAction();
        }

        #endregion



        #region PhotonCallBack

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("Player left room: " + otherPlayer.NickName);
        }
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("Lobby");
        }

        #endregion
        //*******************************************************
        // Demo Test Code
        #region Demo test functions

        //*********************************
        // Demo purpose
        private string gameVersion = "1";
        //*********************************
        void DemoStart()
        {
            Debug.Log("GameManager: Connecting to Master...");
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.GameVersion = this.gameVersion;
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.NickName = "MyPlayer";
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("GameManager: Connected to Master Succeed");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 1 });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("GameManager: Joined Room");
             StartCoroutine("StartBattle");
        }

        #endregion
        // Demo Test Code
        //*******************************************************
    }
}


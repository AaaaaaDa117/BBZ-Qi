
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBase
{
    public class UIManager : MonoBehaviour
    {
        #region Public Reference

        public Text text;
        //public GameObject textTurnBegin;
        //public GameObject textTurnEnd;

        public Text myPlayerName;
        //public Text myPlayerAction;

        public Text textOpponentName;
        //public Text textOpponentAction;

        public Text textMyHP;
        public Text textOtherHP;
        public Text textMySP;

        public Slider sliderTimeCounter;

        public Button btnAttack;
        public Button btnSuperAttack;
        public Button btnCharge;
        public Button btnDefend;

        //animator object
        public GameObject myPlayer;
        public GameObject otherPlayer;

        public HealthUI myHealth;
        public HealthUI otherHealth;

        public EnergyUI myEnergyUI;

        public GameObject GameOverUI;
        public Text textResult;

        #endregion

        #region Private Field

        private const string sTakeAction = "Take your Action";

        // Reference of players' avatars from game manager
        private Avatar pMyAvatar;
        private Avatar pOtherAvatar;

        #endregion


        #region UI Update Methods

        /// <summary>
        /// Initialize UI on battle starts
        /// </summary>
        /// <param name="myAva"></param>
        /// <param name="otherAva"></param>
        public void OnInitialize(Avatar myAva, Avatar otherAva)
        {
            this.pMyAvatar = myAva;
            this.pOtherAvatar = otherAva;


            text.text = "Choose an action";
            //textTurnBegin.SetActive(false);
            //textTurnEnd.SetActive(false);

            //sliderTimeCounter.gameObject.SetActive(false);

            UpdatePlayerStatusText();

            myPlayerName.text = pMyAvatar.NickName;
            textOpponentName.text = pOtherAvatar.NickName;

            //myPlayerAction.text = sTakeAction;
            //textOpponentAction.text = "......";

            sliderTimeCounter.value = 1f;

            this.DisableAllButtons();

            this.GameOverUI.SetActive(false);
            //this.canvasGameOver.enabled = false;

            ActionManager.SetPlayerAnim(myPlayer, otherPlayer);

        }

        /// <summary>
        /// Update UI on game over
        /// </summary>
        public void UpdateOnGameOver()
        {
            //this.canvasGameOver.gameObject.SetActive(true);
            //this.canvasGameOver.enabled = true;
            //text.text = "Game Over";
            this.GameOverUI.SetActive(true);
            this.textResult.text = this.GetBattleResult();
            text.text = this.GetBattleResult();

           if(this.pMyAvatar.IsDead())
            {
                this.myPlayer.GetComponent<Animator>().SetTrigger("Die");
            }
            else
            {
                this.otherPlayer.GetComponent<Animator>().SetTrigger("Die");
            }
        }

        /// <summary>
        /// Update UI on turn begins
        /// </summary>
        public void UpdateOnTurnBegins()
        {
            //textTurnBegin.SetActive(true);
            //textTurnEnd.SetActive(false);
            sliderTimeCounter.gameObject.SetActive(true);

            this.HandleButtonsOnTurnBegins();

            //myPlayerAction.text = sTakeAction;
            //textOpponentAction.text = "......";
        }

        /// <summary>
        /// Update UI on turn is completed
        /// </summary>
        public void UpdateOnTurnCompleted()
        {
            //textTurnBegin.SetActive(false);
            //textTurnEnd.SetActive(true);
            sliderTimeCounter.gameObject.SetActive(false);

            UpdatePlayerStatusText();

            this.DisableAllButtons();

            //switch (pMyAvatar.GetAction())
            //{
            //    case Action.Type.Attack:
            //        myPlayerAction.text = "Attack";
            //        break;

            //    case Action.Type.Defend:
            //        myPlayerAction.text = "Defend";
            //        break;

            //    case Action.Type.Charge:
            //        myPlayerAction.text = "Load";
            //        break;

            //    default:
            //        Debug.Assert(false);
            //        break;
            //}

            //switch (pOtherAvatar.GetAction())
            //{
            //    case Action.Type.Attack:
            //        textOpponentAction.text = "Attack";
            //        break;

            //    case Action.Type.Defend:
            //        textOpponentAction.text = "Defend";
            //        break;

            //    case Action.Type.Charge:
            //        textOpponentAction.text = "Load";
            //        break;

            //    default:
            //        Debug.Assert(false);
            //        break;
            //}
        }



        #endregion

        #region Public Methods

        public void UpdateTimeSlider(float timeElapsed, float timeTotal)
        {
            float value = Mathf.Clamp01(timeElapsed / timeTotal);

            this.sliderTimeCounter.value = 1-value;
        }

        #endregion

        #region Private Methods

        private void DisableAllButtons()
        {
            this.btnAttack.interactable = false;
            this.btnCharge.interactable = false;
            this.btnDefend.interactable = false;
            this.btnSuperAttack.interactable = false;
        }

        private void HandleButtonsOnTurnBegins()
        {
            Text btnAttackText = this.btnAttack.GetComponentInChildren<Text>();
            Debug.Assert(btnAttackText != null);
            btnAttackText.color = Color.black;

            if (pMyAvatar.GetSP() > 0)
            {
                this.btnAttack.interactable = true;
            }
            else
            {
                this.btnAttack.interactable = false;

                btnAttackText.color = Color.grey;
            }
            if (pMyAvatar.GetSP() >= 3)
            {
                this.btnSuperAttack.interactable = true;
            }
            
            this.btnCharge.interactable = true;
            this.btnDefend.interactable = true;

        }

        private void UpdatePlayerStatusText()
        {
            int myCurrentHP = this.pMyAvatar.GetHP();

            myHealth.UpdateHealthUI(myCurrentHP);

            //textMyHP.text = this.pMyAvatar.GetHP().ToString();
            //textMySP.text = this.pMyAvatar.GetSP().ToString();
            int myCurrentEnergy = this.pMyAvatar.GetSP();

            myEnergyUI.UpdateEnergyUI(myCurrentEnergy);

            int otherCurrentHP = this.pOtherAvatar.GetHP();
            otherHealth.UpdateHealthUI(otherCurrentHP);

            //textOtherHP.text = this.pOtherAvatar.GetHP().ToString();
        }

        private string GetBattleResult()
        {
            if (this.pMyAvatar.IsDead()) 
            {
                return "You Lose!";
            }
            else
            {
                return "You Win!";
            }
        }

        #endregion

        #region MonoBehaviour Callback
        void Start()
        {
        }

        void Update()
        {
           
        }

        #endregion

    }
}


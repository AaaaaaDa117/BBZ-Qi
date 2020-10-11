
using Photon.Realtime;
using System;
using UnityEngine;

namespace TurnBase
{
    [Serializable]
    public class Avatar
    {
        #region Data

        // Data
        private Player pPunPlayer;
        private Action pAction;
        
        [SerializeField]
        private int HP;
        [SerializeField]
        private int SP;

        public string NickName;

        #endregion

        #region Methods

        public Avatar()
        {
            this.pPunPlayer = null;
            this.pAction = null;

            this.HP = 0;
            this.SP = 0;
        }

        public void Initialize(Player pPlayer, int init_HP = 3)
        {
            this.pPunPlayer = pPlayer;
            this.HP = init_HP;

            if(pPlayer!=null)
            {
                this.NickName = pPlayer.NickName;
            }
            else
            {
                this.NickName = "Computer";
            }
            
        }

        public void Against(Avatar other)
        {
            other.pAction.Accept(this.pAction);
        }

        public bool IsDead()
        {
            return this.HP <= 0;
        }

        public int GetSP()
        {
            return this.SP;
        }
        public int GetHP()
        {
            return this.HP;
        }

        public void TakeDamage(int damage)
        {
            this.HP -= damage;
        }

        public void GatherSP()
        {
            this.SP++;
        }

        public void ConsumeSP(int count)
        {
            this.SP--;

            if (this.SP < 0) 
            {
                this.SP = 0;
            }
        }

        public void SetAction(Action.Type type)
        {
            ActionManager.SetAction(ref this.pAction, type);
            Debug.Assert(this.pAction != null);
        }

        public Action.Type GetAction()
        {
            return this.pAction.GetActType();
        }
        #endregion
    }
}

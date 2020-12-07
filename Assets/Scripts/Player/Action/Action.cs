using UnityEngine;

namespace TurnBase
{
    abstract public class Action
    {
        public enum Type
        {
            Charge = 0,
            Attack,
            SuperAttack,
            Defend,

            None
        }

        #region Data

        protected Type   type;
        protected Avatar pMyAvatar;
        protected Avatar pOtherAvatar;

        protected GameObject pMyPlayerAnim;
        protected GameObject pOtherAnim;

        #endregion

        #region Methods

        private Action() { }

        protected Action(Type _type, Avatar myAva, Avatar otherAva)
        {
            this.type = _type;
            this.pMyAvatar = myAva;
            this.pOtherAvatar = otherAva;
        }

        public Type GetActType()
        {
            return this.type;
        }

        public void SetAnim(GameObject myPlayerAnim, GameObject otherPlayerAnim)
        {
            this.pMyPlayerAnim = myPlayerAnim;
            this.pOtherAnim = otherPlayerAnim;
        }

        #endregion

        #region Visitor Pattern Stuff

        public abstract void Accept(Action action);

        public virtual void AgainstCharge(ActCharge charge)
        {
            Debug.Assert(false);
            Debug.LogError("Action: AgainstCharge() not implemented");
        }

        public virtual void AgainstDefend(ActDefend defend)
        {
            Debug.Assert(false);
            Debug.LogError("Action: AgainstDefend() not implemented");
        }

        public virtual void AgainstAttack(ActAttack attack)
        {
            Debug.Assert(false);
            Debug.LogError("Action: AgainstAttack() not implemented");
        }

        #endregion
    }
}


using UnityEngine;

namespace TurnBase
{
    public class ActDefend : Action
    {
        public ActDefend(Avatar myAva, Avatar otherAva)
            : base(Type.Defend, myAva, otherAva)
        {

        }

        public override void Accept(Action action)
        {
            action.AgainstDefend(this);
        }

        public override void AgainstAttack(ActAttack attack)
        {
            if (attack.power == 1)
            {
                Debug.LogError("Defend succeed!");

                this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Defend");
                this.pOtherAnim.GetComponent<Animator>().SetTrigger("Attack");
            }
            else
            {
                Debug.LogError("Defend failed!");

                this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("DefendFail");
                this.pOtherAnim.GetComponent<Animator>().SetTrigger("SuperAttack");
                this.pMyAvatar.TakeDamage(1);
            }

            // Nothing for now
        }

        public override void AgainstCharge(ActCharge charge)
        {
            Debug.LogError("Defend empty!");

            this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Defend");
            this.pOtherAnim.GetComponent<Animator>().SetTrigger("Charge");
            // Nothing for now
        }

        public override void AgainstDefend(ActDefend defend)
        {
            Debug.LogError("Equal - Defend!");

            this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Defend");
            this.pOtherAnim.GetComponent<Animator>().SetTrigger("Defend");
            // Nothing for now
        }
    }
}

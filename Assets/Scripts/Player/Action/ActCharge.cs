
using UnityEngine;

namespace TurnBase
{
    public class ActCharge : Action
    {
        public ActCharge(Avatar myAva, Avatar otherAva)
            : base(Type.Charge, myAva, otherAva)
        {

        }

        public override void Accept(Action action)
        {
            action.AgainstCharge(this);
        }

        public override void AgainstAttack(ActAttack attack)
        {

            Debug.LogError("Hurt!");

            this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Hurt");
            this.pOtherAnim.GetComponent<Animator>().SetTrigger(attack.power>1?"SuperAttack":"Attack");
            
            // Take Damage
            this.pMyAvatar.TakeDamage(attack.power);

            //this.pOtherAvatar.ConsumeSP(1);
        }

        public override void AgainstCharge(ActCharge charge)
        {
            Debug.LogError("Equal-Charge succeed!");

            this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Charge");
            this.pOtherAnim.GetComponent<Animator>().SetTrigger("Charge");
            this.pMyAvatar.GatherSP();
        }

        public override void AgainstDefend(ActDefend defend)
        {
            Debug.LogError("Charge succeed!");

            this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Charge");
            this.pOtherAnim.GetComponent<Animator>().SetTrigger("Defend");
            this.pMyAvatar.GatherSP();
        }
    }
}

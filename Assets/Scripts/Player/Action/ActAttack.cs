using UnityEngine;

namespace TurnBase
{
    public class ActAttack : Action
    {
        public ActAttack(Avatar myAva, Avatar otherAva)
            : base(Type.Attack, myAva, otherAva)
        {

        }

        public override void Accept(Action action)
        {
            action.AgainstAttack(this);
        }

        public override void AgainstAttack(ActAttack attack)
        {
            Debug.LogError("Equal-No harm!");

            // Consume SP
            this.pMyAvatar.ConsumeSP(1);
            this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Attack");
            this.pOtherAnim.GetComponent<Animator>().SetTrigger("Attack");

        }

        public override void AgainstCharge(ActCharge charge)
        {
            Debug.LogError("Attack succeed!");

            this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Attack");
            this.pOtherAnim.GetComponent<Animator>().SetTrigger("Hurt");

            // Attack succeed!
            this.pMyAvatar.ConsumeSP(1);

            this.pOtherAvatar.TakeDamage(1);
        }

        public override void AgainstDefend(ActDefend defend)
        {
            Debug.LogError("Attack failed!");
            this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Attack");
            this.pOtherAnim.GetComponent<Animator>().SetTrigger("Defend");
            // Attack failed!
            this.pMyAvatar.ConsumeSP(1);
        }
    }
}

using UnityEngine;

namespace TurnBase
{
    public class ActAttack : Action
    {
        public int power = 1;
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
            if (this.power > attack.power)
            {
                Debug.LogError("Attack succeed!");
                // Consume SP
                this.pMyAvatar.ConsumeSP(3);
                this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("SuperAttack");
                this.pOtherAnim.GetComponent<Animator>().SetTrigger("Hurt");

                pOtherAvatar.TakeDamage(1);
            }
            else if (this.power < attack.power)
            {
                Debug.LogError("Attack failed!");
                // Consume SP
                this.pMyAvatar.ConsumeSP(1);
                this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Hurt");
                this.pOtherAnim.GetComponent<Animator>().SetTrigger("SuperAttack");

                pMyAvatar.TakeDamage(1);
            }
            else
            {
                Debug.LogError("Equal-No harm!");

                // Consume SP
                this.pMyAvatar.ConsumeSP(this.power > 1 ? 3 : 1);
                this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger(this.power>1?"SuperAttack":"Attack");
                this.pOtherAnim.GetComponent<Animator>().SetTrigger("Attack");
            }


        }

        public override void AgainstCharge(ActCharge charge)
        {
            Debug.LogError("Attack succeed!");

            this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger(this.power > 1 ? "SuperAttack" : "Attack");
            this.pOtherAnim.GetComponent<Animator>().SetTrigger("Hurt");

            // Attack succeed!
            this.pMyAvatar.ConsumeSP(this.power > 1 ? 3 : 1);

            this.pOtherAvatar.TakeDamage(this.power);
        }

        public override void AgainstDefend(ActDefend defend)
        {
            if (power == 1)
            {
                Debug.LogError("Attack failed!");
                this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("Attack");
                this.pOtherAnim.GetComponent<Animator>().SetTrigger("Defend");
                // Attack failed!
                this.pMyAvatar.ConsumeSP(1);
            }
            else
            {
                Debug.LogError("SuperAttack succed!");
                this.pMyPlayerAnim.GetComponent<Animator>().SetTrigger("SuperAttack");
                this.pOtherAnim.GetComponent<Animator>().SetTrigger("DefendFail");
                // Attack failed!
                this.pMyAvatar.ConsumeSP(3);
                pOtherAvatar.TakeDamage(1);
            }

        }
    }
}

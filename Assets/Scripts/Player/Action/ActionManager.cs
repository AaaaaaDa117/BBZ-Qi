
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TurnBase
{
    public class ActionManager
    {
        #region Data

        private static ActionManager pInstance = null;

        private ActAttack poActAttack;
        private ActAttack poActSuperAttack;
        private ActCharge poActCharge;
        private ActDefend poActDefend;

        #endregion

        #region Methods
        private ActionManager()
        { }

        private void Set(Avatar myAva, Avatar otherAva)
        {
            this.poActAttack = new ActAttack(myAva, otherAva);
            this.poActSuperAttack = new ActAttack(myAva, otherAva);
            this.poActCharge = new ActCharge(myAva, otherAva);
            this.poActDefend = new ActDefend(myAva, otherAva);
        }




        #endregion

        #region Public Interface

        public static void Initialize(Avatar myAva, Avatar otherAva)
        {
            Debug.Assert(pInstance == null);
            pInstance = new ActionManager();
            pInstance.Set(myAva, otherAva);

            pInstance.poActAttack.power = 1;
            pInstance.poActSuperAttack.power = 2;
        }


        public static void SetPlayerAnim(GameObject myPlayerAnim, GameObject otherPlayerAnim)
        {
            Debug.Assert(pInstance != null);
            pInstance.poActAttack.SetAnim(myPlayerAnim, otherPlayerAnim);
            pInstance.poActSuperAttack.SetAnim(myPlayerAnim, otherPlayerAnim);
            pInstance.poActCharge.SetAnim(myPlayerAnim, otherPlayerAnim);
            pInstance.poActDefend.SetAnim(myPlayerAnim, otherPlayerAnim);

        }


        public static void SetAction(ref Action action, Action.Type type)
        {
            Debug.Assert(pInstance != null);

            switch (type)
            {
                case Action.Type.Attack:
                    action = pInstance.poActAttack;
                    break;
                case Action.Type.SuperAttack:
                    action = pInstance.poActSuperAttack;
                    break;

                case Action.Type.Charge:
                    action = pInstance.poActCharge;
                    break;

                case Action.Type.Defend:
                    action = pInstance.poActDefend;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

        }

        //// Getter
        //public static ActAttack GetActAttack()
        //{
        //    Debug.Assert(pInstance != null);

        //    return pInstance.poActAttack;
        //}

        //public static ActCharge GetActCharge()
        //{
        //    Debug.Assert(pInstance != null);

        //    return pInstance.poActCharge;
        //}

        //public static ActDefend GetActDefend()
        //{
        //    Debug.Assert(pInstance != null);

        //    return pInstance.poActDefend;
        //}

        #endregion
    }
}

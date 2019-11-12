using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    //Sorgt dafür das nur Mover oder Fighter angewendetn wird, nicht beide Klassen gleichzeitig
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;

            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            currentAction = action;
        }
    }
}


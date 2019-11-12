using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    //IAction ist ein Vertrag, der festlegt, dass alle Klassen die sich darauf beziehen seine Methoden haben müssen. z.B. Cancel()
    //Von der vererbten Klassen kann immer nur eine funktionieren, alle anderen werden Cancel()
    public interface IAction
    {
        void Cancel();
    }
}


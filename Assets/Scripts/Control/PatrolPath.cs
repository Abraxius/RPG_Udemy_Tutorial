using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;

        //Stellt die Wegpunkte des Pfades visuell mit Gizmos da
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i) //Findet denn nächsten Wegpunkt heraus
        {
            if (i + 1 == transform.childCount)  //Wenn es beim letzten Wegpunkt angekommen ist, verhindert diese Abfrage, das man OutOfIndex kommt! Leidet wieder zum ersten Wegpunkt
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)  //Findet die Position des Childs vom Pfad raus, also des jeweiligen Wegpunktes
        {
            return transform.GetChild(i).position;
        }
    }
}

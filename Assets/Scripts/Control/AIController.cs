using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;  //Wie lang soll der Enemy den Player suchen?
        [SerializeField] PatrolPath patrolPath; //Muss in der Szene verknüft werden, da der Pfad in der Szene existiert
        [SerializeField] float waypointTolerance = 1f; //Abweichung zum Wegpunkt Toleranz

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player)) //Ist der Spieler in Reichweite und kann angegriffen werden?
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PartrolBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PartrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;   //guardPosition ist default

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            mover.StartMoveAction(nextPosition); //geht an die vorgesehene Position
                                                  //fighter.Cancel(); bricht das hinterher rennen ab und bleibt einfach stehen
        }

        private Vector3 GetCurrentWaypoint() //Fragt in PatrolPath.cs nach wo sich der aktuell anzusteuernde Wegpunkt befindet (Position)
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()    //Guckt nach ob es einen nächsten Wegpunkt gibt und welcher dieser ist (Index)
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()   //gibt an ob noch eine Entfernung zum nächsten Wegpunkt besteht
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWayPoint < waypointTolerance;
        }

        private void SuspicionBehaviour()
        {
            //Suspicion State
            GetComponent<ActionScheduler>().CancelCurrentAction(); //Bleibt an der letzten Player stelle und hält ausschau
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //Von Unity benannt
        private void OnDrawGizmosSelected() //zeichnet Gizmos bei dem momentan selektierten Objekt
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

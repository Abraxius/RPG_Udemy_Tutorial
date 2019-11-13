using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return;    //Wenn tot, wird nichts ausgeführt

            if (InteractWithCombat()) return; //wenn er über enemy hovert gibt es true und macht die darunter liegenden Befehle nicht
            if (InteractWithMovement()) return; //wenn er über dem Boden Hovert gibt es true
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits) //guckt ob ein angeklicktes Objekt das CombatTarget.cs besitzt
            {
                //guckt ob das raycastete Objekt (hover) ein CombatTarget hat, falls nicht, wird die schleife einfach wieder verlassen
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;   //wenn false zurück gegeben wird, wird die Schleife verlassen
                }

                if (Input.GetMouseButton(0)) //Falls es ein CombatTarget hat und angeklickt wird, wird Angegriffen
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

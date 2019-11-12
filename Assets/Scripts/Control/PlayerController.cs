using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control 
{
public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        InteractWithCombat();
        InteractWithMovement();
    }

    private void InteractWithCombat()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits) //guckt ob ein angeklicktes Objekt das CombatTarget.cs besitzt
        {
            //guckt ob das raycastete Objekt (hover) ein CombatTarget hat, falls nicht, wird die schleife einfach wieder verlassen
            CombatTarget target = hit.transform.GetComponent<CombatTarget>();
            if (target == null) continue; //wenn target == null, schleife verlassen

            if(Input.GetMouseButtonDown(0)) //Falls es ein CombatTarget hat und angeklickt wird, wird Angegriffen
            {
                GetComponent<Fighter>().Attack(target);
            }
        }
    }

    private void InteractWithMovement() 
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if (hasHit)
        {
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }

    private static Ray GetMouseRay() 
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
}

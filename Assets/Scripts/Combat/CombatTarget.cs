using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))] //Bedeutet: Das GameObject kann CombatTarget.cs nur haben, wenn es auch ein Health.cs hat!!! Health.cs kann nicht einfach weggel�scht werden
    public class CombatTarget : MonoBehaviour
    {

    }
}
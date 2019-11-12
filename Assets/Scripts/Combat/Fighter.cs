using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1.2f;
        [SerializeField] float weaponDamage = 5f;

        Transform target;
        float timeSineLastAttacK = 0;

        private void Update()
        {
            timeSineLastAttacK += Time.deltaTime;

            if (target == null) return; //Führt die nachkommenden Codestellen nur aus, wenn target != null ist, wenn also false zurück gegeben wird, ansonsten beendet es die update

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSineLastAttacK > timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeSineLastAttacK = 0;

            }
        }

        //Animations Event (Bedeutet eine Methode die in der Animation als Trigger festgelegt wurde und von da auf aufgerufen wird, muss genauso heißen wie in der Animation)
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);   //Verursacht den Schaden bei dem target
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }   
}


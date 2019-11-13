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

        Health target;
        float timeSineLastAttacK = 0;

        private void Update()
        {
            timeSineLastAttacK += Time.deltaTime;

            if (target == null) return; //Führt die nachkommenden Codestellen nur aus, wenn target != null ist, wenn also false zurück gegeben wird, ansonsten beendet es die update
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSineLastAttacK > timeBetweenAttacks)
            {
                //This will trigger the Hit() event    
                TriggerAttack();
                timeSineLastAttacK = 0;

            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animations Event (Bedeutet eine Methode die in der Animation als Trigger festgelegt wurde und von da auf aufgerufen wird, muss genauso heißen wie in der Animation)
        void Hit()
        {
            if (target == null) return; //wenn durch weglaufen die Animation abgebrochen wird, wird die Methode auch beendet
            target.TakeDamage(weaponDamage);   //Verursacht den Schaden bei dem target
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(CombatTarget combatTarget) //Falls kein CombatTarget ausgewählt ist oder der target isDead ist, wird false zurück gegeben.
        {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }   
}


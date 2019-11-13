using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead()    //Methode zum Abfragen, ob der Character Dead ist
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0); //Sorgt dafür, dass der Wert nicht unter 0 sinken kann
            if (healthPoints == 0)
            {
                Die();

            }
        }

        private void Die()
        {
            if (isDead) return;

            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
        }
    }
}
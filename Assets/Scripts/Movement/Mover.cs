using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    void Update()
    {

        UpdateAnimator();
    }

    private void UpdateAnimator()   //Sorgt für die Animation
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity); //konventiert es von global in local
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed); //Setzt den Animator Wert auf den jeweiligen Speed wert
    }

    public void MoveTo(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }
}

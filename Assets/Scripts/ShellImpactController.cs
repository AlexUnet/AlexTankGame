using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellImpactController : MonoBehaviour
{
    public GameObject impactParticle;

    void OnCollisionEnter(Collision collision){
        Instantiate(impactParticle,collision.GetContact(0).point,transform.rotation);
    }
}

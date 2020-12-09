using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellImpactController : MonoBehaviour
{
    public static int ricochets;
    public GameObject impactParticle;

    void OnCollisionEnter(Collision collision){
        ricochets++;
        Instantiate(impactParticle,collision.GetContact(0).point,transform.rotation);
        StartCoroutine(Destroy());
    }
    
    IEnumerator Destroy(){
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }
}

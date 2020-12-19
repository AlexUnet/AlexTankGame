using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellImpactController : MonoBehaviour
{
    private int ricochets;
    private int perforations;
    public GameObject impactParticle;
    [SerializeField] Rigidbody body;
    [SerializeField] SphereCollider explotion;
    public bool active;

    void Awake(){
        explotion.enabled = false;
    }

    void OnCollisionEnter(Collision collision){
        ricochets++;
        Instantiate(impactParticle,collision.GetContact(0).point,transform.rotation);
        StartCoroutine(Destroy());
    }
    void OncollisionStay(){
        
    }
    void OnCollisionExit(){

    }

    void OnTriggerEnter(Collider other){
        perforations++;
        explotion.enabled = true;
        Debug.Log("TRIGGER EN COLLIDER DE LA BALA CON "+ other.gameObject.name);
    }
    void OnTriggerStay(){
    }
    void OnTriggerExit(){

    }
    
    IEnumerator Destroy(){
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }
}

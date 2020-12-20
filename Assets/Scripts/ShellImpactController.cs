using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellImpactController : MonoBehaviour
{
    private int ricochets;
    private int perforations;
    public GameObject impactParticle;
    [SerializeField] GameObject fuseParticle;

    [SerializeField] Rigidbody body;
    [SerializeField] SphereCollider explotion;
    private bool active;

    public void SetActive(bool state){
        active = state;
    }

    void Awake(){
        explotion.enabled = false;
        active = true;
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
        
        if(active){            
            //body.isKinematic = true;
            perforations++;
            explotion.enabled = true;
            //Debug.Log("TRIGGER EN COLLIDER DE LA BALA CON "+ other.gameObject.name + " " + perforations);
        }        
    }
    void OnTriggerStay(){
    }
    void OnTriggerExit(){
        Instantiate(fuseParticle,transform.position,transform.rotation);
        //body.isKinematic = true;
        Destroy(this.gameObject);
        //StartCoroutine(Fuse());
    }
    IEnumerator Fuse(){
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
    IEnumerator Destroy(){
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }
}

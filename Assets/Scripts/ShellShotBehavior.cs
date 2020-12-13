using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellShotBehavior : MonoBehaviour
{
    [SerializeField]GameObject parent;
    [SerializeField]SphereCollider explotion;
    LayerMask layerMask = 1 << 8;
    public static bool active = true;

    void Awake(){
        explotion.enabled = false;
    }

    void OnTriggerEnter(Collider other){
        if(active){
            FuseExplotion();
            Destroy(this.gameObject);
            active = false;        
            //Debug.Log("FUSE ACTIVE BY: "+ other.gameObject.name + "");
        }        
    }

    void FuseExplotion(){
        //Debug.Log(new Vector3(transform.position.x,transform.position.y,transform.position.z));
        
        //Collider[] colliders = Physics.OverlapSphere(transform.position,1.5f,layerMask);
        //foreach(Collider nearbyObjects in colliders){
        //    nearbyObjects.GetComponent<PartBehavior>().ExplotionDamage();
        //}
        Destroy(parent);
    }

    
}

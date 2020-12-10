using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellShotBehavior : MonoBehaviour
{
    [SerializeField]GameObject explotion;
    [SerializeField]GameObject parent;
    LayerMask layerMask = 1 << 8;
    public static bool active = true;

    void OnTriggerEnter(Collider other){
        if(active){
            Destroy(this.gameObject);
            active = false;
            FuseExplotion();            
            //Debug.Log("FUSE ACTIVE BY: "+ other.gameObject.name + "");
        }        
    }

    void FuseExplotion(){
        Collider[] colliders = Physics.OverlapSphere(transform.position,1.5f,layerMask);
        foreach(Collider nearbyObjects in colliders){
            nearbyObjects.GetComponent<PartBehavior>().ExplotionDamage();
            //Debug.Log("obj: " + nearbyObjects.gameObject.name);
        }
        Destroy(parent);
    }

    
}

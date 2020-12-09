using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBehavior : MonoBehaviour
{
    private TankBehavior parent;
    private Collider part;
    private bool damaged;

    void Awake(){
        parent = this.GetComponentInParent<TankBehavior>();
        part = this.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("PERFORATION IN:" + this.gameObject.name + "BY: " + other.gameObject.name);
        if(!damaged){
            parent.Impact(this.gameObject.name,1);
            damaged = true;                     
        }        
    }

    public void ExplotionDamage(){
        parent.Impact(this.gameObject.name,2);
    }
}

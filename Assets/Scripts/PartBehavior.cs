using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBehavior : MonoBehaviour
{
    private TankBehavior parent;
    private Collider part;
    private bool state;

    void Awake(){
        parent = this.GetComponentInParent<TankBehavior>();
        part = this.GetComponent<Collider>();
        state = true;
    }

    void OnTriggerEnter(Collider other){
        //Debug.Log("PERFORATION IN:" + this.gameObject.name + "BY: " + other.gameObject.name);
        if(state){
            if(other.gameObject.tag == "Bullet"){
                parent.Impact(this.gameObject.name,1,other.gameObject.name);
            }else{

            }  
        }                                   
    }

    public void ExplotionDamage(){
        parent.Impact(this.gameObject.name,2,"EXPLOTION");
    }
}

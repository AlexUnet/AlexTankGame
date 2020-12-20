using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBehavior : MonoBehaviour
{
    private TankBehavior parent;
    private Collider part;

    [SerializeField]private int vitality; //para definir la resistencia de cada parte en el futuro si se quiere
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
                //Debug.Log("PERFORATION BY SHELL DAMAGE IN: " + gameObject.name);
                parent.Impact(this.gameObject.name,3,other.gameObject.name);
            }else{
                float dis = Vector3.Distance(other.gameObject.transform.position,transform.position);
                //Debug.Log("EXPLOTION DAMAGE IN"+ gameObject.name+ " at distance: " + dis);
                ExplotionDamage(gameObject.name,dis);
            }  
        }                                   
    }
    public void ExplotionDamage(string part,float distance){
        if(distance < 1.1f){
            parent.Impact(part,3,"explotion");
        }else if(distance < 1.50f){
            parent.Impact(part,2,"explotion");
        }else if(distance < 2.5){
            parent.Impact(part,1,"explotion");
        }        
    }
}

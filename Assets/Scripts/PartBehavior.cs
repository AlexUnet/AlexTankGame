using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBehavior : MonoBehaviour
{
    private TankBehavior parent;
    private Collider part;

    private PartsStateHudBehavior partStateBehavior;

    private Renderer partRenderer;

    [SerializeField]private int vitality; //vida de la parte
    [SerializeField]private int resistance;//para definir la resistencia de cada parte en el futuro si se quiere

    private bool state;

    
    void Awake(){
        vitality = 3;
        parent = this.GetComponentInParent<TankBehavior>();
        part = this.GetComponent<Collider>();
        partStateBehavior = this.GetComponent<PartsStateHudBehavior>();
        partRenderer = GetComponent<Renderer>();
        state = true;
    }

    #region PART HUD STATE

    public void SetVitality(int vitality){
        this.vitality = vitality;
        UpdateHudState();
    }
    
    public void UpdateHudState(){ 
        if(vitality == 2){
            partRenderer.material.SetColor("_Color",Color.yellow);
        }else if(vitality == 1){
            partRenderer.material.SetColor("_Color",Color.red);
        }else if(vitality <= 0){
            partRenderer.material.SetColor("_Color",Color.black);
        }else if(vitality == 3){
            partRenderer.material.SetColor("_Color",Color.white);
        }
    }

    #endregion

    #region IMPACT DAMAGE CALCULATIONS

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

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    private bool carried;
    private Vector3 flagPos = new Vector3(0.021f,0.5f,-0.728f); //posición del engine para estética
    private Collider body;

    void OnTriggerStay(Collider other){
        body = this.GetComponent<Collider>();
        if(!carried){
            if(other != null){
                if(other.gameObject.tag == "Player"){
                    carried = true;
                    this.transform.parent = other.transform;
                    this.transform.localPosition = flagPos;
                    body.enabled = false;
                }
            }
        }
    }
    public void Drop(){
        if(carried){
            carried = false;
            this.transform.parent = null;
            this.transform.position = new Vector3(transform.position.x,0,transform.position.z);
            body.enabled = true;
        }        
    }
}

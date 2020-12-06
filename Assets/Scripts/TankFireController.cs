using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFireController : MonoBehaviour
{

    public Transform cannon;
    public Animator fire_A;
    GameObject shell;
    public ParticleSystem fireEffect;
    bool fire;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){   
            Instantiate(fireEffect,transform.position,transform.rotation);
            fire_A.SetBool("fire",true);
            Debug.Log("FIRE");
            //Instantiate(shell,transform.position,transform.rotation);
            StartCoroutine(Reload());            
        }        
    }

    IEnumerator Reload(){
        fire = !fire;
        yield return new WaitForSeconds(5);
        
    }
}

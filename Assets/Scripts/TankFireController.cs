using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFireController : MonoBehaviour
{
    public Rigidbody tank;
    public Animator fire_A;
    public GameObject shell;
    public ParticleSystem fireEffect;
    bool fire;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(!fire){
                fire = true;
                Instantiate(fireEffect,transform.position,transform.rotation);
                fire_A.SetBool("fire",true);
                //Debug.Log("FIRE");
                StartCoroutine(Reload());
                Instantiate(shell,transform.position,transform.rotation).GetComponent<Rigidbody>().AddForce(transform.forward * 100,ForceMode.Impulse);
                tank.AddExplosionForce(400f,transform.position,700f,1,ForceMode.Impulse);
            }else{
                //Debug.Log("RELOADING");
            }
            
            
        }        
    }

    IEnumerator Reload(){
        yield return new WaitForSeconds(5);
        fire = false;
    }
}

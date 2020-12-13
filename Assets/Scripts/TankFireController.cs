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


    
    
    private bool cannonBreech = true;
    private bool barrel = true;

    void Awake(){

    }

    public void SetCannonBreech(bool state){
        cannonBreech = state;
    }

    public void SetCannonBarrel(bool state){
        barrel = state;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(cannonBreech){
                    if(!fire){
                    fire = true;
                    Instantiate(fireEffect,transform.position,transform.rotation);
                    fire_A.SetBool("fire",true);
                    StartCoroutine(Reload());
                    if(barrel)
                        Instantiate(shell,transform.position,transform.rotation).GetComponent<Rigidbody>().AddForce(transform.forward * 200,ForceMode.Impulse);
                    else
                        Debug.Log("CANON BARREL DAMAGED WEAPON FIRE NOT POSIBLE");
                    tank.AddExplosionForce(200f,transform.position,700f,1,ForceMode.Impulse);
                }else{
                    //Debug.Log("RELOADING");
                }
            }else{
                Debug.Log("CANON BREECH DESTROYED WEAPON FIRE NOT POSIBLE");
            }           
        }        
    }

    IEnumerator Reload(){
        yield return new WaitForSeconds(5);
        fire = false;
    }
}

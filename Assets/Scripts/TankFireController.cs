using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TankFireController : NetworkBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private Rigidbody tankbody;

    [SerializeField] private Animator fire_Animator;
    [SerializeField] private GameObject shell;

    [SerializeField] private ParticleSystem fireEffect;

    private int reloadTime;
    private bool fire;
    private bool cannonBreech = true;
    private bool barrel = true;
    public void SetLoader(bool state){
        if(state){
            reloadTime = 5;
        }else{
            reloadTime = 9;
        }
    }

    public void SetCannonBreech(bool state){
        cannonBreech = state;
    }

    public void SetCannonBarrel(bool state){
        barrel = state;
    }
    void Update()
    {
        if(!isLocalPlayer)
            return;
        if(Input.GetMouseButtonDown(0)){
            if(cannonBreech){
                    if(!fire){
                    fire = true;
                    Instantiate(fireEffect,firePos.position,firePos.rotation);
                    fire_Animator.SetBool("fire",true);
                    StartCoroutine(Reload());
                    if(barrel)
                        Instantiate(shell,firePos.position,firePos.rotation).GetComponent<Rigidbody>().AddForce(firePos.forward * 200,ForceMode.Impulse);
                    else
                        Debug.Log("CANON BARREL DAMAGED WEAPON FIRE NOT POSIBLE");
                    tankbody.AddExplosionForce(200f,firePos.position,700f,1,ForceMode.Impulse);
                }else{
                    Debug.Log("RELOADING");
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

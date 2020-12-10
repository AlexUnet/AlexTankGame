using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    
    public static int crew;
    public static bool fire;
    public static bool death;


    void Awake(){
        crew = 5;
    }
    public void Impact(string partName,int damage){
        Debug.LogError("DAMAGE IN:" + partName);
        switch (partName)
        {
            case "Commander":
                break;
            case "Gunner":
                break;
            case "Loader":
                break;
            case "Driver":
                break;
            case "MachineGunner":
                break;
            case "HorizontalAiming":
                break;
            case "CanonBreech":
                break;
            case "Transmission":
                break;
            case "Engine":
                KillEngine();               
                break;
            case "FuelTank":
                StartFire();            
                break;
            case "Barrel":
                break;
            case "Ammo":
                StartCoroutine(JackInWaitTime());
                break;
            case "Radiator":
                break;
            default:
                Debug.Log("wtf nigga?");
                break;
        }
        
    }

    public void StartFire(){
        fire = true;
        StartCoroutine(AmmoCookingTime());
    }

    public void Death(){
        //KILL THE TANK
    }

    #region Ammo Animation
    
    [SerializeField]GameObject jackInEffect;
    [SerializeField]GameObject AmmoExplotionDeath;
    [SerializeField]Rigidbody turret;

    public void AmmoExplotionAnimation(){
        transform.DetachChildren();
        turret.constraints = RigidbodyConstraints.None;
        Instantiate(AmmoExplotionDeath,transform);        
        turret.AddExplosionForce(Random.Range(3000f,4500f),transform.position,1000f,1,ForceMode.Impulse);
    }

    IEnumerator JackInWaitTime(){
        Instantiate(jackInEffect,transform);
        yield return new WaitForSeconds(1.5f);
        AmmoExplotionAnimation();
    }

    #endregion

    #region FuelTank Animation

    [SerializeField]GameObject fireEffect;
    [SerializeField]Transform engine;
    
    void FuelTankFireAnimation(){

    }

    IEnumerator AmmoCookingTime(){
        Instantiate(fireEffect,engine);
        yield return new WaitForSeconds(15);
        if(fire){
            StartCoroutine(JackInWaitTime());
        }
    }
    #endregion

    #region Death Engine Animation

    public void KillEngine(){
        this.gameObject.GetComponent<SimpleCarController>().SetEngine(false);
    }

    #endregion

    
}

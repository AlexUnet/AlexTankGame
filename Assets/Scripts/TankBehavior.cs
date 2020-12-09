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
                break;
            case "FuelTank":
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

    public void Death(){
        //KILL THE TANK
    }

    #region Ammo Animation
    
    [SerializeField]GameObject jackInEffect;
    [SerializeField]GameObject AmmoExplotionDeath;
    [SerializeField]Rigidbody turret;

    public void AmmoExplotionAnimation(){
        transform.DetachChildren();
        Debug.Log("OBJECT name: "+ turret.gameObject.name);
        Instantiate(AmmoExplotionDeath,transform);
        turret.AddExplosionForce(3000f,transform.position,1000f,1,ForceMode.Impulse);
    }

    IEnumerator JackInWaitTime(){
        Instantiate(jackInEffect,transform);
        yield return new WaitForSeconds(2);
        AmmoExplotionAnimation();
    }

    #endregion

    
}

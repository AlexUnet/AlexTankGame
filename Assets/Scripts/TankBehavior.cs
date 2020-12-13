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
        commander = gunner = loader = machineGunner = driver = true;
    }
    public void Impact(string partName,int damage){
        Debug.LogError("DAMAGE IN:" + partName);
        switch (partName)
        {
            case "Commander":
                SetCommander(false,-1);
                break;
            case "Gunner":
                SetGunner(false,-1);
                break;
            case "Loader":
                SetLoader(false,-1);
                break;
            case "Driver":
                SetDriver(false,-1);
                break;
            case "MachineGunner":
                SetMachineGunner(false,-1);
                break;
            case "HorizontalAiming":
                KillHorizontalAiming();
                break;
            case "CanonBreech":
                KillCanonBreech();
                break;
            case "Transmission":
                KillTransmission();
                break;
            case "Engine":
                KillEngine();               
                break;
            case "FuelTank":
                StartFire();            
                break;
            case "Barrel":
                KillBarrel();
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

    #region Death Ammo 
    
    [SerializeField]GameObject jackInEffect;
    [SerializeField]GameObject AmmoExplotionDeath;
    [SerializeField]GameObject turret;

    public void AmmoExplotionAnimation(){
        Rigidbody turretRB = turret.AddComponent<Rigidbody>();
        transform.DetachChildren();
        turretRB.mass = 200;
        Instantiate(AmmoExplotionDeath,transform);        
        turretRB.AddExplosionForce(Random.Range(3000f,4500f),transform.position,1000f,1,ForceMode.Impulse);
    }

    IEnumerator JackInWaitTime(){
        Instantiate(jackInEffect,transform);
        yield return new WaitForSeconds(1.5f);
        AmmoExplotionAnimation();
    }

    #endregion

    #region Death FuelTank 

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

    #region Death Engine 

    public void KillEngine(){
        this.gameObject.GetComponent<SimpleCarController>().SetEngine(false);
    }

    #endregion

    #region Death Horizontal Aiming
    public void KillHorizontalAiming(){
        this.gameObject.GetComponentInChildren<SimpleTankTurretMovement>().SetHorizontalAiming(false);
    }

    #endregion

    #region Crew

    bool commander,gunner,loader,machineGunner,driver;

    public void CheckCrew(){
        if(crew == 0){
            Death();
        }
    }

    public void SetCommander(bool state,int counter){
        crew += counter; // -1 disminuye(impacto) +1 aumenta(reparación)
        commander = state;
    }

    public void SetGunner(bool state,int counter){
        crew += counter;
        gunner = state;
    }

    public void SetLoader(bool state,int counter){
        crew += counter;
        loader = state;
    }

    public void SetMachineGunner(bool state,int counter){
        crew += counter;
        machineGunner = state;
    }

    public void SetDriver(bool state,int counter){
        crew += counter;
        driver = state;
    }

    #endregion

    #region Canon Breech

    public void KillCanonBreech(){
        this.gameObject.GetComponentInChildren<TankFireController>().SetCannonBreech(false);
    }
    #endregion

    #region Transmission

    public void KillTransmission(){
        this.gameObject.GetComponentInChildren<SimpleCarController>().SetTransmission(false);
    }
    #endregion

    #region Barrel

    public void KillBarrel(){
        this.gameObject.GetComponentInChildren<TankFireController>().SetCannonBarrel(false);
    }
    #endregion

    
}

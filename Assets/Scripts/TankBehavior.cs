using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    public const int partCount = 18;
    int crew;
    bool fire;
    bool death;

    int[] parts = new int[partCount];// FALTAN LAS RUEDAS +4 
    
    void Awake(){
        crew = 5;
        commander = gunner = loader = machineGunner = driver = true;
        for(int i = 0;i < partCount; i++){
            parts[i] = 3;
        }
    }
    public void Impact(string partName,int damage, string damagerName){

        //Debug.LogError("DAMAGE IN:" + partName + " BY: " + damagerName);
        switch (partName)
        {
            case "Commander":
            if(DamagePart(damage,0))
                SetCommander(false,-1);
                break;
            case "Gunner":
            if(DamagePart(damage,1))
                SetGunner(false,-1);
                break;
            case "Loader":
            if(DamagePart(damage,2))
                SetLoader(false,-1);
                break;
            case "Driver":
            if(DamagePart(damage,3))
                SetDriver(false,-1);
                break;
            case "MachineGunner":
            if(DamagePart(damage,4))
                SetMachineGunner(false,-1);
                break;
            case "HorizontalAiming":
            if(DamagePart(damage,5))
                KillHorizontalAiming();
                break;
            case "CanonBreech":
            if(DamagePart(damage,6))
                KillCanonBreech();
                break;
            case "Transmission":
            if(DamagePart(damage,7))
                KillTransmission();
                break;
            case "Engine":
            if(DamagePart(damage,8))
                KillEngine();               
                break;
            case "FuelTankR":
            if(DamagePart(damage,9))
                StartFire();            
                break;
            case "FuelTankL":
            if(DamagePart(damage,10))
                StartFire();            
                break;
            case "Barrel":
            if(DamagePart(damage,11))
                KillBarrel();
                break;
            case "AmmoR":
            if(DamagePart(damage,12))
                StartCoroutine(JackInWaitTime());
                break;
            case "AmmoL":
            if(DamagePart(damage,13))
                StartCoroutine(JackInWaitTime());
                break;
            case "Radiator":
            if(DamagePart(damage,14))
                Debug.Log("OH NO RADITOR DEATH");
                break;
            default:
                Debug.Log("wtf nigga?");
                break;
        }
    }

    public bool DamagePart(int damage, int part){
        parts[part] -= damage;
        Debug.Log("la Parte: N#" + part + " recibió: " + damage+ " le quedan " + parts[part]);
        if(parts[part] <= 0)
            return true; //la parte está muerta
        return false; // todavía tiene vida
    }

    public void UpdateState(){
        
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
        yield return new WaitForSeconds(Random.Range(0.5f,3.0f));
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

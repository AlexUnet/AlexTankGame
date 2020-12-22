using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    const int partCount = 18;
    private int crew;
    private bool fire;
    private bool death;

    private GameObject buffer;

    [SerializeField]private PartBehavior[] partsBehavior = new PartBehavior[partCount];

    private int[] parts = new int[partCount];// FALTAN LAS RUEDAS +4 
    
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
                StartCoroutine(WaitForReorganizeTime());
                break;
            case "Loader":
            if(DamagePart(damage,2))
                SetLoader(false,-1);
                break;
            case "Driver":
            if(DamagePart(damage,3))
                SetDriver(false,-1);
                StartCoroutine(WaitForReorganizeTime());
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
                KillRadiator();                
                break;
            default:
                Debug.Log("wtf nigga?");
                break;
        }
        CheckCrew();        
    }

    public bool DamagePart(int damage, int part){
        if(parts[part] > 0){
            parts[part] -= damage;
            partsBehavior[part].Setvitality(parts[part]);
            Debug.Log("la Parte: N#" + part + " recibió damage: " + damage+ " le quedan " + parts[part]);
            if(parts[part] <= 0)
                return true; //la parte está muerta
        }        
        return false; // la parte no se dañó porque posiblemente esté muerta
    }

    
    public void Death(){
        this.GetComponent<SimpleCarController>().enabled = false;
        this.GetComponent<SpecialActionController>().enabled = false;
        this.GetComponentInChildren<SimpleTankTurretMovement>().enabled = false;
        this.GetComponentInChildren<SimpleCanonController>().enabled = false;
        this.GetComponentInChildren<TankFireController>().enabled = false;
        //KILL THE TANK
    }

    #region Fire Behavior
    public void StartFire(){
        if(!fire){
            fire = true;
            StartCoroutine(AmmoCookingTime());
        }        
    }

    public void StartFireEffect(){
        buffer = Instantiate(fireEffect,engine);
    }

    public void StopFire(){
        if(fire)
            StartCoroutine(ExtinguishFire());
    }

    IEnumerator ExtinguishFire(){
        yield return new WaitForSeconds(3);
        Destroy(buffer);
        fire = false;
    }
    #endregion

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
        Death();
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

    IEnumerator AmmoCookingTime(){
        StartFireEffect();
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
        Debug.Log("REMAINIG CREW " + crew);
        if(crew < 2){
            Death();
        }
    }
    IEnumerator WaitForReorganizeTime(){
        yield return new WaitForSeconds(2);
        ReorganizeCrew();
    }

    public void SetCommander(bool state,int counter){
        crew += counter; // -1 disminuye(impacto) +1 aumenta(reparación)
        commander = state;
    }

    public void SetGunner(bool state,int counter){
        this.gameObject.GetComponentInChildren<SimpleCanonController>().SetGunner(state);
        crew += counter;
        gunner = state;
    }

    public void SetLoader(bool state,int counter){
        this.gameObject.GetComponentInChildren<TankFireController>().SetLoader(state);
        crew += counter;
        loader = state;
    }

    public void SetMachineGunner(bool state,int counter){
        crew += counter;
        machineGunner = state;
    }

    public void SetDriver(bool state,int counter){
        this.gameObject.GetComponent<SimpleCarController>().SetDriver(state);
        crew += counter;
        driver = state;
    }

    public void ReorganizeCrew(){
        if(crew < 2){
            Debug.Log("no es posible reorganizar solo queda 1 tripulante");
            return;
        }            
        if(!driver){
            if(machineGunner){
                Debug.Log("MACHINE GUNNER POR DRIVER");
                ExchangeCrewMembersArray(3,4);
                SetMachineGunner(false,0);
                SetDriver(true,0);
            }else if(commander){
                Debug.Log("COMMANDER POR DRIVER");
                ExchangeCrewMembersArray(3,0);
                SetCommander(false,0);
                SetDriver(true,0);
            }else if(loader){
                ExchangeCrewMembersArray(3,2);
                Debug.Log("LOADER POR DRIVER");
                SetLoader(false,0);
                SetDriver(true,0);
            }
        }
        if(!gunner){
            if(machineGunner){
                Debug.Log("MACHINE GUNNER POR GUNNER");
                ExchangeCrewMembersArray(1,4);
                SetMachineGunner(false,0);
                SetGunner(true,0);
            }else if(commander){
                Debug.Log("COMMANDER POR GUNNER");
                ExchangeCrewMembersArray(1,0);
                SetCommander(false,0);
                SetGunner(true,0);
            }else if(loader){
                Debug.Log("LOADER POR GUNNER");
                ExchangeCrewMembersArray(1,2);
                SetLoader(false,0);
                SetGunner(true,0);
            }
        }
    }

    public void ExchangeCrewMembersArray(int damaged,int replacement){
        parts[damaged] = parts[replacement];
        parts[replacement] = 0;
        partsBehavior[damaged].Setvitality(parts[damaged]);
        partsBehavior[replacement].Setvitality(parts[replacement]);
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

    #region Radiator

    public void KillRadiator(){
        this.gameObject.GetComponentInChildren<SimpleCarController>().SetRadiator(false);
    }

    #endregion

    
}

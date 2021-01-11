using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SimpleTankTurretMovement : NetworkBehaviour
{
    [SerializeField] private Transform Turret;
    public bool horizontalAiming; 
    void Awake(){
        horizontalAiming = true;
    }
    [SerializeField]Transform playerPointView;
    void Update () {
        if(!isLocalPlayer)
            return;
        TurretRotation(Input.mousePosition,playerPointView);
        if(horizontalAiming){
            TurretRotation(Input.mousePosition,Turret);                     
        }else{
            Debug.Log("HORIZONTAL AIMING DESTROYED TURRET MOVEMENT NOT POSIBLE IN" + this.gameObject.name);
        }
    }

    public void TurretRotation(Vector3 localTargetPos,Transform objetive){
        if(localTargetPos.x > 600){
            objetive.Rotate(0,Time.deltaTime * 13,0);                               
        }else if (localTargetPos.x < 400){
            objetive.Rotate(0,Time.deltaTime * -13,0);
        }
    }
    public void SetHorizontalAiming(bool state){
        horizontalAiming = state;
    }
}

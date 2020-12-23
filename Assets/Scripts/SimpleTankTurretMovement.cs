using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTankTurretMovement : MonoBehaviour
{
    public bool horizontalAiming;
    [SerializeField]private Transform parent;
    public float range = Mathf.Infinity;
    [SerializeField] private Camera mainCamera;    

    void Awake(){
        horizontalAiming = true;
    }
    [SerializeField]Transform testCube;
    [SerializeField]Transform playerPointView;
    void Update () {

        //Debug.Log(Input.mousePosition.x);

        TurretRotation(Input.mousePosition,playerPointView);
        if(horizontalAiming){
            TurretRotation(Input.mousePosition,transform);                     
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

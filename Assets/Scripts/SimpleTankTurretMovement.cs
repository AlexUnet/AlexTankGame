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

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mainCamera.transform.position,ray.direction * range,Color.green);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
             
        Vector3 localTargetPos = transform.InverseTransformPoint(hit.point);
        Vector3 localCameratPos = playerPointView.InverseTransformPoint(hit.point);
        localTargetPos.y = 0.0f;

        TurretRotation(localCameratPos,playerPointView);
        if(horizontalAiming){
            TurretRotation(localTargetPos,transform);                     
        }else{
            Debug.Log("HORIZONTAL AIMING DESTROYED TURRET MOVEMENT NOT POSIBLE IN" + this.gameObject.name);
        }
    }

    public void TurretRotation(Vector3 localTargetPos,Transform objetive){
        if(localTargetPos.x > 2){
            objetive.Rotate(0,Time.deltaTime * 13,0);                               
        }else if (localTargetPos.x < -2){
            objetive.Rotate(0,Time.deltaTime * -13,0);
        }
    }
    public void SetHorizontalAiming(bool state){
        horizontalAiming = state;
    }
}

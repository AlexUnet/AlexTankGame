using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialActionController : MonoBehaviour
{
    [SerializeField]TankBehavior tankBehavior;
    [SerializeField]Camera thirdPersonCamera;
    [SerializeField]Camera firstPersonCamera;
    [SerializeField]Canvas scopeCanvas;
    
    int current; int second;
    bool state;
    bool zoom;

    void Awake(){

    }
    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            if(!state){
                state = true;
                current = 1; second = 0;
            }else{
                current = 0; second = 1;
                state = false;
            }
            scopeCanvas.enabled = state;
            thirdPersonCamera.targetDisplay = current;
            firstPersonCamera.targetDisplay = second;
            firstPersonCamera.fieldOfView = 27;
            zoom = false;
        }

        if(Input.GetMouseButtonDown(1)){
            if(!zoom){
                zoom = true;
                firstPersonCamera.fieldOfView = 17;
            }else{
                zoom = false;
                firstPersonCamera.fieldOfView = 27;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha6)){
            tankBehavior.StopFire();
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            tankBehavior.StartFire();
        }
    }
}

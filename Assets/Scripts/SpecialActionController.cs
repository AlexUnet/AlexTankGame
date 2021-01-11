using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using UnityEngine;

public class SpecialActionController : NetworkBehaviour
{
    [SerializeField]TankBehavior tankBehavior;
    [SerializeField]Camera thirdPersonCamera;
    [SerializeField]Camera firstPersonCamera;
    [SerializeField]CinemachineVirtualCamera vCamera;
      
    [SerializeField]GameObject scopeObject;

    [SerializeField]SimpleCanonController canonLayerControl;
    
    int current; int second;
    bool state;
    bool zoom;

    void Awake(){
        tankBehavior = GetComponent<TankBehavior>();
    }
    void Start(){
        if(isLocalPlayer){
            thirdPersonCamera.gameObject.SetActive(true);
            firstPersonCamera.gameObject.SetActive(true);
            vCamera.gameObject.SetActive(true);
        }
        else{
            thirdPersonCamera.gameObject.SetActive(false);
            firstPersonCamera.gameObject.SetActive(false);
            vCamera.gameObject.SetActive(false);
        }
    }
    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            if(!state){//mira
                state = true;
                current = 1; second = 0;
            }else{//tercera persona
                current = 0; second = 1;
                state = false;
            }
            canonLayerControl.SetView(state);
            //scopeCanvas.enabled = state;
            scopeObject.SetActive(state);
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
        if(Input.GetKeyDown(KeyCode.R)){
            tankBehavior.CheckDeathParts();            
        }
        if(Input.GetKeyDown(KeyCode.E)){

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTankTurretMovement : MonoBehaviour
{
    public bool horizontalAiming;
    public Transform turretY;
    [SerializeField]private Transform parent;
    public float range = 1000f;
    [SerializeField] private Camera mainCamera;
    

    void Awake(){
        horizontalAiming = true;
        
    }
    void Update () {

        if(horizontalAiming){
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(mainCamera.transform.position,ray.direction * range,Color.green);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            //
            Vector3 localTargetPos = transform.InverseTransformPoint(hit.point);
            localTargetPos.y = 0.0f;

            Quaternion rotationGoal = Quaternion.LookRotation(localTargetPos);
            Quaternion newRotation = Quaternion.RotateTowards(turretY.localRotation, rotationGoal, 30.0f * Time.deltaTime);
            turretY.localRotation = newRotation;
            //
            /*
            Vector3 dir = hit.point - transform.position;        
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;*/
            //turretY.LookAt(hit.point);
            //turretY.localRotation = Quaternion.Euler(0, rotation.y, 0);
        }else{
            Debug.Log("HORIZONTAL AIMING DESTROYED TURRET MOVEMENT NOT POSIBLE IN" + this.gameObject.name);
        }
    }
    public void SetHorizontalAiming(bool state){
        horizontalAiming = state;
    }

    private void RotateBase(Vector3 aimPoint)
      {
         // TODO: Turret needs to rotate the long way around if the aimpoint gets behind
         // it and traversal limits prevent it from taking the shortest rotation.
         if (turretY != null)
         {
            // Note, the local conversion has to come from the parent.
            Vector3 localTargetPos = transform.InverseTransformPoint(aimPoint);
            localTargetPos.y = 0.0f;

            // Clamp target rotation by creating a limited rotation to the target.
            // Use different clamps depending if the target is to the left or right of the turret.
            Vector3 clampedLocalVec2Target = localTargetPos;
            /*if (limitTraverse)
            {
               if (localTargetPos.x >= 0.0f)
                  clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * rightTraverse, float.MaxValue);
               else
                  clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * leftTraverse, float.MaxValue);
            }
            */
            // Create new rotation towards the target in local space.
            Quaternion rotationGoal = Quaternion.LookRotation(clampedLocalVec2Target);
            Quaternion newRotation = Quaternion.RotateTowards(turretY.localRotation, rotationGoal, 30.0f * Time.deltaTime);

            // Set the new rotation of the base.
            turretY.localRotation = newRotation;
         }
      }
}

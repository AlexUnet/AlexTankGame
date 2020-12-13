using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCanonController : MonoBehaviour
{
    [SerializeField]Camera main;

    public GameObject testPoint;
    void LateUpdate(){
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit,9999);
        //Debug.DrawRay(transform.position,ray.direction * 9999,Color.green);

        testPoint.transform.position = hit.point;

        Vector3 dir = hit.point - transform.position;        
        Quaternion lookRotation = Quaternion.LookRotation(dir,transform.up);
        Vector3 rotation1 = lookRotation.eulerAngles;
        transform.LookAt(hit.point);
        //Debug.Log(transform.localEulerAngles); 
        transform.localRotation = Quaternion.Euler(TransformAngleX(transform.localEulerAngles.x),TransformAngleX(transform.localEulerAngles.y),0);
        //transform.localRotation = Quaternion.Euler(TransformAngleX(rotation1.x),TransformAngleX(rotation1.y),0);
    }

    float TransformAngleX(float angle){
        if(angle < 15){
            return angle;
        }
        else if(angle > 15 && angle < 345){            
            var a = Mathf.Abs(angle -345);
            var b = Mathf.Abs(angle -15);
            return (a <= b)?345:15;
        }else{
            return angle;
        }
    }
}

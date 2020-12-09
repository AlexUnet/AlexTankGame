using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCanonController : MonoBehaviour
{
    void LateUpdate(){
    }

    void Update(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        Debug.DrawRay(transform.position,ray.direction * 9999,Color.green);

        transform.LookAt(hit.point);

        transform.localRotation = new Quaternion(
            Mathf.Clamp(transform.localRotation.x,-0.25f,0.10f),
            Mathf.Clamp(transform.localRotation.y,-0.25f,0.25f),
            transform.localRotation.z,1);
    }
}

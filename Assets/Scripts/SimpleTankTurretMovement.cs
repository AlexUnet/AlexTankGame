using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTankTurretMovement : MonoBehaviour
{
    public Transform TurretY;
    public float range = 1000f;

// Update is called once per frame
    void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position,ray.direction * range,Color.green);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        Vector3 dir = hit.point - transform.position;        
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        TurretY.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}

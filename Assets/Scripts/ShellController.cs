using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public Rigidbody cuerpo;
    public Ray HitDetecter;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float angle;

        if(Physics.Raycast(transform.position,transform.forward,out hit,2f)){
            angle = (-90 + Vector3.Angle(transform.forward,hit.normal));
            if(angle < 49.00){
                this.GetComponentInParent<MeshCollider>().isTrigger = false;
                Debug.LogError("RICOCHETT " + "WAY TO HIT: " + hit.collider.gameObject.name + "ANGLE: " + angle);
            }else{
                Debug.LogAssertion("HIT " + "WAY TO HIT: " + hit.collider.gameObject.name + "ANGLE: " + angle);
            }
            Destroy(this.gameObject);
        }
        
        Debug.DrawRay(transform.position,transform.forward,Color.red);
        Debug.DrawRay(transform.position,-transform.forward,Color.blue);
    }
}

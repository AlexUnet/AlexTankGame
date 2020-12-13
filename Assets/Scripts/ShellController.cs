using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{

    public Rigidbody cuerpo;
    public Ray HitDetecter;

    [SerializeField]SphereCollider explotion;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float angle;
        int layerMask = 1 << 8; // el rayo detecta
        layerMask = ~layerMask; // todas menos las partes internas

        if(Physics.Raycast(transform.position,transform.forward,out hit,3f,layerMask)){
            angle = (-90 + Vector3.Angle(transform.forward,hit.normal));
            if(angle < 49.00){
                this.GetComponentInParent<MeshCollider>().isTrigger = false;
                ShellShotBehavior.active = false;
                Debug.LogError("RICOCHETT " + "way to hit: " + hit.collider.gameObject.name + " angle: " + angle);
            }else{
                ShellShotBehavior.active = true;
                Debug.Log(new Vector3(transform.position.x,transform.position.y,transform.position.z));
                Debug.Log(new Vector3(transform.rotation.x,transform.rotation.y,transform.rotation.z));              
                Debug.LogAssertion("HIT " + "way to hit: " + hit.collider.gameObject.name + " angle: " + angle);
                explotion.enabled = true;
            }
            Destroy(this.gameObject);
        }
        //Debug.DrawRay(transform.position,transform.forward,Color.red);
        //Debug.DrawRay(transform.position,-transform.forward,Color.blue);
    }
}

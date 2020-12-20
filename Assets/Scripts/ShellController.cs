using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    //collider de la bala para que atraviese
    [SerializeField]public MeshCollider body;
    //distancia a la que se mide si rebota o no
    [SerializeField] float detectionDistance;
    //RB de prueba para bajar la velocidad y facilitar la detección
    [SerializeField]Rigidbody bodyRB;

    RaycastHit hit;
    float angle;
    int layerMask = 1 << 8;
    void Awake(){
        // el rayo detecta todas menos las partes internas
        layerMask = ~layerMask; 
    }
    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position,transform.forward,out hit,detectionDistance,layerMask)){
            angle = (-90 + Vector3.Angle(transform.forward,hit.normal));
            if(angle < 49.00){
                Debug.LogError("RICOCHETT " + "way to hit: " + hit.collider.gameObject.name + " angle: " + angle);
                GetComponentInParent<ShellImpactController>().SetActive(false);
            }else{
                body.isTrigger = true;
                bodyRB.velocity = bodyRB.velocity / 2.8f;         
                Debug.LogAssertionFormat("HIT " + "way to hit: " + hit.collider.gameObject.name + " angle: " + angle);
            }
            Destroy(this.gameObject);
        }
    }
}

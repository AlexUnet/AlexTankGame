using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTailController : MonoBehaviour
{
    public void SetTailPosition(){
        transform.parent = null;
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy(){
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour {
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("파괴한다! : " + collision.gameObject.name);
        Destroy(collision.transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("파괴한다! : " + other.gameObject.name);
        Destroy(other.transform.parent.gameObject);
    }
}

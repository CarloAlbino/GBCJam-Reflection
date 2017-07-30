using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollider : MonoBehaviour {

    void Start()
    {
        Destroy(this.gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crush")
        {
            Destroy(this.gameObject);
        }
    }
}

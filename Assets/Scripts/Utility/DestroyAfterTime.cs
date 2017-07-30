using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float m_timeToDestruction = 10;

	void Start ()
    {
        Destroy(this.gameObject, m_timeToDestruction);
	}

}

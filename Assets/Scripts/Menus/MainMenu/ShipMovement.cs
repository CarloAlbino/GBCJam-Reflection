using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    public float count = 2;
    private float curCount = 0;
    public float speed = 1;

	void Update ()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        transform.Rotate(new Vector3(1, 1, 0), Mathf.Abs(speed) * 3 * Time.deltaTime);
        if(curCount < count)
        {
            curCount += Time.deltaTime;
        }
        else
        {
            curCount = 0;
            speed *= -1;
        }
	}
}

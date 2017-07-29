using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private int m_playerNum = 1;
    [SerializeField]
    private float m_speed = 5.0f;
    [SerializeField]
    private float m_boostSpeed = 10.0f;
    [SerializeField]
    private Transform m_mesh;

    string m_lh, m_lv, m_rh, m_rv;

	void Start ()
    {
        m_lh = "LH_" + m_playerNum.ToString();
        m_lv = "LV_" + m_playerNum.ToString();
        m_rh = "RH_" + m_playerNum.ToString();
        m_rv = "RV_" + m_playerNum.ToString();
    }
	
	void Update ()
    {
        Movement();
	}

    private void Movement()
    {
        float leftHorizontalInput = InputManager.Instance.GetAxis(m_lh);
        float leftVerticalInput = InputManager.Instance.GetAxis(m_lv);

        float rightHorizontalInput = InputManager.Instance.GetAxis(m_rh);
        float rightVerticalInput = InputManager.Instance.GetAxis(m_rv);

        Vector2 movementDirection = new Vector2(leftHorizontalInput, -leftVerticalInput);
        transform.Translate(movementDirection * m_speed * Time.deltaTime);

        if (rightHorizontalInput != 0 && rightVerticalInput != 0)
        {
            //float angle = Mathf.Atan2(rightVerticalInput, -rightHorizontalInput) * Mathf.Rad2Deg;
            //m_mesh.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //m_mesh.rotation = Quaternion.LookRotation(new Vector3(0, rightHorizontalInput, rightVerticalInput));
            //m_mesh.eulerAngles = new Vector3(m_mesh.eulerAngles.x, 90, 90);

            m_mesh.LookAt(new Vector3(m_mesh.transform.position.x + rightHorizontalInput, m_mesh.transform.position.y + -rightVerticalInput, m_mesh.transform.position.z));
        }
    }
}

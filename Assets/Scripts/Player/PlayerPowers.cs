using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour {

    [SerializeField]
    private float m_fireRate = 0.2f;
    private float m_fireCount = 0.0f;

    [SerializeField]
    private GameObject m_projectile;
    [SerializeField]
    private Transform m_projectileSpawn;

    [SerializeField]
    private int m_playerNum;

	void Start () {
		
	}

	void Update ()
    {
        Shoot();
	}

    private void Shoot()
    {
        if(m_fireCount <= 0)
        {
            if(InputManager.Instance.GetAxis("RTrigger_" + m_playerNum.ToString()) > 0 || InputManager.Instance.GetButton("RBumper_" + m_playerNum.ToString()))
            {
                GameObject p = Instantiate(m_projectile, m_projectileSpawn.position + m_projectileSpawn.forward, m_projectileSpawn.rotation) as GameObject;
                p.GetComponent<Projectile>().projectileTag = m_playerNum.ToString();
                p.GetComponent<Projectile>().Launch();
                m_fireCount = m_fireRate;
            }
        }
        else
        {
            m_fireCount -= Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    [SerializeField]
    private int m_playerNum;

    [SerializeField]
    private Transform m_crushHidingSpot;

    [SerializeField]
    private Transform m_respawnPosition;

    [SerializeField]
    private float m_respawnTime = 3.0f;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crush")
        {
            // Hit
            GameController.Instance.AddScore(-5, m_playerNum);
            transform.position = m_crushHidingSpot.position;
            StartCoroutine(RespawnCount());
        }

        if(other.tag == "Pickup")
        {
            // Pickup
            GameController.Instance.AddScore(1, m_playerNum);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator RespawnCount()
    {
        yield return new WaitForSeconds(m_respawnTime);

        transform.position = m_respawnPosition.position;
    }
}

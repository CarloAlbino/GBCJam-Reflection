using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    [SerializeField]
    private int m_playerNum;

    [SerializeField]
    private float m_respawnTime = 3.0f;

    [SerializeField]
    private GameObject m_deathParticles;
    [SerializeField]
    private GameObject m_lastParticles;

    private PlayerAudio m_audio;

    void Start()
    {
        m_audio = GetComponent<PlayerAudio>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crush")
        {
            if (GameController.Instance.remainingTime <= 0)
                return;

            // Hit
            m_audio.PlayEffect(1);
            GameController.Instance.AddScore(-5, m_playerNum);
            m_lastParticles = Instantiate(m_deathParticles, transform.position, transform.rotation) as GameObject;
            transform.position = GameController.Instance.hidePosition.position;
            StartCoroutine(RespawnCount());
        }

        if(other.tag == "Pickup")
        {
            // Pickup
            m_audio.PlayEffect(0);
            GameController.Instance.AddScore(1, m_playerNum);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator RespawnCount()
    {
        yield return new WaitForSeconds(m_respawnTime);

        if(m_lastParticles != null)
            Destroy(m_lastParticles);
        m_audio.PlayEffect(2);
        transform.position = GameController.Instance.GetSpawnPos(m_playerNum).position;
    }
}

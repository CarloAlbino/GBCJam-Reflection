using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTriggerMessage : MonoBehaviour {

    private LevelController m_levelController;
    public AudioSource m_audio;

    void Start()
    {
        m_levelController = FindObjectOfType<LevelController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Folding")
        {
            m_levelController.SignalClosing();
        }

        if (other.tag == "Open")
        {
            m_levelController.SignalOpen();
        }

        if (other.tag == "RemoveBack")
        {
            m_levelController.SignalRemove();
        }

        if(other.tag == "Crushing")
        {
            m_levelController.SignalCrush();
        }

        if(other.tag == "Close")
        {
            m_levelController.SignalClose();
            m_audio.Play();
        }
    }
}

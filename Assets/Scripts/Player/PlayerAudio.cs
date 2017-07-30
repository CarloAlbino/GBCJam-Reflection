using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    public AudioSource m_playerShot;
    public AudioSource m_playerEffect;

    public AudioClip[] m_effects;

    public void Shoot(bool isShooting)
    {
        if (isShooting)
            m_playerShot.Play();
        else
            m_playerShot.Stop();
    }

    public void PlayEffect(int num)
    {
        m_playerEffect.clip = m_effects[num];
        m_playerEffect.Play();
    }
}

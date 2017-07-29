using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScreen : MonoBehaviour {

    [SerializeField]
    private Text[] m_playerText;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < m_playerText.Length; i++)
        {
            if(GameController.Instance.PlayerActive(i + 1))
            {
                m_playerText[i].text = "Press Start when players are ready.";
            }
            else
            {
                m_playerText[i].text = "Player " + (i+1) + "\nPress A to Join";
            }
        }
	}
}

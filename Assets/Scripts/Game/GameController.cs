using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController> {

    private bool[] m_activePlayers = new bool[4];
    [SerializeField]
    private int[] m_score = new int[4];

    [SerializeField]
    private Transform[] m_spawnPositions;
    [SerializeField]
    private Transform m_hidePosition;
    public Transform hidePosition {get { return m_hidePosition; } }

    private bool m_isGame = false;

    [SerializeField]
    private int m_gameScene = 2;

	void Start ()
    {
		for(int i = 0; i < m_activePlayers.Length; i++)
        {
            m_activePlayers[i] = false;
            m_score[i] = 0;
        }
	}

	void Update ()
    {
        if (!m_isGame)
        {
            CharacterSelectControls();
        }
	}

    private void CharacterSelectControls()
    {
        for (int i = 1; i < 5; i++)
        {
            if (InputManager.Instance.GetButtonDown("A_" + i))
            {
                m_activePlayers[i - 1] = true;
            }

            if (InputManager.Instance.GetButtonDown("B_" + i))
            {
                m_activePlayers[i - 1] = false;
            }

            if (InputManager.Instance.GetButtonDown("Pause_" + i))
            {
                // Start game
                m_isGame = true;
                SceneManager.LoadScene(m_gameScene);
            }
        }
    }

    public void AddScore(int score, int player)
    {
        m_score[player - 1] += score;
    }

    public int GetScore(int player)
    {
        return m_score[player - 1];
    }

    public void RestartGame()
    {
        for (int i = 0; i < m_activePlayers.Length; i++)
        {
            m_score[i] = 0;
        }
    }

    public void ToMainMenu()
    {
        for (int i = 0; i < m_activePlayers.Length; i++)
        {
            m_activePlayers[i] = false;
            m_score[i] = 0;
        }
        m_isGame = false;
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    public Transform GetSpawnPos(int playerNum)
    {
        return m_spawnPositions[playerNum - 1];
    }
}

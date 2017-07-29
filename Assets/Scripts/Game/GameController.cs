using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField]
    private Image m_quitTimer;
    private float m_quitTime = 3;
    private float m_quitCount = 0;

	void Start ()
    {
		for(int i = 0; i < m_activePlayers.Length; i++)
        {
            m_activePlayers[i] = false;
            m_score[i] = 0;
        }

        m_quitTimer = GameObject.Find("TimerImage").GetComponent<Image>();
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
                if (m_activePlayers[i-1] == true)
                {
                    // Start game
                    m_isGame = true;
                    SceneManager.LoadScene(m_gameScene);
                }
            }
        }

        if (InputManager.Instance.GetButton("B_1"))
        {
            Debug.Log("Pressed B");
            m_quitCount += Time.deltaTime;
            m_quitTimer.fillAmount += m_quitCount / m_quitTime;
            if (m_quitCount > m_quitTime)
                ToMainMenu();
        }
        else if (InputManager.Instance.GetButton("B_1") == false)
        {
            m_quitCount = 0;
            m_quitTimer.fillAmount = 0;
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
        SceneManager.LoadScene(m_gameScene);
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

    public bool PlayerActive(int playerNum)
    {
        return m_activePlayers[playerNum - 1];
    }
}

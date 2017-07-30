using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Singleton<GameController> {

    [SerializeField]
    private GameObject[] m_playerPrefabs;
    [SerializeField]
    private bool[] m_activePlayers = new bool[4];
    [SerializeField]
    private int[] m_score = new int[4];

    [SerializeField]
    private Transform[] m_spawnPositions;
    [SerializeField]
    private Transform m_hidePosition;
    public Transform hidePosition {get { return m_hidePosition; } }

    [SerializeField]
    private bool m_isGame = false;

    [SerializeField]
    private int m_gameScene = 2;

    [SerializeField]
    private Image m_quitTimer;
    private float m_quitTime = 3;
    private float m_quitCount = 0;

    [SerializeField]
    private int m_gameTime = 90;
    public int gameTime { get { return m_gameTime; } }
    private int m_remainingTime = 90;
    public int remainingTime { get { return m_remainingTime; } }

	void Start ()
    {
		//for(int i = 0; i < m_activePlayers.Length; i++)
  //      {
  //          m_activePlayers[i] = false;
  //          m_score[i] = 0;
  //      }

        if(!m_isGame)
            m_quitTimer = GameObject.Find("TimerImage").GetComponent<Image>();
	}

	void Update ()
    {
        if (!m_isGame)
        {
            CharacterSelectControls();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToMainMenu();
            }
        }
	}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded");
        if(scene.buildIndex == m_gameScene)
        {
            Debug.Log("Scene game");
            m_isGame = true;

            for(int i = 0; i < 4; i++)
            {
                if(m_activePlayers[i] == true)
                {
                    Instantiate(m_playerPrefabs[i], m_spawnPositions[i].position, Quaternion.identity);
                    m_remainingTime = m_gameTime;
                }
            }

            StartCoroutine(CountDownTime());
        }
    }

    #region CharacterSelect
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
    #endregion

    #region Score
    public void AddScore(int score, int player)
    {
        if (remainingTime <= 0)
            return;

        m_score[player - 1] += score;
    }

    public int GetScore(int player)
    {
        return m_score[player - 1];
    }
    #endregion

    #region Time
    public IEnumerator CountDownTime()
    {
        yield return new WaitForSeconds(1);
        m_remainingTime--;
        if(m_remainingTime > 0)
            StartCoroutine(CountDownTime());
    }
    #endregion

    #region SceneControl
    public void RestartGame()
    {
        for (int i = 0; i < m_activePlayers.Length; i++)
        {
            m_score[i] = 0;
        }
        StopAllCoroutines();
        m_remainingTime = m_gameTime;
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
    #endregion

    #region GameControl
    public Transform GetSpawnPos(int playerNum)
    {
        return m_spawnPositions[playerNum - 1];
    }

    public bool PlayerActive(int playerNum)
    {
        return m_activePlayers[playerNum - 1];
    }
    #endregion
}

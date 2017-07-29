using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCanvasController : MonoBehaviour {

    [SerializeField]
    private Text[] m_scoreLabels;
    [SerializeField]
    private Text m_timeLabel;
    [SerializeField]
    private Image m_timeClock;

    [SerializeField]
    private GameObject m_gameOverText;

	void Update () {
		for(int i = 0; i < 4; i++)
        {
            m_scoreLabels[i].text = "Player " + (i + 1) + "\n" + GameController.Instance.GetScore(i + 1);
        }

        m_timeLabel.text = "Time: " + GameController.Instance.remainingTime + " sec";

        m_timeClock.fillAmount = (float)GameController.Instance.remainingTime / (float)GameController.Instance.gameTime;

        if (InputManager.Instance.GetButtonDown("A_" + 1))
        {
            GameController.Instance.RestartGame();
        }
        if (GameController.Instance.remainingTime <= 0)
        {
            m_gameOverText.SetActive(true);

            for(int i = 1; i < 5; i++)
            {
                if(InputManager.Instance.GetButtonDown("A_" + i))
                {
                    GameController.Instance.RestartGame();
                }

                if(InputManager.Instance.GetButtonDown("B_" + i))
                {
                    GameController.Instance.ToMainMenu();
                }
            }
        }
        else
        {
            m_gameOverText.SetActive(false);
        }
    }
}

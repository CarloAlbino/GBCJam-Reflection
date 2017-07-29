using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField]
    private float m_levelSpeed = 10;
    public float levelSpeed { get { return m_levelSpeed; } }
    [SerializeField]
    private float m_cameraSpeedFactor = 0.1f;

    [SerializeField]
    private MeshRenderer m_foldBackground;

    private int m_foldCount = 0;

    private CameraController m_cameraController;

    void Start () {
        m_cameraController = FindObjectOfType<CameraController>();
	}

	void Update () {
		
	}

    public void SignalClosing()
    {
        m_foldBackground.enabled = true;
        m_cameraController.MoveCamera(1, m_levelSpeed * m_cameraSpeedFactor, 15, true);
    }

    public void SignalRemove()
    {
        m_foldBackground.enabled = false;
    }

    public void SignalOpen()
    {
        m_foldCount++;
        m_levelSpeed += 5;
        m_cameraController.MoveCamera(0, m_levelSpeed * m_cameraSpeedFactor, 6, true);
    }
}

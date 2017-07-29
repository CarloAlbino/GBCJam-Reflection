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

    [SerializeField]
    private GameObject[] m_foldColliders;
    [SerializeField]
    private Transform m_foldPosition;
    private GameObject m_fold = null;
    private int m_lastFoldNum = -1;

    private int m_foldCount = 0;

    private CameraController m_cameraController;

    void Start () {
        m_cameraController = FindObjectOfType<CameraController>();
	}

	void Update () {
		
	}

    public void SignalClosing()
    {
        if (m_fold != null)
            Destroy(m_fold);

        int nextFold;
        do
        {
            nextFold = Random.Range(0, m_foldColliders.Length);
        } while (nextFold == m_lastFoldNum);
        m_lastFoldNum = nextFold;

        m_fold = Instantiate(m_foldColliders[nextFold], m_foldPosition) as GameObject;

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
        m_cameraController.MoveCamera(0, m_levelSpeed * m_cameraSpeedFactor, 6.7f, true);
    }

    public void SignalCrush()
    {
        Collider[] cols = m_fold.GetComponentsInChildren<Collider>();
        foreach (Collider c in cols)
            c.enabled = true;
    }
}

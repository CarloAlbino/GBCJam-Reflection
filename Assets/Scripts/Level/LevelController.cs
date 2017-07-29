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

    [SerializeField]
    private GameObject m_pickup;
    [SerializeField]
    private int m_numOfObjectsToSpawn = 20;
    [SerializeField]
    private Transform m_spawnVolume;

    private CameraController m_cameraController;

    void Start () {
        m_cameraController = FindObjectOfType<CameraController>();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            SignalRemove();
        }
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

        for (int i = 0; i < m_numOfObjectsToSpawn; i++)
        {
            Instantiate(m_pickup, new Vector3(Random.Range(m_spawnVolume.position.x - m_spawnVolume.localScale.x / 2, m_spawnVolume.position.x + m_spawnVolume.localScale.x / 2),
                Random.Range(m_spawnVolume.position.y - m_spawnVolume.localScale.y / 2, m_spawnVolume.position.y + m_spawnVolume.localScale.y / 2),
                m_spawnVolume.position.z), Quaternion.identity);
        }
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

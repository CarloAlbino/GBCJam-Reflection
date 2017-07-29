using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFolding : MonoBehaviour {

    [SerializeField]
    private Transform m_levelPivot;
    [SerializeField]
    private float m_rotateSpeed = 1.0f;

    private LevelController m_levelController;

    void Start()
    {
        m_levelController = FindObjectOfType<LevelController>();
    }

	void Update ()
    {
        m_levelPivot.Rotate(Vector3.right, -m_levelController.levelSpeed * Time.deltaTime);
	}
}

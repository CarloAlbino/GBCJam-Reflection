using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    [SerializeField]
    private float m_lifeTime = 1.2f;
    [SerializeField]
    private float m_speed = 5.0f;

    private string m_tag;
    public string projectileTag { get { return projectileTag; } set { m_tag = value; } }

    [SerializeField]
    private Rigidbody m_rb;
    private Coroutine m_destroyCoroutine = null;

	void Start ()
    {
        //m_rb = GetComponent<Rigidbody>();
        m_destroyCoroutine = StartCoroutine(DestroyAfterTime());
	}

    void OnCollsionEnter(Collision other)
    {
        if(other.collider.gameObject.tag == "Player" + m_tag)
        {
            StopCoroutine(m_destroyCoroutine);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(m_lifeTime);
        Destroy(this.gameObject);
    }
	
    public void Launch()
    {
        m_rb.AddForce(transform.forward * m_speed, ForceMode.Impulse);
    }
}

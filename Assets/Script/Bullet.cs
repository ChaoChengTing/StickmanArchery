using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public bool m_isMoving = true;
    private string arrowTag = "Arrow";
    private GameObject m_hitObject;
    private Vector3 m_hitPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (m_isMoving && collision.gameObject.tag != arrowTag)
        {
            m_isMoving = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            m_hitPosition = transform.position - collision.gameObject.transform.position;
            m_hitObject = collision.gameObject;
        }
    }
    
    void Update()
    {
        if (!m_isMoving && m_hitObject)
            transform.position = m_hitPosition + m_hitObject.transform.position;
    }
}